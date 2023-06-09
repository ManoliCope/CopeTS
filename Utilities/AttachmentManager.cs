using ProjectX.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class AttachmentManager
    {
        public static bool IsExecutableFile(string fileExtension)
        {
            ExecutableFileExtensions EFX;
            bool Result = Enum.TryParse<ExecutableFileExtensions>(fileExtension.ToUpper(), true, out EFX);
            if (Result = false || EFX != 0)
                return true;
            else
                return false;
        }

        public static bool IsImage(string fileExtension)
        {
            ImageFormats ImageFormat;
            bool Result = Enum.TryParse<ImageFormats>(fileExtension.ToLower(), true, out ImageFormat);
            if (Result = false || ImageFormat == 0)
                return false;
            else
                return true;
        }

        public static bool IsAudio(string fileExtension)
        {
            AudioFormats audioFormats;
            bool Result = Enum.TryParse<AudioFormats>(fileExtension.ToLower(), true, out audioFormats);
            if (Result = false || audioFormats == 0)
                return false;
            else
                return true;
        }

        public static bool IsVideo(string fileExtension)
        {
            VideoFormats videoFormats;
            bool Result = Enum.TryParse<VideoFormats>(fileExtension.ToLower(), true, out videoFormats);
            if (Result = false || videoFormats == 0)
                return false;
            else
                return true;
        }

        public static bool IsDocument(string fileExtension)
        {
            DocumentFormats DocumentFormat;
            bool Result = Enum.TryParse<DocumentFormats>(fileExtension.ToLower(), true, out DocumentFormat);
            if (Result = false || DocumentFormat == 0)
                return false;
            else
                return true;
        }
    }
}
