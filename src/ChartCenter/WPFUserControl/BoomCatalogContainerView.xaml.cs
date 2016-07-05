using System;
using System.Collections.Generic;
using System.IO;
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
using ChartCenter.WPFViewModel;
using JJBoom;
using JJBoom.Core;
using JJBoom.Core.Files;
using Microsoft.Office.Interop.PowerPoint;
using Clipboard = System.Windows.Forms.Clipboard;

namespace ChartCenter.WPFUserControl
{
    /// <summary>
    /// BoomCatalog.xaml 的交互逻辑
    /// </summary>
    public partial class BoomCatalogContainerView : UserControl
    {
        public BoomCatalogContainerView()
        {
            InitializeComponent();
          
        }

        private void AddCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            BoomCatalogViewModel newBoomCatalogViewModel = new BoomCatalogViewModel();
            newBoomCatalogViewModel.BoomCatalogName = FileNameHelper.GetAvailableCatalogName("New Catalog");
            newBoomCatalogViewModel.DeleteThisCatalogViewModel = DeleteThisCatalogViewModel;
            BoomCatalogContainerViewModel boomCatalogContainerViewModel = this.DataContext as BoomCatalogContainerViewModel;
            boomCatalogContainerViewModel.BoomCatalogViewModels.Add(newBoomCatalogViewModel);
            MemoryStream stream = BoomWriter.SerializeToStream(BoomCatalogConvert.ConvertToBoomsCatalog(newBoomCatalogViewModel));
            BoomWriter.StreamToFile(stream, UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + newBoomCatalogViewModel.BoomCatalogName + ".jjb");
        }

        private void DeleteThisCatalogViewModel(BoomCatalogViewModel boomCatalogViewModel)
        {
            BoomCatalogContainerViewModel boomCatalogContainerViewModel = this.DataContext as BoomCatalogContainerViewModel;
            boomCatalogContainerViewModel.BoomCatalogViewModels.Remove(boomCatalogViewModel);
            File.Delete(UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + boomCatalogViewModel.BoomCatalogName + ".jjb");
        }

        private void DeleteCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomCatalogContainerViewModel boomCatalogViewModel = menuItem.DataContext as BoomCatalogContainerViewModel;
            if (MessageBox.Show("Delete", "Delete Catalog", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                boomCatalogViewModel.BoomCatalogViewModels.Remove(boomCatalogViewModel.SelectedBoomCatalogViewModel);
            }
        }

        private void ExportCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            BoomCatalogContainerViewModel boomCatalogViewModel = menuItem.DataContext as BoomCatalogContainerViewModel;
            boomCatalogViewModel.SelectedBoomCatalogViewModel.ExportCatalog();
        }

    
    }
}
