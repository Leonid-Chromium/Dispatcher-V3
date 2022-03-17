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
	/// Логика взаимодействия для EquipmentEditAndInfo.xaml
	/// </summary>
	public partial class EquipmentEditAndInfo : UserControl
	{
		public EquipmentUC parent { get; set; }
		public int equipID { get; set; }

		public EquipmentEditAndInfo()
		{
			InitializeComponent();
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			parent.ChrngeVisibilityEAI(0);
		}

		private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			EditItemC.UpdateInfo(equipID);
		}
	}
}
