using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.AttachmentRepository
{
    public interface IAttachmentRepository
    {
        Attachment SaveAttachment(string IdReference, int IdFileDirectory, int IdFileType, string FileFormat, int IdUser, string FileMD5, string FileName, string FileDesc, int IdObjectReference, int IdDocumentType, out int status);
        Attachment ClearFile(int IdAttachment, FileDirectories fileDirectory, string IdReference, int IdDocumentType);
        DisplayFile DisplayAttachment(string FileAttachmentMD5, string FileDirectoryMD5, bool IsThumb);
        AttachmentModel GetAttachments(string IdReference, FileDirectories fileDirectory);
        Attachment GetAttachment(string IdAttachment);
        List<Attachment> GetAttachments(ObjectReferences objectReferences, DocumentTypes documentTypes, string IdReference);
    }
}
