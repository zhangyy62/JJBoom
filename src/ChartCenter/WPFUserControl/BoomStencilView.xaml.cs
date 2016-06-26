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
using ChartCenter.WPFViewModel;

namespace ChartCenter.WPFUserControl
{
    /// <summary>
    /// BoomStencil.xaml 的交互逻辑
    /// </summary>
    public partial class BoomStencilView : UserControl
    {
        public BoomStencilView()
        {
            InitializeComponent();
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                textBox.IsEnabled = false;
                e.Handled = true;
            }
            e.Handled = false;
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.IsEnabled = false;
            e.Handled = true;
        }


        private void UIElement_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if ((bool)e.NewValue)
            {
                textBox.Focus();

                textBox.SelectAll();
                textBox.Background = Brushes.White;
            }
            textBox.Background = Brushes.Transparent;
        }
    }
}
