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

namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для SettingsUC1.xaml
	/// </summary>
	public partial class SettingsUC1 : UserControl
	{
		public SettingsUC1()
		{
			InitializeComponent();
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mwin = (MainWindow)Window.GetWindow(this);
			mwin.MainWindowSpaceControle(mwin.Workspace);
		}
	}
}
