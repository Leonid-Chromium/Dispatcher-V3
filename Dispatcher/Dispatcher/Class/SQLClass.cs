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
    class SQLClass
    {
        public string StandartConString = "";

        public DataTable ReturnDT(string ConStr, string  Query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                Trace.WriteLine("ConString = " + ConStr);
                Trace.WriteLine("Query = " + Query);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query,ConStr);
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataTable;
        }

        public DataTable ReturnDT(string Query)
        {
            return ReturnDT(StandartConString, Query);
        }
    }
}
