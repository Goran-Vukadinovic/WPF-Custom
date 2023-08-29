using EntryPoint.ViewModel;
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

namespace EntryPoint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsChildOfControl(e.Source as DependencyObject, MyToolBarControl))
            {
                MyToolBarControl.popup.IsOpen = false;
                MyToolBarControl.popupBacklinks.IsOpen = false;
            }
        }

        private bool IsChildOfControl(DependencyObject element, DependencyObject control)
        {
            if (element == null)
                return false;

            if (element == control)
                return true;

            return IsChildOfControl(VisualTreeHelper.GetParent(element), control);
        }
    }
}
