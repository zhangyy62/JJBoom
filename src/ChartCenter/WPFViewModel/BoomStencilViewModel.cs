using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JJBoom.Core;
using Microsoft.Office.Interop.PowerPoint;
using View = System.Windows.Forms.View;

namespace JJBoom
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

        private bool _textEnabled;

        public bool TextEnabled
        {
            get
            {
                return _textEnabled;
            }

            set
            {
                _textEnabled = value;
                this.RaisePropertyChanged("TextEnabled");
            }
        }


        private Visibility _disPlayStencilNameVisibility = Visibility.Visible;

        public Visibility DisPlayStencilNameVisibility
        {
            get
            {
                return _disPlayStencilNameVisibility;

            }

            set
            {
                _disPlayStencilNameVisibility = value;
                this.RaisePropertyChanged("DisPlayStencilNameVisibility");
            }
        }

        private Visibility _renameStencilNameVisibility = Visibility.Collapsed;

        public Visibility RenameStencilNameVisibility
        {
            get
            {
                return _renameStencilNameVisibility;

            }

            set
            {
                _renameStencilNameVisibility = value;
             
                this.RaisePropertyChanged("RenameStencilNameVisibility");
            }
        }

        public Action FocusAndSelectAll { get; set; } 

        public void SetCurrentViewModelByBoom(Boom boom)
        {
            _boom = boom;
            if (boom.Icon == null)
            {
                boom.Icon = Image.FromStream(boom.PreviewImage);
            }
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
