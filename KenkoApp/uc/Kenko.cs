﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;

namespace KenkoApp.uc
{
    public class Kenko
    {
        
        public static DataTable getData(string sp, string cari = "")
        {
            DataTable dt;

            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand(sp, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cari", cari);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                DataColumn Col = dt.Columns.Add("No", typeof(System.Int32));
                Col.SetOrdinal(0);// to put the column in position 0;
                int a = 1;


                foreach (DataRow r in dt.Rows)
                {
                    r["No"] = a;
                    a++;
                }

            }
            catch
            {
                MessageBox.Show("db error");
                dt = null;
            }

            return dt;
        }

        public static DataTable getDataQuery(string query)
        {
            DataTable dt;

            try
            {
                SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);
                thisConnection.Open();

                string Get_Data = query;

                SqlCommand cmd = thisConnection.CreateCommand();
                cmd.CommandText = Get_Data;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                DataColumn Col = dt.Columns.Add("No", typeof(System.Int32));
                Col.SetOrdinal(0);// to put the column in position 0;
                int a = 1;
                foreach (DataRow r in dt.Rows)
                {
                    r["No"] = a;
                    a++;
                }

            }
            catch
            {
                MessageBox.Show("db error");
                dt = null;
            }

            return dt;
        }

        public static string FormatLocalDate(string date)
        {
            string[] tgl = date.Split('/');
            string newDate = tgl[2] + tgl[1] + tgl[0];

            return newDate;
        }

        public static string uploadImage(string url, string id = "")
        {
            string startupPath = Environment.CurrentDirectory;
            string extension = Path.GetExtension(url);
            string newFilename, currentImage = "";

            if (id == "")
            {
                newFilename = DateTime.Now.ToString("yyyyMMddHHmmss") + getLastUserId() + extension;
            } else
            {
                newFilename = DateTime.Now.ToString("yyyyMMddHHmmss") + String.Format("{0:0000}", id) + extension;
                currentImage = getImage(Convert.ToInt32(id));
            }

            string newFilePath = Directory.GetParent(startupPath).Parent.FullName + "\\images";

            if (File.Exists(newFilePath + "\\" + currentImage) && currentImage != "default.jpg")
            {
                File.Delete(newFilePath + "\\" + currentImage);
            }


            File.Copy(url, newFilePath + "\\" + newFilename);

            return newFilename;
        }

        public static string generateIdTable(string table, string kode)
        {
            string id, idtable;

            id = kode;
            idtable = "0001";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + table;


                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    idtable = data.GetValue(0).ToString();
                    int lastNumber = Convert.ToInt32(idtable.Remove(0, 2));
                    lastNumber++;

                    idtable = String.Format("{0:0000}", lastNumber);
                }

                conn.Close();
            }

            return id + idtable;
        }

        public static string generateMemberId()
        {
            string id, idMember;

            id = DateTime.Now.ToString("yyyyMMdd");
            idMember = "0001";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_Member_GetLast", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cari", id);

               
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    idMember = data.GetValue(0).ToString();
                    int lastNumber = Convert.ToInt32(idMember.Remove(0, 8));
                    lastNumber++;

                    idMember = String.Format("{0:0000}", lastNumber);
                }
                
                conn.Close();
            }

            return id + idMember;
        }

        public static string generateId(string type, string sp)
        {
            string id, idtype;

            id = type + DateTime.Now.ToString("yyyyMMdd");
            idtype = "0001";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cari", id);


                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    idtype = data.GetValue(0).ToString();
                    int lastNumber = Convert.ToInt32(idtype.Remove(0, 10));

                    lastNumber++;
                    idtype = String.Format("{0:0000}", lastNumber);
                }

                conn.Close();
            }



            return id + idtype;
        }


        public static string getAntrian()
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_Transaksi_Reservasi_GetCount", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    count = int.Parse(data.GetValue(0).ToString());
                    count++;
                }

                conn.Close();
            }


            return count.ToString();
        }


        public static string getLastUserId()
        {
            string idUser;

            idUser = "0001";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                String query = "SELECT TOP(1) id_user FROM Tabel_User ORDER BY id_user DESC";
                using (SqlCommand SDA = new SqlCommand(query, conn))
                {
                    SqlDataReader data = SDA.ExecuteReader();
                    if (data.Read())
                    {
                        idUser = data.GetValue(0).ToString();
                        int lastNumber = Convert.ToInt32(idUser);
                        idUser = String.Format("{0:0000}", (lastNumber + 1));
                    }
                }
                conn.Close();
            }

            return idUser;
        }

        public static string getImage(int idUser)
        {
            string imageName = "";


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                String query = "SELECT TOP(1) foto FROM Tabel_User WHERE id_user = " + idUser;
                using (SqlCommand SDA = new SqlCommand(query, conn))
                {
                    SqlDataReader data = SDA.ExecuteReader();
                    if (data.Read())
                    {
                        imageName = data.GetValue(0).ToString();
                    }
                }
                conn.Close();
            }

            return imageName;
        }

        public static string formatCurrency(double text)
        {
            
            return text.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"));
        }

        public static string getNumber1(string currency)
        {
            string resultString = Regex.Match(currency, @"\d+").Value;
            return resultString;
        }

        public static string getNumber(string text)
        {
            string numer = string.Empty;
            foreach (char str in text)
            {
                if (char.IsDigit(str))
                {
                    numer += str.ToString();
                }

            }

            return numer.Remove(numer.Length - 3, 2);
        }

        public static string getNumber2(string text)
        {
            string numer = string.Empty;
            foreach (char str in text)
            {
                if (char.IsDigit(str))
                {
                    numer += str.ToString();
                }

            }

            return numer;
        }

        public static void textFieldCurrencyFormat(TextBox txtBox, string symbol)
        {
            string numberOnly = getNumber2(txtBox.Text == symbol || txtBox.Text == "" ? symbol + "0" : txtBox.Text);

            double text = Double.Parse(numberOnly);

            String formattedNumber = String.Format(symbol+"{0:n0}", text);

            txtBox.Text = formattedNumber.Replace(",", ".");
            txtBox.SelectionStart = txtBox.Text.Length;
            txtBox.SelectionLength = 0;
        }

        //Validation
        public static bool fieldRequired(string text, Label label)
        {
            bool valid = false;
            if (string.IsNullOrEmpty(text))
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Wajib diisi.";
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                valid = true;
            }

            return valid;
        }

        public static bool fieldMin(string text, Label label, int min)
        {
            bool valid = false;
            if (string.IsNullOrEmpty(text))
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Wajib diisi.";
            } else if(text.Length < min)
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Minimal " + min + " karakter";
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                valid = true;
            }

            return valid;
        }

        public static bool dateRequired(DatePicker datePicker, Label label)
        {
            bool valid = false;
            if (datePicker.Text == "")
            {
                label.Visibility = Visibility.Visible;
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                valid = true;
            }

            return valid;
        }

        public static bool fieldRetype(string text, string text2, Label label)
        {
            bool valid = false;
            if(string.IsNullOrEmpty(text2))
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Wajib diisi.";
            } else if (text != text2)
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Password tidak cocok.";
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                valid = true;
            }

            return valid;
        }

        public static bool comboRequired(ComboBox comboBox, Label label)
        {
            bool valid = false;
            if (comboBox.SelectedIndex < 0)
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Wajib diisi.";
            }
            else
            {
                label.Visibility = Visibility.Hidden;
                valid = true;
            }

            return valid;
        }

        public static bool emailInput(string text, Label label)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            if (regex.IsMatch(text))
            {
                label.Visibility = Visibility.Hidden;
                return true;
            }
            else
            {
                label.Visibility = Visibility.Visible;
                label.Content = "Format email salah.";
                return false;
            }

        }



        public static bool toggleRequired(Label label, params bool[] arguments)
        {
            bool result = false;

            foreach(bool r in arguments)
            {
                if(r)
                {
                    result = true;
                }
            }

            if (!result)
            {
                label.Visibility = Visibility.Visible;
            }
            else
            {
                label.Visibility = Visibility.Hidden;
            }

            return result;
        }

        public static bool numberOnlyInput(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            return e.Handled;
        }

        public static void alphabetOnlyInput(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static string getJenkel(ToggleButton rdlaki)
        {
            string jenkel;

            if (rdlaki.IsChecked == true)
            {
                jenkel = "Laki-Laki";
            }
            else
            {
                jenkel = "Perempuan";
            }

            return jenkel;
        }
    }
}
