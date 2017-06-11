using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJBoom.Core
{
    public static class UserInfoStorage
    {
        private readonly static string _userDocumentPath;

        private readonly static string _jjboomDocumentFolder;

         static UserInfoStorage()
        {
            _userDocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _jjboomDocumentFolder = _userDocumentPath + @"\JJBoom\";
            if (!Directory.Exists(_jjboomDocumentFolder))
            {
                Directory.CreateDirectory(_jjboomDocumentFolder);
            }
        }

        public static string GetCurrentJJBoomDocumentFolderPath()
        {
            return _jjboomDocumentFolder;
        }
    }
}
