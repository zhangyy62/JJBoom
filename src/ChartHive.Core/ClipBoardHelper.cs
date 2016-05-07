using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartHive.Core
{
    public static class ClipBoardHelper
    {
        public static string string_0 = "Art::GVML ClipFormat";
        public static string string_1 = "Art::Table ClipFormat";

        public static CustomTwoTuples<string, Stream> smethod_0()
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(string_0))
            {
                return new CustomTwoTuples<string, Stream>(string_0, dataObject.GetData(string_0) as MemoryStream);
            }
            if (dataObject.GetDataPresent(string_1))
            {
                return new CustomTwoTuples<string, Stream>(string_1, dataObject.GetData(string_1) as MemoryStream);
            }
            return null;
        }

        public static Stream smethod_1()
        {
            return (Clipboard.GetDataObject().GetData("PNG") as MemoryStream);
        }
 
    }
}
