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
 
using JJBoom.Core;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using Newtonsoft.Json;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using ShapeRange = Microsoft.Office.Interop.PowerPoint.ShapeRange;
using View = Microsoft.Office.Interop.PowerPoint.View;

namespace JJBoom
{
    public partial class Ribbon1
    {
        private string filepath = String.Empty;

        private CustomTaskPane _customTaskPane  ;

        private BoomCatalogContainer _boomCatalogContainer = new BoomCatalogContainer();

        private string _selectedCatalogName;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
             _customTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(_boomCatalogContainer, "JJ Boom");
            _customTaskPane.Width = 480;
            _customTaskPane.Visible = false;                       
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
                    boomCatalogViewModel.AddStencil(boomStencilViewModel);

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

        private void Ribbon1_Close(object sender, EventArgs e)
        {
            foreach (BoomCatalogViewModel boomCatalogViewModel in GlobalBoomCatalogsCache.GetInstance().GetChangedBoomCatalogs())
            {
                MemoryStream stream = BoomWriter.SerializeToStream(BoomCatalogConvert.ConvertToBoomsCatalog(boomCatalogViewModel));
                BoomWriter.StreamToNewFile(stream, UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + boomCatalogViewModel.FileName + ".jjb");
            }
        }
    }
}
