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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KenkoApp.forms
{
    /// <summary>
    /// Interaction logic for FormSupplier.xaml
    /// </summary>
    public partial class FormSupplier : Window
    {
        private int idSupplier;

        public FormSupplier()
        {
            InitializeComponent();
            btnSave.Click += btnSave_Click;
        }

        public FormSupplier(string type, int idSupplier)
        {
            InitializeComponent();
            if (type == "Edit")
            {
                this.idSupplier = idSupplier;
                lblTitle.Text = "Edit Data Supplier";
                lblSubtitle.Text = "Form ini untuk mengedit data supplier";
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

                SqlCommand cmd = new SqlCommand("sp_Supplier_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("nama_supplier", txtNamaSupplier.Text);
                cmd.Parameters.AddWithValue("nama_kontak", txtNamaKontak.Text);
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("email", String.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                cmd.Parameters.AddWithValue("bank", txtBank.Text);
                cmd.Parameters.AddWithValue("no_rekening", txtNoRekening.Text);
                cmd.Parameters.AddWithValue("keterangan", String.IsNullOrEmpty(txtKeterangan.Text) ? (object)DBNull.Value : txtKeterangan.Text);

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
                MessageBox.Show("Data gagal diupdate", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_Supplier_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_supplier", idSupplier);
                cmd.Parameters.AddWithValue("nama_supplier", txtNamaSupplier.Text);
                cmd.Parameters.AddWithValue("nama_kontak", txtNamaKontak.Text);
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("email", String.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                cmd.Parameters.AddWithValue("bank", txtBank.Text);
                cmd.Parameters.AddWithValue("no_rekening", txtNoRekening.Text);
                cmd.Parameters.AddWithValue("keterangan", String.IsNullOrEmpty(txtKeterangan.Text) ? (object)DBNull.Value : txtKeterangan.Text);

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
                    MessageBox.Show("Data gagal diupdate : " + ex.Message);
                }
            }
        }

        private void txtNamaSupplier_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
            GridCursor1.Visibility = Visibility.Hidden;
        }

        private void txtNamaKontak_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);
            GridCursor1.Visibility = Visibility.Hidden;
        }

        private void txtAlamat_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Height = 53;
            GridCursor.Margin = new Thickness(0, ((68 * 2)), 0, 0);
            GridCursor1.Visibility = Visibility.Hidden;
        }

        private void txtNoTelp_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 3) + 27), 0, 0);
            GridCursor1.Visibility = Visibility.Hidden;
        }
        private void txtEmail_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor1.Visibility = Visibility.Visible;
            GridCursor1.Margin = new Thickness(0, (68 * 0), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtBank_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor1.Visibility = Visibility.Visible;
            GridCursor1.Margin = new Thickness(0, (68 * 1), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtNoRekening_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor1.Visibility = Visibility.Visible;
            GridCursor1.Margin = new Thickness(0, (68 * 2), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtKeterangan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor1.Visibility = Visibility.Visible;
            GridCursor1.Height = 53;
            GridCursor1.Margin = new Thickness(0, (68 * 3), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtNamaSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaSupplier.Text, lblNamaSupplier);
        }

        private void txtNamaKontak_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaKontak.Text, lblNamaKontak);
        }

        private void txtAlamat_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
        }

        private void txtNoTelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
        }

        private void txtBank_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtBank.Text, lblBank);
        }

        private void txtNoRekening_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNoRekening.Text, lblNoRekening);
        }

        private void txtKeterangan_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtKeterangan.Text, lblKeterangan);
        }

        private bool validateAll()
        {
            bool namaSupplier = Kenko.fieldRequired(txtNamaSupplier.Text, lblNamaSupplier);
            bool namaKontak = Kenko.fieldRequired(txtNamaKontak.Text, lblNamaKontak);
            bool alamat = Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
            bool noTelp = Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
            bool bank = Kenko.fieldRequired(txtBank.Text, lblBank);
            bool noRek = Kenko.fieldRequired(txtNoRekening.Text, lblNoRekening);

            if (namaSupplier && namaKontak && alamat && noTelp && bank && noRek)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            GridCursor.Height = 30;
            GridCursor1.Height = 30;
        }

        private void txtNoTelp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }
    }
}
