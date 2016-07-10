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

namespace JJBoom
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
                BoomStencilViewModel stencilViewModel = this.DataContext as BoomStencilViewModel;
                stencilViewModel.RenameStencilNameVisibility = Visibility.Collapsed;
                stencilViewModel.DisPlayStencilNameVisibility = Visibility.Visible;

            }
            e.Handled = false;
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            BoomStencilViewModel stencilViewModel = this.DataContext as BoomStencilViewModel;
            stencilViewModel.RenameStencilNameVisibility = Visibility.Collapsed;
            stencilViewModel.DisPlayStencilNameVisibility = Visibility.Visible;
            e.Handled = false;
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            BoomStencilViewModel stencilViewModel = this.DataContext as BoomStencilViewModel;
            stencilViewModel.FocusAndSelectAll = FocusAndSelectAll;
            e.Handled = false;
        }

        private void FocusAndSelectAll()
        {
            Console.WriteLine(Parent);
            RenameStencilTextbox.SelectAll();
            RenameStencilTextbox.Focus();
        }
    }
}
