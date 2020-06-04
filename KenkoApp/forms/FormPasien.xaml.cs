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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KenkoApp.forms
{
    /// <summary>
    /// Interaction logic for FormPasien.xaml
    /// </summary>
    public partial class FormPasien : Window
    {

        private int idPasien;

        public FormPasien()
        {
            InitializeComponent();
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglLahir.Language = lang;
            txtTglLahir.Language = lang;
            btnSave.Click += btnSave_Click;
        }

        public FormPasien(string type, int idPasien)
        {
            InitializeComponent();
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglLahir.Language = lang;
            txtTglLahir.Language = lang;

            if (type == "Edit")
            {
                this.idPasien = idPasien;
                lblTitle.Text = "Edit Data Pasien";
                lblSubtitle.Text = "Form ini untuk mengedit data pasien";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            }
            else
            {
                new FormPasien();
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

                SqlCommand cmd = new SqlCommand("sp_Pasien_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("nama_pasien", txtNamaPasien.Text);
                cmd.Parameters.AddWithValue("jenis_kelamin", Kenko.getJenkel(rdLaki));
                cmd.Parameters.AddWithValue("tgl_lahir", Kenko.FormatLocalDate(txtTglLahir.Text));
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("golongan_darah", getGol());
                cmd.Parameters.AddWithValue("pekerjaan", String.IsNullOrEmpty(txtPekerjaan.Text) ? DBNull.Value : (object)txtPekerjaan.Text);

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

                SqlCommand cmd = new SqlCommand("sp_Pasien_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_pasien", idPasien);
                cmd.Parameters.AddWithValue("nama_pasien", txtNamaPasien.Text);
                cmd.Parameters.AddWithValue("jenis_kelamin", Kenko.getJenkel(rdLaki));
                cmd.Parameters.AddWithValue("tgl_lahir", Kenko.FormatLocalDate(txtTglLahir.Text));
                cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("golongan_darah", getGol());
                cmd.Parameters.AddWithValue("pekerjaan", String.IsNullOrEmpty(txtPekerjaan.Text) ? DBNull.Value : (object)txtPekerjaan.Text);

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

        private void txtNamaPasien_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
        }

        private void rdJenkel_Click(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);

        }

        private void txtTglLahir_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 2)), 0, 0);
        }

        private void txtAlamat_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Height = 60;
            GridCursor.Margin = new Thickness(0, ((68 * 3)), 0, 0);
        }

        private void txtNoTelp_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 4) + 30), 0, 0);
        }

        private void txtPekerjaan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 6) + 59), 0, 0);
        }

        private void txtAlamat_LostFocus(object sender, RoutedEventArgs e)
        {
            GridCursor.Height = 30;
        }

        private void txtNamaPasien_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaPasien.Text, lblNamaPasien);
        }

        private void txtAlamat_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
        }

        private void txtNoTelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
        }

        private void txtPekerjaan_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtPekerjaan.Text, lblPekerjaan);
        }

        private void txtTglLahir_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.dateRequired(txtTglLahir, lblTgl);
        }

        private bool validateAll()
        {
            bool namaPasien = Kenko.fieldRequired(txtNamaPasien.Text, lblNamaPasien);
            bool alamat = Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
            bool noTelp = Kenko.fieldRequired(txtNoTelp.Text, lblNoTelp);
            bool tglLahir = Kenko.dateRequired(txtTglLahir, lblTgl);
            bool jenkel = Kenko.toggleRequired(lblJenkel, (bool)rdLaki.IsChecked, (bool)rdPerempuan.IsChecked);
            bool goldar = Kenko.toggleRequired(lblGolonganDarah, (bool)golA.IsChecked, (bool)golB.IsChecked, (bool)golAB.IsChecked, (bool)golO.IsChecked);

            if (namaPasien && alamat && noTelp && tglLahir && jenkel && goldar)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtNoTelp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private void jenkelToggle_Click(object sender, RoutedEventArgs e)
        {
            toggleJenkel((ToggleButton)sender);
            GridCursor.Margin = new Thickness(0, ((68 * 1) + 10), 0, 0);
        }

        private void golToggle_Click(object sender, RoutedEventArgs e)
        {
            toggleGol((ToggleButton)sender);
            GridCursor.Margin = new Thickness(0, ((68 * 5) + 55), 0, 0);
        }


        private void toggleGol(ToggleButton toggleButton)
        {
            Color white = (Color)ColorConverter.ConvertFromString("#fff");
            Color red = (Color)ColorConverter.ConvertFromString("#EB3F3F");
            golA.IsChecked = false;
            golB.IsChecked = false;
            golAB.IsChecked = false;
            golO.IsChecked = false;
            
            golA.Foreground = new SolidColorBrush(red);
            golB.Foreground = new SolidColorBrush(red);
            golAB.Foreground = new SolidColorBrush(red);
            golO.Foreground = new SolidColorBrush(red);


            toggleButton.IsChecked = true;
            toggleButton.Foreground = new SolidColorBrush(white);
        }

        private void toggleJenkel(ToggleButton toggleButton)
        {
            rdLaki.IsChecked = false;
            rdPerempuan.IsChecked = false;

            toggleButton.IsChecked = true;
        }

        private string getGol()
        {
            string golonganDarah;

            if(golA.IsChecked == true)
            {
                golonganDarah = "A";
            } else if (golB.IsChecked == true) {
                golonganDarah = "B";
            } else if(golAB.IsChecked == true)
            {
                golonganDarah = "AB";
            } else
            {
                golonganDarah = "O";
            }

            return golonganDarah;
        }

        
    }
}
