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
	/// Логика взаимодействия для TimeSpanUC.xaml
	/// </summary>
	public partial class TimeSpanUC : UserControl
	{
		public TimeSpanUC()
		{
			InitializeComponent();
		}

		private void DoItButton_Click(object sender, RoutedEventArgs e)
		{
			DateTime dateTime1 = Convert.ToDateTime(DTTB1.Text);
			DateTime dateTime2 = Convert.ToDateTime(DTTB2.Text);

			TimeSpan timeSpan = dateTime2 - dateTime1;
			OutTB.Text = timeSpan.ToString();
		}
	}
}