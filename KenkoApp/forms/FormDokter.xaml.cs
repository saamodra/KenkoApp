using MaterialDesignThemes.Wpf;
using KenkoApp.uc;
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
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace KenkoApp.forms
{
    /// <summary>
    /// Interaction logic for FormDokter.xaml
    /// </summary>
    public partial class FormDokter : Window
    {
        private int idDokter;

        public FormDokter()
        {
            InitializeComponent();
            btnSave.Click += btnSave_Click;
        }

        public FormDokter(string type, int idDokter)
        {
            InitializeComponent();
            if (type == "Edit")
            {
                this.idDokter = idDokter;
                lblTitle.Text = "Edit Data Dokter";
                lblSubtitle.Text = "Form ini untuk mengedit data dokter";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            }
            else
            {
                btnSave.Click += btnSave_Click;
            }
        }

        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAll())
            {
                MessageBox.Show("Data gagal disimpan", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_Dokter_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("no_sip", txtNoSip.Text);
                cmd.Parameters.AddWithValue("nama_dokter", txtNamaDokter.Text);
                cmd.Parameters.AddWithValue("jenis_kelamin", Kenko.getJenkel(rdLaki));
                cmd.Parameters.AddWithValue("spesialisasi", txtSpesialisasi.Text);
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);
                cmd.Parameters.AddWithValue("password", txtPassword.Password);


                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAll())
            {
                MessageBox.Show("Data gagal disimpan", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_Dokter_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("no_sip", txtNoSip.Text);
                cmd.Parameters.AddWithValue("nama_dokter", txtNamaDokter.Text);
                cmd.Parameters.AddWithValue("jenis_kelamin", Kenko.getJenkel(rdLaki));
                cmd.Parameters.AddWithValue("spesialisasi", txtSpesialisasi.Text);
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);
                cmd.Parameters.AddWithValue("password", txtPassword.Password);
                cmd.Parameters.AddWithValue("id_dokter", idDokter);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diupdate!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtNoSip_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtNamaDokter_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }
        private void jenkelToggle_Click(object sender, RoutedEventArgs e)
        {
            toggleJenkel((ToggleButton)sender);
            lblJenkel.Visibility = Visibility.Hidden;
            GridCursor.Margin = new Thickness(0, ((68 * 2) + 10), 0, 0);
        }

        private void txtSpesialisasi_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 3) + 8), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtNoTelp_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 4) + 7), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtAlamat_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Height = 100;
            GridCursor2.Margin = new Thickness(0, ((68 * 0)), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }


        private void txtEmail_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, ((68 * 1) + 77), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtPassword_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, ((68 * 2) + 77), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtRetypePass_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, ((68 * 3) + 77), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }


        private void txtAlamat_LostFocus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Height = 30;
        }

        private void txtNoTelp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private void txtNoSip_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNoSip.Text, lblNoSip);
        }

        private void txtNamaDokter_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Kenko.fieldRequired(txtNamaDokter.Text, lblNamaDokter);
        }

        private void txtSpesialisasi_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtSpesialisasi.Text, lblSpesialisasi);
        }

        private void txtAlamat_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
        }

        private void txtNoTelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldMin(txtNoTelp.Text, lblNoTelp, 11);
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtEmail.Text, lblEmail); 
        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRequired(txtPassword.Password, lblPassword);
        }

        private void txtRetypePass_TextChanged(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRetype(txtPassword.Password, txtRetypePass.Password, lblRetypePass);
        }

        private bool validateAll()
        {
            bool noSip = Kenko.fieldRequired(txtNoSip.Text, lblNoSip);
            bool namaDokter = Kenko.fieldRequired(txtNamaDokter.Text, lblNamaDokter);
            bool spesialisasi = Kenko.fieldRequired(txtSpesialisasi.Text, lblSpesialisasi);
            bool jenkel = Kenko.toggleRequired(lblJenkel, (bool)rdLaki.IsChecked, (bool)rdPerempuan.IsChecked);
            bool alamat = Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
            bool noTelp = Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
            bool email = Kenko.fieldRequired(txtEmail.Text, lblEmail);
            bool password = Kenko.fieldRequired(txtPassword.Password, lblPassword);
            bool retype = Kenko.fieldRetype(txtPassword.Password, txtRetypePass.Password, lblRetypePass);
            bool emailFormat = false;

            if (email)
            {
                emailFormat = Kenko.emailInput(txtEmail.Text, lblEmail);
            }
            if (noSip && namaDokter && spesialisasi && jenkel && alamat && noTelp && email && emailFormat && retype && password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void toggleJenkel(ToggleButton toggleButton)
        {
            rdLaki.IsChecked = false;
            rdPerempuan.IsChecked = false;

            toggleButton.IsChecked = true;
        }

    }
}
