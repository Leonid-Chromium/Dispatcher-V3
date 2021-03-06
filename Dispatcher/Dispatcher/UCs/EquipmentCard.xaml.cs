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
	/// Логика взаимодействия для EquipmentCard.xaml
	/// </summary>
	public partial class EquipmentCard : UserControl
	{
		public EquipmentList parent { get; set; }

		public int ID { get; set; } = 0;
		public string name { get; set; } = "";
		public string district { get; set; } = "";
		public int statusId { get; set; } = 0;
		public string status { get; set; } = "";

		public EquipmentCard()
		{
			InitializeComponent();
		}

		public int update()
		{
			try
			{
				EquipmentNameL.Content = name;
				EquipmentDistrictL.Content = district;
				StatusL.Content = status;
				switch(statusId)
				{
					default:
						break;
				}
				return 0;
			}
			catch
			{
				return 1;
			}
		}

		private void InfoButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			Trace.WriteLine(ID);
			
			//TODO Название функции страдает

			parent.parent.ChrngeVisibilityEAI(ID);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			update();
		}
	}
}
