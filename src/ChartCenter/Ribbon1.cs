using System;
using System.Collections.Generic;
using System.Configuration;
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
using ChartCenter.WPFViewModel;
using JJBoom.Core;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using Newtonsoft.Json;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using ShapeRange = Microsoft.Office.Interop.PowerPoint.ShapeRange;
using View = Microsoft.Office.Interop.PowerPoint.View;

namespace ChartCenter
{
    public partial class Ribbon1
    {
        // private readonly string filepath = @"C:\Users\rabook\Desktop\test\1.jjb";
        private string filepath = String.Empty;

        private CustomTaskPane _customTaskPane  ;

        private BoomCatalogContainer _boomCatalogContainer = new BoomCatalogContainer();

        private string _selectedCatalogName;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            filepath += Environment.CurrentDirectory + ConfigurationManager.AppSettings["StencilsPath"];
            if (!File.Exists(filepath))
            {
                return;
            }
         
            
            MemoryStream stream = BoomReader.FileToStream(filepath);
         /*   Boom boom = (Boom)StreamUtility.DeserializeFromStream(stream);

            _boomCatalogContainer.SetBoomCatalog(boom);*/
             _customTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(_boomCatalogContainer, "JJ Boom");
            _customTaskPane.Width = 480;
            _customTaskPane.Visible = false;                       
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

                MemoryStream stream = BoomWriter.SerializeToStream(boom);
                Boom sd = (Boom)StreamUtility.DeserializeFromStream(stream);
                //   Boom list = (Boom)serializer.ReadObject(stream);
                BoomWriter.StreamToFile(stream, filepath);
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
 

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
           MemoryStream stream = BoomReader.FileToStream(filepath);
           Boom boom = (Boom)StreamUtility.DeserializeFromStream(stream);
           Clipboard.SetDataObject(StencilDataConvert.ConvertToDataObject(boom.ShapeData, ClipBoardDataProvider.GVMLClipFormat));
          
        }
       
        private void ShowPanel_Click(object sender, RibbonControlEventArgs e)
        {
          
            if (Globals.ThisAddIn.CustomTaskPanes.Contains(_customTaskPane))
            {
                _customTaskPane.Visible = true;
            }  
        }

        private void ExportAllJJs_Click(object sender, RibbonControlEventArgs e)
        {
           
        }

        private void AddToCatalog_Click(object sender, RibbonControlEventArgs e)
        {
            DocumentWindow documentWindow = Globals.ThisAddIn.Application.ActiveWindow;
            Selection selection = documentWindow.Selection;
            try
            {
                if ((selection != null) && (selection.Type == PpSelectionType.ppSelectionShapes))
                {
                    SelectCatalogForm selectCatalogForm = new SelectCatalogForm();
                    selectCatalogForm.GetSelectedCatalogNameAction += GetSelectedCatalog;
                    selectCatalogForm.ShowDialog();
                    //BoomCatalogViewModel boomCatalogViewModel = GlobalBoomCatalogs.GetInstance().GetBoomCatalogViewModelByName(_selectedCatalogName);
                    BoomCatalogViewModel boomCatalogViewModel = null;
                    foreach (BoomCatalogViewModel catalogViewModel in _boomCatalogContainer.GetAllBoomCatalogViewModel())
                    {
                        if (catalogViewModel.BoomCatalogName == _selectedCatalogName)
                        {
                            boomCatalogViewModel = catalogViewModel;
                        }   
                    }
                    ShapeRange shapeRange = selection.HasChildShapeRange ? selection.ChildShapeRange : selection.ShapeRange;
                    Dictionary<Shape, string> dictionary = ShapeRangeDeCompose.smethod_1(shapeRange);
                    shapeRange.Copy();
                    CustomTwoTuples<string, Stream> streamAndShapeDataFormat = ClipBoardDataProvider.GetStreamFromeClipboard();
               
                    Boom boom = new Boom();
                    boom.Name = "测试";
                    boom.Icon = Image.FromStream(ClipBoardDataProvider.GetPng());
                    boom.ShapeData = streamAndShapeDataFormat.GetRightOne();
                    boom.ShapeDataFormat = streamAndShapeDataFormat.GetLeftOne();
                    BoomStencilViewModel boomStencilViewModel = new BoomStencilViewModel();
                    boomStencilViewModel.SetCurrentViewModelByBoom(boom);
                    boomCatalogViewModel.BoomStencilViewModels.Add(boomStencilViewModel);

                }
            }
            finally
            {
                if (selection != null)
                {
                    Marshal.ReleaseComObject(selection);
                }
            }
          

        }

        private void GetSelectedCatalog(string selectedCatalogName)
        {
            _selectedCatalogName = selectedCatalogName;
        }

        
    }
}
