using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using JJBoom.Core;

namespace ChartCenter
{
    public partial class BoomCatalogContainer : UserControl
    {
        public BoomCatalogContainer()
        {
            InitializeComponent();
         
       

        }

        public void SetBoomCatalog(Boom boom)
        {
        /*    BoomCatalogContainerUserControl s = this.elementHost1.Child as BoomCatalogContainerUserControl;
            BoomCatalog boomCatalog = new BoomCatalog();
            boomCatalog.InternalBoom = boom;
            boomCatalog.Image.Source = ToWpfImage(boom.Icon);
            boomCatalog.TextBlock.Text = boom.Name;
            s.boomCatalogContainer.Items.Add(boomCatalog);*/
        }

        public static Stream ToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public static BitmapImage ToWpfImage(System.Drawing.Image img)
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
