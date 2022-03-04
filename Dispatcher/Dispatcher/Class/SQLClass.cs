using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dispatcher.Class
{
    //class SQLClass
    //{
    //    public static string standartConString = @"Data Source=192.168.1.118,1433\SQLEXPRESS;Initial Catalog=NewDispatcher;Persist Security Info=True;User ID=Admin;Password=Admin";

    //    public static DataTable ReturnDT(string Query, string ConStr)
    //    {
    //        DataTable dataTable = new DataTable();

    //        try
    //        {
    //            Trace.WriteLine("ConString = " + ConStr);
    //            Trace.WriteLine("Query = " + Query);

    //            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query, ConStr);
    //            sqlDataAdapter.Fill(dataTable);
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.Message);
    //        }
    //        return dataTable;
    //    }

    //    public static DataTable ReturnDT(string Query)
    //    {
    //        return ReturnDT(Query, standartConString);
    //    }

    //    public static int NoReturn(string Query, string ConStr)
    //    {
    //        try
    //        {
    //            Trace.WriteLine("Connection sring = " + ConStr);
    //            Trace.WriteLine("Query = " + Query);

    //            using (SqlConnection connection = new SqlConnection(ConStr))
    //            {
    //                connection.Open();
    //                SqlCommand command = new SqlCommand(Query, connection);
    //                command.ExecuteNonQuery(); //Выполнение запроса без возращения данных
    //                connection.Close();
    //            }
    //            return 0;
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.Message);
    //            return 1;
    //        }
    //    }

    //    public static int NoReturn(string Query)
    //    {
    //        try
    //        {
    //            return NoReturn(Query, standartConString);
    //        }
    //        catch
    //        {
    //            return 1;
    //        }
    //    }

    //    public static string ArrayToValue(string[] valueName, string[] value) // Используется для формирования строки-перечисления пар ([название столбца] = [значение])
    //    {
    //        List<string> valueСollection = new List<string>();
    //        for (int i = 0; i < valueName.Length; i++)
    //            valueСollection.Add(String.Concat(valueName[i], "=", value[i]));
    //        string resultStr = String.Join(", ", valueСollection);
    //        Trace.WriteLine(resultStr);
    //        return resultStr;
    //    }
    //}
}
