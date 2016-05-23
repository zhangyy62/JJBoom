using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJBoom.Core
{
    public static class ClipBoardDataProvider
    {
        public static string GVMLClipFormat = "Art::GVML ClipFormat";
        public static string TableClipFormat = "Art::Table ClipFormat";

        public static CustomTwoTuples<string, Stream> GetStreamFromeClipboard()
        {
            IDataObject dataObject = Clipboard.GetDataObject();
          
            if (dataObject.GetDataPresent(GVMLClipFormat))
            {
                return new CustomTwoTuples<string, Stream>(GVMLClipFormat, dataObject.GetData(GVMLClipFormat) as MemoryStream);
            }
            if (dataObject.GetDataPresent(TableClipFormat))
            {
                return new CustomTwoTuples<string, Stream>(TableClipFormat, dataObject.GetData(TableClipFormat) as MemoryStream);
            }
            return null;
        }

        public static Stream GetPng()
        {
            return (Clipboard.GetDataObject().GetData("PNG") as MemoryStream);
        }
 
    }
}
