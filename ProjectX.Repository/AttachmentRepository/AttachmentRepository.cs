using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ProjectX.Repository.AttachmentRepository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;
        public AttachmentRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public Attachment SaveAttachment(string IdReference, int IdFileDirectory, int IdFileType, string FileFormat, int IdUser, string FileMD5, string FileName, string FileDesc, int IdObjectReference, int IdDocumentType, out int status)
        {
            Attachment attachment = new Attachment();

            var param = new DynamicParameters();

            param.Add("@IdRefrence", IdReference);
            param.Add("@IdFileSection", 0);
            param.Add("@IdFileDirectory", IdFileDirectory);
            param.Add("@IdObjectReference", IdObjectReference);
            param.Add("@IdFileType", IdFileType);
            param.Add("@IdFileFormat", FileFormat.ToLower());
            param.Add("@IsPrimary", 0);
            param.Add("@FileOrder", 0);
            param.Add("@IdUser", IdUser);
            param.Add("@FileMD5", FileMD5);
            param.Add("@FileName", FileName);
            param.Add("@FileDesc", FileDesc);
            param.Add("@IdDocumentType", IdDocumentType);
            param.Add("@IdAttachment", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Status", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (TransactionScope scope = new TransactionScope())
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    using (SqlMapper.GridReader result = _db.QueryMultiple("tr_attachment_save", param, commandType: CommandType.StoredProcedure))
                    {
                        attachment = result.ReadFirstOrDefault<Attachment>();
                    }
                    status = param.Get<int>("@Status");
                }
                scope.Complete();
            }

            return attachment;
        }

        public Attachment ClearFile(int IdAttachment, FileDirectories fileDirectory, string IdReference, int IdDocumentType)
        {
            var param = new DynamicParameters();
            param.Add("@IdAttachment", IdAttachment);
            param.Add("@IdRefrence", IdReference);
            param.Add("@IdFileDirectory", (int)fileDirectory);
            param.Add("@IdDocumentType", IdDocumentType);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_attachment_clear", param, commandType: CommandType.StoredProcedure))
                {
                    return result.ReadFirstOrDefault<Attachment>();
                }
            }
        }

        public DisplayFile DisplayAttachment(string FileAttachmentMD5, string FileDirectoryMD5, bool IsThumb)
        {
            DisplayFile displayFile = new DisplayFile();

            var param = new DynamicParameters();
            param.Add("@FileAttachmentMD5", FileAttachmentMD5);
            param.Add("@FileDirectoryMD5", FileDirectoryMD5);
            param.Add("@IsThumb", @IsThumb);
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_attachment_display", param, commandType: CommandType.StoredProcedure))
                {
                    displayFile = result.Read<DisplayFile>().FirstOrDefault();
                }
            }
            return displayFile;
        }

        public AttachmentModel GetAttachments(string IdReference, FileDirectories fileDirectory)
        {
            AttachmentModel attachmentModel = new AttachmentModel();
            var param = new DynamicParameters();
            param.Add("@IdRefrence", IdReference);
            param.Add("@IdFileDirectory", (int)fileDirectory);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_attachment_select_all", param, commandType: CommandType.StoredProcedure))
                {
                    attachmentModel = result.ReadFirstOrDefault<AttachmentModel>();
                    if (attachmentModel != null)
                    {
                        attachmentModel.requiredDocuments = result.Read<RequiredDocument>().ToList();
                        attachmentModel.attahcments = result.Read<Attachment>().ToList();
                    }
                }
            }
            return attachmentModel;
        }

        public Attachment GetAttachment(string IdAttachment)
        {
            var param = new DynamicParameters();
            param.Add("@IdAttachment", IdAttachment);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_attachment_select", param, commandType: CommandType.StoredProcedure))
                {
                    return result.ReadFirstOrDefault<Attachment>();
                }
            }
        }

        public List<Attachment> GetAttachments(ObjectReferences objectReferences, DocumentTypes documentTypes , string IdReference)
        {
            var param = new DynamicParameters();
            param.Add("@IdObjectReference", (int)objectReferences);
            param.Add("@IdDocumentType", (int)documentTypes);
            param.Add("@IdReference", IdReference);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_attachment_select_document", param, commandType: CommandType.StoredProcedure))
                {
                    return result.Read<Attachment>().ToList();
                }
            }
        }

    }
}