using System;
using System.Collections.Generic;
using System.Data;
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
			AccessCheck();
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mwin = (MainWindow)Window.GetWindow(this);
			mwin.MainWindowSpaceControle(mwin.Settings);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}

		public void AccessCheck()
		{
			DataTable Access = SQLLib.SQL.ReturnDT(@"
SELECT
RoleUCNode.IdRole
, Roles.RoleName
, UCs.Name
FROM RoleUCNode
LEFT JOIN Roles on Roles.IdRole = RoleUCNode.IdRole
LEFT JOIN UCs on UCs.IdUC = RoleUCNode.IdUC
WHERE RoleUCNode.IdRole = " + App.role,
App.configuration.SQLConnectionString, out string ex);
			if (ex != string.Empty)
				MessageBox.Show(ex);

			Trace.WriteLine("MyTabControle.Items.Count-1 = " + (MyTabControle.Items.Count - 1));
			for (int i = (MyTabControle.Items.Count-1); i>=0; i--)
			{
				Trace.WriteLine("i = " + i);
				TabItem tabItem = (TabItem)MyTabControle.Items[i];
				Trace.WriteLine("-----------");
				Trace.WriteLine(tabItem.Name);
				Trace.WriteLine(tabItem.Content);

				bool needDeleteItem = true;

				foreach (DataRow dataRow in Access.Rows)
				{
					Trace.WriteLine(dataRow.ItemArray[2].ToString());
					if (tabItem.Name.Trim() == dataRow.ItemArray[2].ToString().Trim())
					{
						Trace.WriteLine("ДА");
						needDeleteItem = false;
						break;
					}
				}

				if(needDeleteItem)
					MyTabControle.Items.Remove(tabItem);

			}
		}

        private void BtnNewAuth_Click(object sender, RoutedEventArgs e)
        {
			App.OpenAuthorizationWindow();
        }
    }
}
