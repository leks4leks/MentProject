using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class FileHelper
    {
        public static string SaveFile(HttpPostedFileBase file, string serverPath)
        {
            if (file != null)
            {
                string FileName = (Guid.NewGuid()).ToString() + "." + Path.GetFileName(file.FileName).Split('.').ToList().Last();
                file.SaveAs(serverPath + FileName);
                return FileName;
            }
            return string.Empty;
        }
    }
}