using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Dispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Trace.WriteLine("Выбрана роль: MainWindow\t" + App.role);
            InitializeComponent();
        }

        UserControl ActualSpace;
        public void MainWindowSpaceControle(UserControl userControl)
        {
            if (ActualSpace == null)
			{
				ActualSpace = (UserControl)Workspace;
				//userControl.Visibility = Visibility.Visible;
			}
            if (userControl == ActualSpace && ActualSpace.Visibility != Visibility.Collapsed)
            {
                userControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (ActualSpace != null)
                    ActualSpace.Visibility = Visibility.Collapsed;
                userControl.Visibility = Visibility.Visible;
            }
            ActualSpace = userControl;
        }
    }
}
