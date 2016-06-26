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
