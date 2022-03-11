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

namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для WorkSpaceUC1.xaml
	/// </summary>
	public partial class WorkSpaceUC1 : UserControl
	{
		public WorkSpaceUC1()
		{
			InitializeComponent();
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mwin = (MainWindow)Window.GetWindow(this);
			mwin.MainWindowSpaceControle(mwin.Settings);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
		//	Trace.WriteLine(MyTabControle.GetType());
		//	foreach(TabItem testvar in MyTabControle.Items)
		//	{
		//		Trace.WriteLine("----------------------------");
		//		Trace.WriteLine(testvar.Name);
		//		Trace.WriteLine(testvar.Header);
		//		Trace.WriteLine("----------------------------");
		//	}
		}
	}
}
