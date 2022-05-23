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
using Dispatcher.Class;

using SQLLib;
using DataLib;

namespace Dispatcher.UCs
{
    /// <summary>
    /// Логика взаимодействия для UniversalDataGrid.xaml
    /// </summary>
    public partial class UniversalDataGrid : UserControl
    {
        //SQL query

        public string selectStr { get; set; }

        public string insertStr { get; set; }

        public string updateStr { get; set; }

        public string deleteStr { get; set; }

        //Переменные для SQL

        public string valuesNameStr { get; set; }

        public string[] valuesName { get; set; }

        public string[] values { get; set; }

        //Промежуточная таблица для программной работы с данными

        public DataTable dataTable;


        public UniversalDataGrid()
        {
            InitializeComponent();
        }

        private void MyDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_BeginningEdit");
        }

        private void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_CellEditEnding");
            //setValues();
            TraceValues();
        }

        private void MyDataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_PreparingCellForEdit");
        }

        private void MyDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_RowEditEnding");
            //Trace.WriteLine(DataClass.MyGetItemArray(MyDataGrid, 0));

            //Trace.WriteLine(((DataRowView)MyDataGrid.SelectedItems[0]).Row[0].ToString());

            //setValues();
            //TraceValues();

            /*
            int selectedColumn = MyDataGrid.CurrentCell.Column.DisplayIndex;
            var selectedCell = MyDataGrid.SelectedCells[selectedColumn];
            var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            if (cellContent is TextBlock)
            {
                Trace.WriteLine((cellContent as TextBlock).Text);
            }
            */
        }

        private void MyDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_AddingNewItem");
        }

        private int parserValueName()
        {
            try
            {
                valuesNameStr.Trim();
                valuesName = valuesNameStr.Split(", ");

                return 0;
            }
            catch(Exception ex)
            {
                App.logger.NewLog(400, "Ошибка в UniDG при парсинге " + ex.Message);
                MessageBox.Show(ex.Message);
                return 1;
            }
        }

        //Что за дичь?
        //private int setValues()
        //{
        //    try
        //    {
        //        List<string> valuesList = new List<string> { };
        //        for(int i = 0; i < MyDataGrid.Columns.Count();i++)
        //        {
        //            valuesList.Add(DataClass.MyGetItemArray(MyDataGrid, i));
        //        }

        //        values = valuesList.ToArray();

        //        return 0;
        //    }
        //    catch(Exception ex)
        //    {
        //        //Дай нормальное сообщение в Лог вот здесь v
        //        Log.NewLog(300, "Ошибка в UniDG при setValue" + ex.Message);
        //        MessageBox.Show(ex.Message);

        //        return 1;
        //    }
        //}

        private int TraceValues()
        {
            try
            {
                foreach (string value in values)
                    Trace.Write(value + " ");
                Trace.WriteLine("");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private int selectFun()
        {
            try
            {
                dataTable = SQL.ReturnDT(selectStr, App.configuration.SQLConnectionString, out string ex);
                if(ex == String.Empty)
                {
                    App.logger.NewLog(401, "Ошибка в UniDG при select " + ex);
                }
                MyDataGrid.ItemsSource = dataTable.DefaultView;

                return 0;
            }
            catch (Exception ex)
            {
                //TODO Добавь логи
                App.logger.NewLog(402, "Ошибка в UniDG при select " + ex.Message);
                MessageBox.Show(ex.Message);

                return 1;
            }
        }

        public int insertFun()
        {
            try
            {
                int ResultNoRuturn = SQL.NoReturn(insertStr.Trim().Replace("*", " "), App.configuration.SQLConnectionString, out string ex);
                if (ex == String.Empty)
                {
                    App.logger.NewLog(403, "Ошибка в UniDG при select " + ex);
                }
                return ResultNoRuturn;
            }
            catch (Exception ex)
            {
                App.logger.NewLog(404, "Ошибка в UniDG при insert" + ex.Message);
                MessageBox.Show(ex.Message);

                return 1;
            }
        }

        private void MyDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO Сделай асинхронным
            //TODO Нужно показывать что идёт загрузка
            selectFun();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            parserValueName();
        }
    }
}
