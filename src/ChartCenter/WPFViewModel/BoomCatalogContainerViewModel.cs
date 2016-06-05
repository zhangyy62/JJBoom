using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJBoom.Core;

namespace ChartCenter.WPFViewModel
{
    public class BoomCatalogContainerViewModel : ViewModelBase
    {
        private ObservableCollection<BoomCatalogViewModel> _boomCatalogViewModels = new ObservableCollection<BoomCatalogViewModel>();

        public ObservableCollection<BoomCatalogViewModel> BoomCatalogViewModels
        {
            get
            {
                return _boomCatalogViewModels;
            }

            set
            {
                _boomCatalogViewModels = value;
                this.RaisePropertyChanged("BoomCatalogViewModels");
            }
        }

        public BoomCatalogViewModel SelectedBoomCatalogViewModel { get; set; }

     
    }
}
