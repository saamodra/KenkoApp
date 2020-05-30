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
                new FormDokter();
            }
        }

        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                cmd.Parameters.AddWithValue("spesialisasi", txtSpesialisasi.Text);
                cmd.Parameters.AddWithValue("kota", txtAlamat.Text);
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    this.Close();

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
                cmd.Parameters.AddWithValue("spesialisasi", txtSpesialisasi.Text);
                cmd.Parameters.AddWithValue("kota", txtAlamat.Text);
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diupdate!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtNoSip_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
        }

        private void txtNamaDokter_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);
        }

        private void txtSpesialisasi_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 2)), 0, 0);
        }

        private void txtAlamat_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Height = 53;
            GridCursor.Margin = new Thickness(0, ((68 * 3)), 0, 0);
        }

        private void txtNoTelp_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 4) + 27), 0, 0);
        }

        private void txtEmail_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 5) + 27), 0, 0);
        }

        private void txtAlamat_LostFocus(object sender, RoutedEventArgs e)
        {
            GridCursor.Height = 30;
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
            Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtEmail.Text, lblEmail); 
        }

        private bool validateAll()
        {
            bool noSip = Kenko.fieldRequired(txtNoSip.Text, lblNoSip);
            bool namaDokter = Kenko.fieldRequired(txtNamaDokter.Text, lblNamaDokter);
            bool spesialisasi = Kenko.fieldRequired(txtSpesialisasi.Text, lblSpesialisasi);
            bool alamat = Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
            bool noTelp = Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
            bool email = Kenko.fieldRequired(txtEmail.Text, lblEmail);
            bool emailFormat = false;
            if (email)
            {
                emailFormat = Kenko.emailInput(txtEmail.Text, lblEmail);
            }
            if (noSip && namaDokter && spesialisasi && alamat && noTelp && email && emailFormat)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
