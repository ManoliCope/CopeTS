using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Business.Attachment
{
    public interface IAttachmentBusiness
    {
        //Task<FileUploadResp> SaveFile(IFormFileCollection files, int IdFileDirectory, string IdReference, int IdUser, int IdDocumentType);
        Task<FileUploadResp> SaveFile(IList<IFormFile> files, int IdFileDirectory, string IdReference, int IdUser, int IdDocumentType);

        GlobalResponse ClearFile(int IdAttachment, int IdFileDirectory, string IdRefrence, int IdDocumentType);

        DisplayFile DisplayFile(DisplayFileReq request);

        AttachmentModel GetAttachments(FileDirectories fileDirectory, string IdReference);

        List<Entities.dbModels.Attachment> GetAttachments(ObjectReferences objectReferences, DocumentTypes documentTypes, string IdReference);
    }
}