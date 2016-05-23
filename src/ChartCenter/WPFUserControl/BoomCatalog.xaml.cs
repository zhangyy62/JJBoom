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
using JJBoom.Core;

namespace ChartCenter.WPFUserControl
{
    /// <summary>
    /// BoomCatalog.xaml 的交互逻辑
    /// </summary>
    public partial class BoomCatalog : UserControl
    {
        
        public BoomCatalog()
        {
            InitializeComponent();
        }

        public Boom InternalBoom { get; set; }
    }
}
