using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JJBoom.Core;
using Microsoft.Office.Interop.PowerPoint;
using Clipboard = System.Windows.Forms.Clipboard;

namespace ChartCenter.WPFUserControl
{
    /// <summary>
    /// BoomCatalog.xaml 的交互逻辑
    /// </summary>
    public partial class BoomCatalogContainer : UserControl
    {
        public BoomCatalogContainer()
        {
            InitializeComponent();
            
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem =sender as ListBoxItem;
            BoomCatalog boomCatalog = listBoxItem.Content as BoomCatalog;
            Clipboard.SetDataObject(StencilDataConvert.ConvertToDataObject(boomCatalog.InternalBoom.ShapeData, ClipBoardDataProvider.GVMLClipFormat));
            DocumentWindow documentWindow = Globals.ThisAddIn.Application.ActiveWindow;
            View view = documentWindow.View;
            view.Paste();
        }
    }
}
