using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChartCenter.WPFUserControl;
using ChartCenter.WPFViewModel;
using JJBoom.Core;
 

namespace ChartCenter
{
    public partial class BoomCatalogContainer : UserControl
    {
        private BoomCatalogContainerViewModel _boomCatalogContainerViewModel;

        public BoomCatalogContainer()
        {
            InitializeComponent();
            BoomCatalogContainerView boomCatalogContainerView = this.elementHost1.Child as BoomCatalogContainerView;
            BoomCatalogContainerViewModel boomCatalogContainerViewModel = new BoomCatalogContainerViewModel();
        
            var files = Directory.GetFiles(UserInfoStorage.GetCurrentJJBoomDocumentFolderPath(), "*.jjb");
            foreach (string file in files)
            {
                BoomCatalogViewModel boomCatalogViewModel = new BoomCatalogViewModel();
                MemoryStream stream = BoomReader.FileToStream(file);
         
                BoomCatalog boomCatalog = (BoomCatalog)StreamUtility.DeserializeFromStream(stream);
                foreach (Boom boom in boomCatalog.Booms)
                {                 
                    BoomStencilViewModel boomStencilViewModel = new BoomStencilViewModel();
                    boomStencilViewModel.SetCurrentViewModelByBoom(boom);
                    boomCatalogViewModel.BoomStencilViewModels.Add(boomStencilViewModel);
                }

                boomCatalogViewModel.BoomCatalogName = boomCatalog.BoomCatalogName;
                boomCatalogViewModel.DeleteThisCatalogViewModel = DeleteThisCatalogViewModel;
                boomCatalogContainerViewModel.BoomCatalogViewModels.Add(boomCatalogViewModel);
            }     
            boomCatalogContainerView.DataContext = boomCatalogContainerViewModel;
            _boomCatalogContainerViewModel = boomCatalogContainerViewModel;
            GlobalBoomCatalogs.GetInstance().BoomCatalogViewModels = boomCatalogContainerViewModel.BoomCatalogViewModels;
        }

        private void DeleteThisCatalogViewModel(BoomCatalogViewModel boomCatalogViewModel)
        {
            _boomCatalogContainerViewModel.BoomCatalogViewModels.Remove(boomCatalogViewModel);
            File.Delete(UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + boomCatalogViewModel.BoomCatalogName + ".jjb"); 
        }

        public IEnumerable<BoomCatalogViewModel> GetAllBoomCatalogViewModel()
        {
            BoomCatalogContainerView boomCatalogContainerView = this.elementHost1.Child as BoomCatalogContainerView;
            BoomCatalogContainerViewModel boomCatalogContainerViewModel = boomCatalogContainerView.DataContext as BoomCatalogContainerViewModel;
            return boomCatalogContainerViewModel.BoomCatalogViewModels;
        }
    }
}
