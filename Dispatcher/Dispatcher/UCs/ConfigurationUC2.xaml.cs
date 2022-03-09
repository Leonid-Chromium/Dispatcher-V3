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

using System.Diagnostics;
using Dispatcher.Class;


namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для ConfigurationUC2.xaml
	/// </summary>
	public partial class ConfigurationUC2 : UserControl
	{
		public int updateInfoAboutConfig()
		{
			try
			{
				ConfigurationCB.ItemsSource = ConfigManage.GetAllConfigurationName();
				ConfigurationCB.SelectedItem = App.configuration.name;
				SQLConnectionStringTB.Text = App.configuration.SQLConnectionString;
				serverIpTB.Text = App.configuration.serverIp;
				serverPortTB.Text = App.configuration.serverPort;
				netPathTB.Text = App.configuration.netPath;
				districtTB.Text = App.configuration.district;
				roomTB.Text = App.configuration.room;
				themePathTB.Text = App.configuration.themePath;
				themeTB.Text = App.configuration.theme;
				assetsPathTB.Text = App.configuration.assetsPath;
				logPathTB.Text = App.configuration.logPath;

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		public ConfigurationUC2()
		{
			InitializeComponent();
		}

		private void ConfigurationCB_DropDownOpened(object sender, EventArgs e)
		{
			updateInfoAboutConfig();
		}

		private void ConfigurationCB_DropDownClosed(object sender, EventArgs e)
		{
			//TODO Сделай защиту от пустого выбора
			Trace.WriteLine(ConfigurationCB.SelectedItem.ToString());
			App.configuration = ConfigManage.GetConfiguration(ConfigurationCB.SelectedItem.ToString());
			ConfigManage.SetSavedConfiguration(ConfigurationCB.SelectedItem.ToString());
			updateInfoAboutConfig();
		}

		private int DeleteConfiguration2()
		{
			try
			{
				Trace.WriteLine("Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " собираются удалить");

				MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить крнфигурацию " + App.configuration.name + ".\nЭта операция необратима",
					"Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
				Trace.WriteLine("result = " + result);

				if (result == MessageBoxResult.Yes)
				{
					//DEBUG Сделать удаление конфигурации
					ConfigManage.DeleteConfiguration(App.configuration.name);
					App.configuration = ConfigManage.GetConfiguration(ConfigManage.GetAllConfigurationName().First());
					Trace.WriteLine("Конфигурация удалена");
					Log.NewLog(101, "Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " удалили");
				}
				else
				{
					Trace.WriteLine("Отмена удаления");
				}
				updateInfoAboutConfig();

				return 0;
			}
			catch
			{
				return 1;
			}
		}

		private void delConfButton_Click(object sender, RoutedEventArgs e)
		{
			DeleteConfiguration2();
		}

		private void SaveConfiguration()
		{
			try
			{
				Trace.WriteLine("Готовимся изменять конфигурацию");

				MessageBoxResult result = MessageBox.Show("Вы уверены что хотите изменить крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
				Trace.WriteLine("result = " + result);

				if (result == MessageBoxResult.Yes)
				{
					Configuration newConfiguration = new Configuration();
					newConfiguration.name = ConfigurationCB.Text.ToString();
					newConfiguration.themePath = themePathTB.Text;
					newConfiguration.theme = themeTB.Text;
					newConfiguration.assetsPath = assetsPathTB.Text;
					newConfiguration.logPath = logPathTB.Text;
					newConfiguration.SQLConnectionString = SQLConnectionStringTB.Text;
					newConfiguration.serverIp = serverIpTB.Text;
					newConfiguration.serverPort = serverPortTB.Text;
					newConfiguration.netPath = netPathTB.Text;
					newConfiguration.district = districtTB.Text;
					newConfiguration.room = roomTB.Text;

					// Проверка на неповторяемость имён конфигурации
					// WARNING Очень криво и что то меняем
					while (ConfigManage.HaveName(newConfiguration.name) && (newConfiguration.name != App.configuration.name))
						newConfiguration.name = String.Concat(newConfiguration.name, " Дублёр");

					Trace.WriteLine(App.configuration.name);
					newConfiguration.TraceConfiguration();
					ConfigManage.ChangeConfiguration(App.configuration.name, newConfiguration); //Изменяем конфигурацию в файле конфигурации
					App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
					Log.NewLog(001, "Конфигурацию " + App.configuration.name + " изменили" + ((newConfiguration.name != App.configuration.name) ? (" на " + newConfiguration.name) : "")); // Это тернарный оператор. Я не хочу об этом говорить
				}
				updateInfoAboutConfig();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message);
				Log.NewLog(301, ex.Message);
			}
		}

		private void saveConfButton_Click(object sender, RoutedEventArgs e)
		{
			SaveConfiguration();
		}

		private void AddConfiguration()
		{
			try
			{
				Trace.WriteLine("Делаем новую конфигурацию");

				MessageBoxResult result = MessageBox.Show("Вы уверены что хотите добавить новую крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
				Trace.WriteLine("result = " + result);

				if (result == MessageBoxResult.Yes)
				{
					Configuration newConfiguration = new Configuration();
					newConfiguration = App.configuration;
					//Вот сюда проверку впихни
					//TODO Нужно зациклить
					if (App.configuration.name == ConfigurationCB.Text.ToString())
						newConfiguration.name = String.Concat(newConfiguration.name, " Дубль");
					else
						newConfiguration.name = ConfigurationCB.Text.ToString();

					ConfigManage.SetConfiguration(newConfiguration); //добавляем новую конфигурацию
					App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
					Log.NewLog(101, "Добавили новую конфигурацию " + App.configuration.name);
				}
				updateInfoAboutConfig();
			}
			catch (Exception ex)
			{
				Log.NewLog(301, "Ошибка при добавлении конфигурации " + ex.Message);
			}
		}

		private void addConfButton_Click(object sender, RoutedEventArgs e)
		{
			AddConfiguration();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			updateInfoAboutConfig();
		}
	}
}
