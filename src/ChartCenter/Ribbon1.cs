using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using JJBoom.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using Newtonsoft.Json;
using View = Microsoft.Office.Interop.PowerPoint.View;

namespace ChartCenter
{
    public partial class Ribbon1
    {
        private readonly string filepath = @"C:\Users\rabook\Desktop\powermockup2.3.1\1.jjb";
 

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            MemoryStream stream = FileToStream(filepath);
            Boom boom = (Boom)DeserializeFromStream(stream);
            BoomCatalogContainer boomCatalogContainer = new BoomCatalogContainer();
            boomCatalogContainer.SetBoomCatalog(boom);
            var currentTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(boomCatalogContainer, "JJ Boom");
            currentTaskPane.Visible = true;
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            DocumentWindow documentWindow = Globals.ThisAddIn.Application.ActiveWindow;
            Selection selection = documentWindow.Selection;
            if ((selection != null) && (selection.Type == PpSelectionType.ppSelectionShapes))
            {
                   ShapeRange shapeRange = selection.HasChildShapeRange ? selection.ChildShapeRange : selection.ShapeRange;
                   Dictionary<Shape, string> dictionary = ShapeRangeDeCompose.smethod_1(shapeRange);
                   shapeRange.Copy();
                CustomTwoTuples<string, Stream> streamAndShapeDataFormat = ClipBoardDataProvider.GetStreamFromeClipboard();
                CustomTwoTuples<string, Stream> class2 = ClipBoardDataProvider.GetStreamFromeClipboard();
                Boom boom = new Boom();
                boom.Name = "张轩测试";
                boom.Icon = Image.FromStream(ClipBoardDataProvider.GetPng());
                boom.ShapeData = streamAndShapeDataFormat.GetRightOne();
                boom.ShapeDataFormat = streamAndShapeDataFormat.GetLeftOne();
                /*   using (FileStream sFile = new FileStream(@"C:\Users\rabook\Desktop\powermockup2.3.1\1.hidat", FileMode.OpenOrCreate))
                   {
                       sFile.Write(StreamToBytes(class2.GetRightOne()), 0, 0);
                   }*/

                MemoryStream stream = SerializeToStream(boom);
                Boom sd = (Boom)DeserializeFromStream(stream);
                //   Boom list = (Boom)serializer.ReadObject(stream);
                StreamToFile(stream, filepath);
            }

            /*       try
                   {
                       if ((shapeRange != null) && (shapeRange.Count > 0))
                       {
                           IStencil stencil = StencilCategoryHelper.smethod_3();
                           if (stencil != null)
                           {
                            //   this.adxtaskPane_0.set_Visible(true);
                              /* Application.DoEvents();
                               StencilLib currentStencilLib = this.CurrentStencilLib;
                               if (currentStencilLib != null)
                               {
                                   currentStencilLib.method_1(stencil);
                               }#1#
                           }
                       }
                       else
                       {
                          // Dialogs.ShowInfoMessage(Strings.ShapeSelectionRequired);
                       }
                   }
                   finally
                   {
                       if (shapeRange != null)
                       {
                           Marshal.ReleaseComObject(shapeRange);
                       }
                   }*/
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            DocumentWindow documentWindow = Globals.ThisAddIn.Application.ActiveWindow;
            View view = documentWindow.View;
            view.Paste();
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public void StreamToFile(Stream stream, string fileName)
        {

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fs);
            }
        }

        public MemoryStream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
           MemoryStream stream = FileToStream(filepath);
           Boom boom = (Boom)DeserializeFromStream(stream);
           Clipboard.SetDataObject(StencilDataConvert.ConvertToDataObject(boom.ShapeData, ClipBoardDataProvider.GVMLClipFormat));
          
        }

        /// <summary>
        /// serializes the given object into memory stream
        /// </summary>
        /// <param name="objectType">the object to be serialized</param>
        /// <returns>The serialized object as memory stream</returns>
        public static MemoryStream SerializeToStream(object objectType)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, objectType);
            return stream;
        }

        /// <summary>
        /// deserializes as an object
        /// </summary>
        /// <param name="stream">the stream to deserialize</param>
        /// <returns>the deserialized object</returns>
        public static object DeserializeFromStream(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object objectType = formatter.Deserialize(stream);
            return objectType;
        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            MemoryStream stream = FileToStream(filepath);
            Boom boom = (Boom)DeserializeFromStream(stream);
            BoomCatalogContainer boomCatalogContainer = new BoomCatalogContainer();
            boomCatalogContainer.SetBoomCatalog(boom);
            var currentTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(boomCatalogContainer, "JJ Boom");
            currentTaskPane.Visible = true;
        }
    }
}
