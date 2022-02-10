using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class DataClass
    {
        public static int DTtoTrace(DataTable dataTable)
        {
            try
            {
                Trace.WriteLine("");
                Trace.WriteLine("Общая информация");
                Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
                Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Trace.Write("|");
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Trace.Write(String.Format("{0,3}", dataTable.Rows[i].ItemArray[j].ToString()));
                        Trace.Write("|");
                    }
                    Trace.WriteLine("");
                }

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    Trace.WriteLine(String.Format(dataTable.Columns[i].ColumnName + " " + dataTable.Columns[i].DataType));
                }
                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
