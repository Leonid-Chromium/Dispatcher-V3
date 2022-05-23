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
		/// <summary>
		/// Обновление информации о конфигурации
		/// </summary>
		/// <returns></returns>
		// TODO Раздели на отдельные методы
		public int UpdateInfoAboutConfig()
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


				App.logger.NewLog(100, "Узналим информацию о конфигурации " + App.configuration.name);

				return 0;
			}
			catch (Exception ex)
			{
				App.logger.NewLog(400, "ConfigurationUC2.UpdateInfoAboutConfig " + ex.Message);
				return 1;
			}
		}

		public ConfigurationUC2()
		{
			InitializeComponent();
		}

		private void ConfigurationCB_DropDownOpened(object sender, EventArgs e)
		{
			UpdateInfoAboutConfig();
		}

		private void ConfigurationCB_DropDownClosed(object sender, EventArgs e)
		{
			//TODO Сделай защиту от пустого выбора
			Trace.WriteLine(ConfigurationCB.SelectedItem.ToString());
			App.configuration = ConfigManage.GetConfiguration(ConfigurationCB.SelectedItem.ToString());
			App.logger.NewLog(200, "Используется конфигурация " + App.configuration.name);
			ConfigManage.SetSavedConfiguration(ConfigurationCB.SelectedItem.ToString());
			UpdateInfoAboutConfig();
		}

		/// <summary>
		/// Удаляет выбранную конфигурацию
		/// </summary>
		/// <returns></returns>
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
					ConfigManage.DeleteConfiguration(App.configuration.name);
					App.configuration = ConfigManage.GetConfiguration(ConfigManage.GetAllConfigurationName().First());
					Trace.WriteLine("Конфигурация удалена");
					App.logger.NewLog(201, "Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " удалили");
				}
				else
				{
					Trace.WriteLine("Отмена удаления");
				}
				UpdateInfoAboutConfig();

				return 0;
			}
			catch (Exception ex)
			{
				App.logger.NewLog(401, "ConfigurationUC2.DeleteConfiguration2 " + ex.Message);
				return 1;
			}
		}

		private void delConfButton_Click(object sender, RoutedEventArgs e)
		{
			DeleteConfiguration2();
		}

		/// <summary>
		/// Сохраняет изменения конфигурации
		/// </summary>
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
					App.logger.NewLog(202, "Конфигурацию " + App.configuration.name + " изменили" + ((newConfiguration.name != App.configuration.name) ? (" на " + newConfiguration.name) : "")); // Это тернарный оператор. Я не хочу об этом говорить
				}
				UpdateInfoAboutConfig();
			}
			catch (Exception ex)
			{
				App.logger.NewLog(402, ex.Message);
				Trace.WriteLine(ex.Message);
			}
		}

		private void saveConfButton_Click(object sender, RoutedEventArgs e)
		{
			SaveConfiguration();
		}

		/// <summary>
		/// Добавляет новую конфигурацию с указанным в ComboBox-е названием
		/// </summary>
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
					App.logger.NewLog(203, "Добавили новую конфигурацию " + App.configuration.name);
				}
				UpdateInfoAboutConfig();
			}
			catch (Exception ex)
			{
				App.logger.NewLog(403, "Ошибка при добавлении конфигурации " + ex.Message);
			}
		}

		private void addConfButton_Click(object sender, RoutedEventArgs e)
		{
			AddConfiguration();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateInfoAboutConfig();
		}
	}
}
