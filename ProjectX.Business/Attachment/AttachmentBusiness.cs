using ProjectX.Business.Caching;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.File;
using ProjectX.Entities.Resources;
using ProjectX.Repository.AttachmentRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

namespace ProjectX.Business.Attachment
{
    public class AttachmentBusiness : IAttachmentBusiness
    {
        private IAttachmentRepository _attachmentRepository;
        private readonly TrAppSettings _appSettings;
        private IDatabaseCaching _databaseCaching;
        private IList<FileDirectory> _fileDirectories;
        private IList<AppConfig> _appConfigs;
        private readonly ILogger<AttachmentBusiness> _logger;

        public AttachmentBusiness(IOptions<TrAppSettings> appIdentitySettingsAccessor, IAttachmentRepository attachmentRepository, IDatabaseCaching databaseCaching, ILogger<AttachmentBusiness> logger)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
            _attachmentRepository = attachmentRepository;
            _databaseCaching = databaseCaching;
            _fileDirectories = _databaseCaching.GetFileDirectories();
            _appConfigs = _databaseCaching.GetAppConfigs();
            _logger = logger;
        }

        //public async Task<FileUploadResp> SaveFile(IFormFileCollection files, int IdFileDirectory, string IdReference, int IdUser, int IdDocumentType)
        public async Task<FileUploadResp> SaveFile(IList<IFormFile> files, int IdFileDirectory, string IdReference, int IdUser, int IdDocumentType)
        {
            FileUploadResp response = new FileUploadResp();

            if (files == null || files.Count == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                return response;
            }

            FileDirectory fileDirectory = _fileDirectories.FirstOrDefault(x => x.IdFileDirectory == IdFileDirectory);

            if (fileDirectory == null)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                return response;
            }

            if (FileDirectories.Default_GOP_Template != (FileDirectories)fileDirectory.IdFileDirectory && ValueChecker.IsNullValue(IdReference))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                return response;
            }

            //Check Media Types
            foreach (IFormFile formFile in files)
            {
                if (formFile.Length > (5 * 1024 * 1024)) // File size limit 5  mb other files
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.FileSizeTooLarge);
                    return response;
                }

                // Check If Executable
                if (AttachmentManager.IsExecutableFile(Path.GetExtension(formFile.FileName).Replace(".", "").ToUpper()))
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                    return response;
                }

                // Check File Type
                if ((FileTypes)fileDirectory.IdFileType != FileTypes.Any)
                {
                    switch ((FileTypes)fileDirectory.IdFileType)
                    {
                        case FileTypes.Image:
                            if (!AttachmentManager.IsImage(Path.GetExtension(formFile.FileName).Replace(".", "")))
                            {
                                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                                return response;
                            }
                            break;
                        case FileTypes.Audio:
                            if (!AttachmentManager.IsAudio(Path.GetExtension(formFile.FileName).Replace(".", "")))
                            {
                                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                                return response;
                                //// Convert From CAF to MP3
                                //if (Path.GetExtension(formFile.FileName).Replace(".", "") == AudioFormats.caf.ToString())
                                //{
                                //Path.ChangeExtension(formFile.FileName, ".mp3");
                                //    FFMpegConverter ffMpeg = new FFMpegConverter();
                                //    ffMpeg.ConvertLiveMedia((pathToVideoFile, "video.mp4", Format.mp3);
                                //}
                            }
                            break;
                        case FileTypes.Video:
                            if (!AttachmentManager.IsVideo(Path.GetExtension(formFile.FileName).Replace(".", "")))
                            {
                                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                                return response;
                            }
                            break;
                        case FileTypes.Document:
                            if (!AttachmentManager.IsDocument(Path.GetExtension(formFile.FileName).Replace(".", "")))
                            {
                                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                                return response;
                            }
                            break;
                        default:
                            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                            return response;
                    }
                }

                response.uploadedFiles.Add(new UploadedFile
                {
                    IdReference = IdReference,
                    FileName = Path.GetFileNameWithoutExtension(formFile.FileName),
                    IdFileType = fileDirectory.IdFileType,
                    IsDone = false,
                    file = formFile,
                    FileDesc = Path.ChangeExtension(formFile.FileName, null)
                });
            }
            string BaseDirectory = _appConfigs.Where(x => x.Code == "BSDIR").FirstOrDefault().Value;
            //string BaseDirectory = @"C:\ccAttachments\";
            //fileDirectory.UploadDirectory = fileDirectory.UploadDirectory.Replace("H:", "D:");
           Directory.CreateDirectory(Path.Combine(BaseDirectory, fileDirectory.UploadDirectory));

            foreach (UploadedFile uploadedFile in response.uploadedFiles)
            {
                string FileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(uploadedFile.file.FileName));


                bool fileSaved = false;
                string filePath = string.Empty;
                string FullPath = Path.Combine(BaseDirectory, fileDirectory.UploadDirectory, FileName);
                try
                {
                    if ((FileTypes)fileDirectory.IdFileType == FileTypes.Video)
                    {
                        FileName = Path.ChangeExtension(FileName, ".mp4");
                        FullPath = Path.Combine(BaseDirectory, fileDirectory.UploadDirectory, FileName);
                        string FilePath = string.Empty;
                        string path = string.Empty;
                    }

                    if ((FileTypes)fileDirectory.IdFileType == FileTypes.Audio)
                    {
                        FileName = Path.ChangeExtension(FileName, ".mp3");
                        FullPath = Path.Combine(BaseDirectory, fileDirectory.UploadDirectory, FileName);
                        string FilePath = string.Empty;
                        string path = string.Empty;
                    }

                    /*if (uploadedFile.file.ContentType.ToLower() == "application/pdf")
                    {
                        byte[] fileBytes;
                        using (var ms = new MemoryStream())
                        {
                            uploadedFile.file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        PdfLoadedDocument loadedDocument = new PdfLoadedDocument(fileBytes);
                        PdfCompressionOptions options = new PdfCompressionOptions
                        {
                            CompressImages = true,
                            ImageQuality = 50,
                            OptimizeFont = true,
                            OptimizePageContents = true,
                            RemoveMetadata = true
                        };

                        loadedDocument.Compress(options);
                        MemoryStream outputDocument = new MemoryStream();
                        loadedDocument.Save(outputDocument);
                        outputDocument.Position = 0;
                        loadedDocument.Close(true);

                        using (FileStream fileStream = new FileStream(Path.Combine(BaseDirectory, fileDirectory.UploadDirectory, FileName), FileMode.Create))
                        {
                            byte[] bytes = new byte[outputDocument.Length];
                            outputDocument.Read(bytes, 0, (int)outputDocument.Length);
                            await fileStream.WriteAsync(bytes, 0, bytes.Length);
                            fileSaved = true;
                        }
                    }
                    else
                    {*/
                    using (FileStream fileStream = new FileStream(Path.Combine(BaseDirectory, fileDirectory.UploadDirectory, FileName), FileMode.Create))
                    {
                            await uploadedFile.file.CopyToAsync(fileStream);
                            fileSaved = true;
                    }
                    /*}*/
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Attachment/Save");
                    uploadedFile.FilePath = string.Empty;
                    uploadedFile.IsDone = false;
                }
                if (fileSaved)
                {
                    int status = 0;
                    string fileMD5 = DataHashing.CreateMD5(Guid.NewGuid().ToString());
                    Entities.dbModels.Attachment attachment = _attachmentRepository.SaveAttachment(uploadedFile.IdReference, IdFileDirectory, uploadedFile.IdFileType, Path.GetExtension(FileName), IdUser, fileMD5, FileName, uploadedFile.FileDesc, fileDirectory.IdObjectReference, IdDocumentType, out status);
                    if ((StatusCodeValues)status == StatusCodeValues.success)
                    {
                        uploadedFile.idAttachment = attachment.IdAttachment;
                        uploadedFile.idDocumentType = attachment.IdDocumentType;
                        uploadedFile.documentType = attachment.DocumentType;
                        filePath = string.Concat(_appConfigs.Where(x => x.Code == "APILK").FirstOrDefault().Value, "?f=", fileDirectory.NameMd5, "&r=", fileMD5, "&t=false");
                    }
                }
                if (!string.IsNullOrEmpty(filePath))
                {
                    uploadedFile.FilePath = filePath;
                    uploadedFile.IsDone = true;
                    uploadedFile.file = null;
                }
            }
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            return response;
        }

        public GlobalResponse ClearFile(int IdAttachment, int IdFileDirectory, string IdRefrence, int IdDocumentType)
        {
            GlobalResponse response = new GlobalResponse();
            string BaseDirectory = _appConfigs.Where(x => x.Code == "BSDIR").FirstOrDefault().Value;

            if (ValueChecker.IsZeroValue(IdAttachment))
            {
                if (ValueChecker.IsZeroValue(IdFileDirectory) || ValueChecker.IsNullValue(IdRefrence))
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
                    return response;
                }
            }

            FileDirectory fileDirectory = _fileDirectories.FirstOrDefault(x => x.IdFileDirectory == IdFileDirectory);
            Entities.dbModels.Attachment file = _attachmentRepository.ClearFile(IdAttachment, (FileDirectories)IdFileDirectory, IdRefrence, IdDocumentType);

            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            return response;
        }

        public DisplayFile DisplayFile(DisplayFileReq request)
        {
            DisplayFile displayFile = _attachmentRepository.DisplayAttachment(request.r, request.f, request.t);

            if (!string.IsNullOrEmpty(displayFile.FilePath))
            {
                switch (Path.GetExtension(displayFile.FilePath).ToLower())
                {
                    //Audios
                    case ".wav":
                        displayFile.ContentType = "audio/wav";
                        break;
                    case ".mp3":
                        displayFile.ContentType = "audio/mpeg";
                        break;
                    //Videos
                    case ".mp4":
                        displayFile.ContentType = "video/mp4";
                        break;
                    // Images
                    case ".memorybmp":
                    case ".bmp":
                        displayFile.ContentType = "image/bmp";
                        break;
                    case ".emf":
                    case ".wmf":
                        displayFile.ContentType = "application/x-msmetafile";
                        break;
                    case ".gif":
                        displayFile.ContentType = "image/gif";
                        break;
                    case ".png":
                        displayFile.ContentType = "image/png";
                        break;
                    case ".tiff":
                    case ".tif":
                        displayFile.ContentType = "image/tiff";
                        break;
                    case ".icon":
                        displayFile.ContentType = "image/x-icon";
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".jfif":
                        displayFile.ContentType = "image/jpeg";
                        break;
                    //Documents
                    case ".pdf":
                        displayFile.ContentType = "application/pdf";
                        break;
                    case ".csv":
                    case ".xlsx":
                    case ".xls":
                        displayFile.ContentType = "application/vnd.ms-excel";
                        break;
                    case ".docx":
                    case ".doc":
                        displayFile.ContentType = "application/msword";
                        break;
                    case ".zip":
                        displayFile.ContentType = "application/zip";
                        break;
                    default:
                        displayFile = null;
                        break;
                }
            }

            displayFile.FilePath = Path.Combine(_appConfigs.Where(x => x.Code == "BSDIR").FirstOrDefault().Value,
                _fileDirectories.FirstOrDefault(x => x.IdFileDirectory == displayFile.IdFileDirectory).UploadDirectory,
                displayFile.FilePath);
            return displayFile;
        }

        public AttachmentModel GetAttachments(FileDirectories fileDirectory, string IdReference)
        {
            AttachmentModel attachmentModel = new AttachmentModel();

            attachmentModel = _attachmentRepository.GetAttachments(IdReference, fileDirectory);


            if (attachmentModel.attahcments != null)
            {
                if (attachmentModel.attahcments.Count > 0)
                {
                    foreach (Entities.dbModels.Attachment attachment in attachmentModel.attahcments)
                    {
                        attachment.FileUrl = string.Concat(_appConfigs.Where(x => x.Code == "APILK").FirstOrDefault().Value, "?f=", attachment.f, "&r=", attachment.r, "&t=false");
                    }
                }
            }

            return attachmentModel;
        }

        public List<Entities.dbModels.Attachment> GetAttachments(ObjectReferences objectReferences, DocumentTypes documentTypes, string IdReference)
        {
            List<Entities.dbModels.Attachment> attachments = _attachmentRepository.GetAttachments(objectReferences, documentTypes, IdReference);

            foreach (Entities.dbModels.Attachment attachment in attachments)
            {
                attachment.FileUrl = Path.Combine(_appConfigs.Where(x => x.Code == "BSDIR").FirstOrDefault().Value,
                    _fileDirectories.FirstOrDefault(x => x.IdFileDirectory == attachment.IdFileDirectory).UploadDirectory,
                    attachment.FileUrl);
            }
            return attachments;
        }
    }
}
