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
	/// Логика взаимодействия для EquipmentUC.xaml
	/// </summary>
	public partial class EquipmentUC : UserControl
	{
		public EquipmentUC()
		{
			InitializeComponent();
			EquipList.parent = this;
			EquipEAI.parent = this;
		}

		public void ChrngeVisibilityEAI(int id)
		{
			if (EquipList.Visibility == Visibility.Visible && EquipEAI.Visibility == Visibility.Collapsed)
			{
				EquipList.Visibility = Visibility.Collapsed;
				EquipEAI.equipID = id;
				EquipEAI.Visibility = Visibility.Visible;
			}
			else if (EquipList.Visibility == Visibility.Collapsed && EquipEAI.Visibility == Visibility.Visible)
			{
				EquipList.Visibility = Visibility.Visible;
				EquipEAI.Visibility = Visibility.Collapsed;
			}
		}
	}
}
