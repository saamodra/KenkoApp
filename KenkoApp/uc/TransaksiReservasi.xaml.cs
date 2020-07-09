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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for TransaksiReservasi.xaml
    /// </summary>
    public partial class TransaksiReservasi : UserControl
    {
        public TransaksiReservasi()
        {
            InitializeComponent();
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglLahir.Language = lang;
            txtTglLahir.Language = lang;
        }

        private void TransaksiReservasi_Loaded(object sender, RoutedEventArgs e)
        {
            dataMaster.ItemsSource = Kenko.getData("sp_Pasien_Read", "").DefaultView;
            
            cmbDokter.ItemsSource = Kenko.getData("sp_Dokter_Read", "").DefaultView;
            cmbDokter.SelectedValuePath = "id_dokter";
            cmbDokter.DisplayMemberPath = "nama_dokter";

            txtKasir.Text = "Samodra";
            txtTglTransaksi.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblAntrian.Text = Kenko.getAntrian();
            
        }

        private void btnPilih_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            formReadOnly(true);
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            id_pasien.Text = dataRowView[1].ToString();
            txtNamaPasien.Text = dataRowView[2].ToString();
            if (dataRowView[3].ToString() == "Laki-Laki")
            {
                rdLaki.IsChecked = true;
            }
            else
            {
                rdPerempuan.IsChecked = true;
            }

            txtTglLahir.SelectedDate = DateTime.Parse(dataRowView[4].ToString());
            txtAlamat.Text = dataRowView[5].ToString();
            txtNoTelp.Text = dataRowView[6].ToString();
            checkGol(dataRowView[7].ToString());
            txtPekerjaan.Text = dataRowView[8].ToString();

        }

        private void formReadOnly(bool state)
        {
            txtNamaPasien.IsReadOnly = state;
            txtAlamat.IsReadOnly = state;
            txtNoTelp.IsReadOnly = state;
            txtPekerjaan.IsReadOnly = state;
        }

        private void checkGol(string golDar)
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

            if (golDar == "A")
            {
                golA.IsChecked = true;
                golA.Foreground = new SolidColorBrush(white);
            }
            else if (golDar == "B")
            {
                golB.IsChecked = true;
                golB.Foreground = new SolidColorBrush(white);
            }
            else if (golDar == "AB")
            {
                golAB.IsChecked = true;
                golAB.Foreground = new SolidColorBrush(white);
            }
            else if (golDar == "O")
            {
                golO.IsChecked = true;
                golO.Foreground = new SolidColorBrush(white);
            }
        }

        private void ClearForm()
        {
            txtNamaPasien.Text = "";
            txtTglLahir.SelectedDate = null;
            rdLaki.IsChecked = false;
            rdPerempuan.IsChecked = false;
            txtAlamat.Text = "";
            txtNoTelp.Text = "";
            golA.IsChecked = false;
            golAB.IsChecked = false;
            golB.IsChecked = false;
            golO.IsChecked = false;
            txtPekerjaan.Text = "";
        }

        private void ClearDokter()
        {
            //cmbDokter.SelectedIndex = 0;
            txtSpesialisasi.Text = "";
            lblAntrian.Text = Kenko.getAntrian();
            lblNamaDokter.Content = "-";
            txtKeluhan.Text = "";

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
                    ClearForm();

                    dataMaster.ItemsSource = Kenko.getData("sp_Pasien_Read", "").DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            Kenko.fieldMin(txtNoTelp.Text, lblNoTelp, 11);
        }

        private void txtTglLahir_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.dateRequired(txtTglLahir, lblTgl);
        }

        private bool validateAll()
        {
            bool namaPasien = Kenko.fieldRequired(txtNamaPasien.Text, lblNamaPasien);
            bool alamat = Kenko.fieldRequired(txtAlamat.Text, lblAlamat);
            bool noTelp = Kenko.fieldMin(txtNoTelp.Text, lblNoTelp, 11);
            bool tglLahir = Kenko.dateRequired(txtTglLahir, lblTgl);
            bool jenkel = Kenko.toggleRequired(lblJenkel, (bool)rdLaki.IsChecked, (bool)rdPerempuan.IsChecked);

            if (namaPasien && alamat && noTelp && tglLahir && jenkel)
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
        }

        private void golToggle_Click(object sender, RoutedEventArgs e)
        {
            toggleGol((ToggleButton)sender);
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

            if (golA.IsChecked == true)
            {
                golonganDarah = "A";
            }
            else if (golB.IsChecked == true)
            {
                golonganDarah = "B";
            }
            else if (golAB.IsChecked == true)
            {
                golonganDarah = "AB";
            }
            else if (golO.IsChecked == true)
            {
                golonganDarah = "O";
            }
            else
            {
                golonganDarah = "-";
            }

            return golonganDarah;
        }

        private void txtNamaPasien_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.alphabetOnlyInput(e);
        }

        private void cmbDokter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = Kenko.getData("sp_Dokter_GetDokter", cmbDokter.SelectedValue.ToString());

            if (dt.Rows.Count > 0)
            {

                txtSpesialisasi.Text = dt.Rows[0]["spesialisasi"].ToString();
                lblNamaDokter.Content = dt.Rows[0]["nama_dokter"].ToString();                
            }
            else
            {
                MessageBox.Show("Data tidak ditemukan", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDaftar_Click(object sender, RoutedEventArgs e)
        {

            if (id_pasien.Text == "ID Pasien" || id_pasien.Text == "")
            {
                MessageBox.Show("Silahkan pilih pasien terlebih dahulu.", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);

            } else if(cmbDokter.SelectedIndex < 0) {
                MessageBox.Show("Silahkan pilih dokter terlebih dahulu.", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_Transaksi_Reservasi", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("no_reservasi", Kenko.generateId("RV", "sp_Transaksi_Reservasi_GetLast"));
                cmd.Parameters.AddWithValue("id_dokter", cmbDokter.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("id_pasien", id_pasien.Text);
                cmd.Parameters.AddWithValue("keterangan", txtKeluhan.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Reservasi berhasil.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    ClearForm();
                    formReadOnly(false);
                    ClearDokter();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            formReadOnly(false);
        }
    }
}
