using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ChartCenter.WPFViewModel;
using JJBoom.Core;
using Microsoft.Office.Interop.PowerPoint;
using Clipboard = System.Windows.Forms.Clipboard;
using Image = System.Drawing.Image;
using MenuItem = System.Windows.Controls.MenuItem;
using Shape = System.Windows.Shapes.Shape;
using UserControl = System.Windows.Controls.UserControl;
using View = Microsoft.Office.Interop.PowerPoint.View;


namespace ChartCenter.WPFUserControl
{
    /// <summary>
    /// BoomCatalog.xaml 的交互逻辑
    /// </summary>
    public partial class BoomCatalogView : UserControl
    {
        
        public BoomCatalogView()
        {
            InitializeComponent();
        }

        public Boom InternalBoom { get; set; }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void ExportCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomCatalogViewModel boomCatalogViewModel = menuItem.DataContext as BoomCatalogViewModel;
            if (boomCatalogViewModel != null)
            {
                boomCatalogViewModel.ExportCatalog();
            }
        }

        private void OnAddToThisCatalogClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomCatalogViewModel boomCatalogViewModel = menuItem.DataContext as BoomCatalogViewModel;
            DocumentWindow documentWindow = Globals.ThisAddIn.Application.ActiveWindow;
            Selection selection = documentWindow.Selection;
            try
            {
                if ((selection != null) && (selection.Type == PpSelectionType.ppSelectionShapes))
                {
                    ShapeRange shapeRange = selection.HasChildShapeRange ? selection.ChildShapeRange : selection.ShapeRange;                     
                    shapeRange.Copy();
                    CustomTwoTuples<string, Stream> streamAndShapeDataFormat = ClipBoardDataProvider.GetStreamFromeClipboard();
                    Boom boom = new Boom();
                    boom.Name = "测试";
                    boom.Icon = Image.FromStream(ClipBoardDataProvider.GetPng());
                    boom.ShapeData = streamAndShapeDataFormat.GetRightOne();
                    boom.ShapeDataFormat = streamAndShapeDataFormat.GetLeftOne();
                    BoomStencilViewModel boomStencilViewModel = new BoomStencilViewModel();
                    boomStencilViewModel.SetCurrentViewModelByBoom(boom);
                    if (boomCatalogViewModel != null)
                    {
                        boomCatalogViewModel.BoomStencilViewModels.Add(boomStencilViewModel);
                    }
                   
                    MemoryStream stream = BoomWriter.SerializeToStream(BoomCatalogConvert.ConvertToBoomsCatalog(boomCatalogViewModel));
                    BoomWriter.StreamToFile(stream,UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + boomCatalogViewModel.BoomCatalogName + ".jjb");
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

        private string _selectedCatalogName;

        private void GetSelectedCatalog(string selectedCatalogName)
        {
            _selectedCatalogName = selectedCatalogName;
        }

        private void OnDoubleClickBoomStencilView(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;

         
            BoomStencilViewModel boomStencilViewModel = listBoxItem.Content as BoomStencilViewModel;

            Clipboard.SetDataObject(StencilDataConvert.ConvertToDataObject(boomStencilViewModel.GetCurrentBoom().ShapeData, boomStencilViewModel.GetCurrentBoom().ShapeDataFormat));
         
            //粘贴操作
            DocumentWindow documentWindow = Globals.ThisAddIn.Application.ActiveWindow;
            View view = documentWindow.View;
            view.Paste();
        }
    }
}
