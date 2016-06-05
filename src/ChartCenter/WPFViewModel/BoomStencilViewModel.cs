using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChartCenter.WPFUserControl;
using JJBoom.Core;

namespace ChartCenter.WPFViewModel
{
    public class BoomStencilViewModel : ViewModelBase
    {
        private bool _isReadOnly = true;

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly; 
                
            }

            set
            {
                _isReadOnly = value;
                this.RaisePropertyChanged("IsReadOnly");
            }
        }

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }

            set
            {
                _imageSource = value;
                this.RaisePropertyChanged("ImageSource");
            }
        }

        private string _text;

        private Boom _boom;

        public Boom GetCurrentBoom()
        {
            return _boom;
        }
 

        public string Text
        {
            get
            {
                return _text; 
                
            }
            set
            {
                _text = value;
                this.RaisePropertyChanged("Text");
            }
        }

        public void SetCurrentViewModelByBoom(Boom boom)
        {
            _boom = boom;
            /* BoomCatalog boomCatalog = new BoomCatalog();
             boomCatalog.InternalBoom = boom;
             boomCatalog.Image.Source = ToWpfImage(boom.Icon);
             boomCatalog.TextBlock.Text = boom.Name;*/
            ImageSource = ToWpfImage(boom.Icon);
            Text = boom.Name;
        }


        private static BitmapImage ToWpfImage(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }
    }
}
