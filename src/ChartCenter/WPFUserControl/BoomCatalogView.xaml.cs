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
using Brush = System.Drawing.Brush;
using Clipboard = System.Windows.Forms.Clipboard;
using Image = System.Drawing.Image;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;
using Shape = System.Windows.Shapes.Shape;
using TextBox = System.Windows.Controls.TextBox;
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

        private void DeleteCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomCatalogViewModel boomCatalogViewModel = menuItem.DataContext as BoomCatalogViewModel;
            if (MessageBox.Show(string.Format("Delete Catalog {0}", boomCatalogViewModel.BoomCatalogName), "Delete Catalog", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                boomCatalogViewModel.DeleteThisCatalogViewModel.Invoke(boomCatalogViewModel);
            }
        }

        private void ExportCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomCatalogViewModel boomCatalogViewModel = menuItem.DataContext as BoomCatalogViewModel;
            
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


        private void OnRenameCatalogClick(object sender, RoutedEventArgs e)
        {
            BoomCatalogViewModel boomCatalogViewModel = this.DataContext as BoomCatalogViewModel;
            boomCatalogViewModel.HeaderEnabled = true;
       

        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = true;
          
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.IsEnabled = false;
            e.Handled = true;  
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                textBox.IsEnabled = false;
                e.Handled = true;
            }
            e.Handled = false;
        }

        private void UIElement_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if ((bool)e.NewValue)
            {
                textBox.Focus();
            
                textBox.SelectAll();
                textBox.Background = Brushes.White;
            }
            textBox.Background = Brushes.Transparent;
        }

        private void OnRenameStencilClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomStencilViewModel boomStencilViewModel = menuItem.DataContext as BoomStencilViewModel;
            boomStencilViewModel.TextEnabled = true;
        }

        private void OnDeleteStencilClick(object sender, RoutedEventArgs e)
        {
            BoomCatalogViewModel boomCatalogViewModel = this.DataContext as BoomCatalogViewModel;
            BoomStencilViewModel boomStencilViewModel = sender as BoomStencilViewModel;
            boomCatalogViewModel.BoomStencilViewModels.Remove(boomStencilViewModel);
        }
    }
}
