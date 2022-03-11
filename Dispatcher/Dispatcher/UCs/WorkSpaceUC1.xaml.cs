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
			DataTable Access = SQLLib.SQL.ReturnDT(@"SELECT
--RoleUCNode.IdNode
RoleUCNode.IdRole
--, RoleUCNode.IdUC
, Roles.RoleName
--, Roles.RolePassword
, UCs.Name
--, UCs.Note
FROM RoleUCNode
LEFT JOIN Roles on Roles.IdRole = RoleUCNode.IdRole
LEFT JOIN UCs on UCs.IdUC = RoleUCNode.IdUC

WHERE RoleUCNode.IdRole = " + App.role, App.configuration.SQLConnectionString, out string ex);
			if (ex != string.Empty)
				MessageBox.Show(ex);

			
			TabItem myTabControle = this.SuperTab;
			myTabControle.Visibility = Visibility.Collapsed;




			foreach (TabItem tabItem in MyTabControle.Items)
			{
				Trace.WriteLine("-----------");
				Trace.WriteLine(tabItem.Name);
				Trace.WriteLine(tabItem.Content);
				//tabItem.Visibility = Visibility.Collapsed;
				foreach (DataRow dataRow in Access.Rows)
				{
					Trace.WriteLine(dataRow.ItemArray[2].ToString());
					if (tabItem.Name.Trim() == dataRow.ItemArray[2].ToString().Trim())
					{
						Trace.WriteLine("ДА");
						tabItem.Visibility = Visibility.Visible;
						break;
					}
				}
			}
		}

        public void MyButtonPenis_Click(object sender, RoutedEventArgs e)
        {
			App.OpenAuthorizationWindow();
		}
    }
}
