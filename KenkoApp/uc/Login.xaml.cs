using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            LoginUser();

        }

        private void LoginUser()
        {
            string sp = "";
            string role = "";
            int iNama = 0;

            if (rdDokter.IsChecked == true)
            {
                sp = "sp_Dokter_Login";
                role = "Dokter";
                iNama = 2;
            }
            else
            {
                sp = "sp_User_Login";
                role = "User";
                iNama = 1;
            }


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtUsername.Text;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPassword.Password;

                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    MessageBox.Show("Login berhasil!", "Berhasil", MessageBoxButton.OK, MessageBoxImage.Information);

                    Application.Current.Properties["id"] = data.GetValue(0).ToString();
                    Application.Current.Properties["nama"] = data.GetValue(iNama).ToString();
                    if (role == "User")
                    {
                        switch(data.GetValue(5).ToString())
                        {
                            case "1":
                                Application.Current.Properties["role"] = "Admin";
                                Content = new Admin();
                                break;
                            case "2":
                                Application.Current.Properties["role"] = "Kasir";
                                Content = new Kasir();
                                break;
                            case "3":
                                Application.Current.Properties["role"] = "Manager";
                                Content = new Manager();
                                break;
                        }
                    } else
                    {
                        Application.Current.Properties["role"] = role;
                        Content = new Dokter();

                    }


                }
                else
                {
                    MessageBox.Show("Username atau password salah!", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                conn.Close();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtUsername_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtUsername.Focus();
            }
        }

        private void txtPassword_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginUser();
            }
        }
    }
}
