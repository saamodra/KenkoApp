using MaterialDesignThemes.Wpf.Transitions;
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
            this.Content = new Admin();

            //if (txtUsername.Text == "admin" && txtPassword.Password == "admin")
            //{
            //    MessageBox.Show("Login Berhasil", "Login berhasil", MessageBoxButton.OK, MessageBoxImage.Information);
            //    this.Content = new Dashboard();
            //}
            //else
            //{
            //    MessageBox.Show("Login gagal", "Login gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
