using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using JJBoom.Core;
 

namespace ChartCenter.WPFViewModel
{
    public class BoomCatalogViewModel : ViewModelBase
    {

        private ObservableCollection<BoomStencilViewModel> _boomStencilViewModels = new ObservableCollection<BoomStencilViewModel>();

        public ObservableCollection<BoomStencilViewModel> BoomStencilViewModels
        {
            get
            {
                return _boomStencilViewModels;
            }

            set
            {
                _boomStencilViewModels = value;
                this.RaisePropertyChanged("BoomStencilViewModels");
            }
        }

        private string _boomCatalogName;

        public string BoomCatalogName
        {
            get
            {
                return _boomCatalogName;
                
            }

            set
            {
                _boomCatalogName = value;
                this.RaisePropertyChanged("BoomCatalogName");
            }
       }

        private bool _headerEnabled;

        public bool HeaderEnabled
        {
            get
            {
                return _headerEnabled; 
                
            }
            set
            {
                _headerEnabled = value;
                this.RaisePropertyChanged("HeaderEnabled");
            }
        }

        private Visibility _userDefinedVisibility = Visibility.Visible;

        public Visibility UserDefinedVisibility
        {
            get
            {
                return _userDefinedVisibility;
                
            }

            set
            {
                _userDefinedVisibility = value;
                this.RaisePropertyChanged("UserDefinedVisibility");
            }
        }

        private Visibility _renameTextBoxVisibility = Visibility.Collapsed;

        public Visibility RenameTextBoxVisibility
        {
            get
            {
                return _renameTextBoxVisibility;

            }

            set
            {
                _renameTextBoxVisibility = value;
                this.RaisePropertyChanged("RenameTextBoxVisibility");
            }
        }

        private Visibility _displayHeaderVisibility = Visibility.Visible;

        public Visibility DisplayHeaderVisibility
        {
            get
            {
                return _displayHeaderVisibility;

            }

            set
            {
                _displayHeaderVisibility = value;
                this.RaisePropertyChanged("DisplayHeaderVisibility");
            }
        }

        public void SetCurrentBoomCatalog(IEnumerable<BoomCatalog> boomCatalogs)
        {
            foreach (BoomCatalog boomCatalog in boomCatalogs)
            {
                BoomStencilViewModel boomStencilViewModel = new BoomStencilViewModel();

                BoomStencilViewModels.Add(boomStencilViewModel);
            }
        }

        public void ExportCatalog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"jjboom files   (*.jjb)|*.jjb";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string localFilePath = saveFileDialog.FileName.ToString(); //获得文件路径 
           /*     BoomCatalog boomCatalog = new BoomCatalog();
                foreach (BoomStencilViewModel boomStencilViewModel in _boomStencilViewModels)
                {
                    Boom boom = boomStencilViewModel.GetCurrentBoom();
                    Boom newBoom = new Boom();
                    newBoom.Name = boom.Name;
                    newBoom.ShapeData = boom.ShapeData;
                    newBoom.ShapeDataFormat = boom.ShapeDataFormat;
                    boomCatalog.Booms.Add(newBoom);
                }*/
                MemoryStream stream = BoomWriter.SerializeToStream(BoomCatalogConvert.ConvertToBoomsCatalog(this));
                // Boom sd = (Boom)StreamUtility.DeserializeFromStream(stream);
                //   Boom list = (Boom)serializer.ReadObject(stream);
                BoomWriter.StreamToFile(stream, localFilePath);
            }
        }

        public Action<BoomCatalogViewModel> DeleteThisCatalogViewModel { get; set; }

    }
}
