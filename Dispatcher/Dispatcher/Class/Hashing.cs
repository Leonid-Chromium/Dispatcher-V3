using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class Hashing
    {
        public static string GetMD5Hash(string text)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash =
                hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }
        /*

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string hash = Hashing.GetMD5Hash(passwordTB.Text);
            Trace.WriteLine(hash + " " + hash.Length);
            DataTable dataTable = SQLClass.ReturnDT("SELECT MyPassword FROM MyUser WHERE MyLogin = '" + loginTB.Text + "'");
            for(int i = 0; i < dataTable.Rows.Count; i++)
                if (dataTable.Rows[i].ItemArray[0].ToString() == hash)
                    MessageBox.Show("Salam");
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string hash = Hashing.GetMD5Hash(passwordTB.Text);
            Trace.WriteLine(hash + " " + hash.Length);
            int check = SQLClass.NoReturn("INSERT INTO MyUser(MyLogin, MyPassword) VALUES ('" + loginTB.Text + "', '" + hash + "')");
            if (check == 0)
                MessageBox.Show("Принимаем в семью");
        }

         */
    }
}
