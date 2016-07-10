using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJBoom.Core.Files
{
    public static class FileHelper
    {
        /// <summary>
        /// auto move file to new name path.appending '_' when same name 
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public static void RenameFile(string oldName, string newName)
        {
            Directory.Move(UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + oldName + ".jjb", UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + newName + ".jjb");
        }

        public static void DeleteFile(string path)
        {
            Directory.Delete(UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + path + ".jjb");
        }
    }
}
