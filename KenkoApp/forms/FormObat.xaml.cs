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
    /// Interaction logic for FormObat.xaml
    /// </summary>
    public partial class FormObat : Window
    {
        private string idObat;

        public FormObat()
        {
            InitializeComponent();
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglExpired.Language = lang;
            txtTglExpired.Language = lang;
            txtIdObat.Text = Kenko.generateObatId();
            btnSave.Click += btnSave_Click;
        }

        public FormObat(string type, string idObat)
        {
            InitializeComponent(); 
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglExpired.Language = lang;
            txtTglExpired.Language = lang;
            if (type == "Edit")
            {
                this.idObat = idObat;
                lblTitle.Text = "Edit Data Obat";
                lblSubtitle.Text = "Form ini untuk mengedit data supplier";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            }
            else
            {
                new FormObat();
            }
        }

        private void FormObat_Loaded(object sender, RoutedEventArgs e)
        {
            cmbKategori.ItemsSource = Kenko.getData("sp_Kategori_Cari").DefaultView;

            cmbSatuan.ItemsSource = Kenko.getData("sp_Satuan_Cari").DefaultView;

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

                SqlCommand cmd = new SqlCommand("sp_Obat_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_obat", txtIdObat.Text);
                cmd.Parameters.AddWithValue("nama_obat", txtNamaObat.Text);
                cmd.Parameters.AddWithValue("id_kategori", cmbKategori.SelectedValue);
                cmd.Parameters.AddWithValue("id_satuan", cmbSatuan.SelectedValue);
                cmd.Parameters.AddWithValue("tgl_masuk", DateTime.Now.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("tgl_expired", Kenko.FormatLocalDate(txtTglExpired.Text));
                cmd.Parameters.AddWithValue("stok", txtStok.Text);
                cmd.Parameters.AddWithValue("harga_beli", txtHargaBeli.Text);
                cmd.Parameters.AddWithValue("harga_jual", txtHargaJual.Text);
                cmd.Parameters.AddWithValue("het", txtHet.Text);
                cmd.Parameters.AddWithValue("produsen", txtProdusen.Text);
                cmd.Parameters.AddWithValue("deskripsi", txtDeskripsi.Text);
                cmd.Parameters.AddWithValue("updated_at", DateTime.Now);

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

                SqlCommand cmd = new SqlCommand("sp_Obat_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_obat", idObat);
                cmd.Parameters.AddWithValue("nama_obat", txtNamaObat.Text);
                cmd.Parameters.AddWithValue("id_kategori", cmbKategori.SelectedValue);
                cmd.Parameters.AddWithValue("id_satuan", cmbSatuan.SelectedValue);
                cmd.Parameters.AddWithValue("tgl_expired", Kenko.FormatLocalDate(txtTglExpired.Text));
                cmd.Parameters.AddWithValue("stok", txtStok.Text);
                cmd.Parameters.AddWithValue("harga_beli", txtHargaBeli.Text);
                cmd.Parameters.AddWithValue("harga_jual", txtHargaJual.Text);
                cmd.Parameters.AddWithValue("het", txtHet.Text);
                cmd.Parameters.AddWithValue("produsen", txtProdusen.Text);
                cmd.Parameters.AddWithValue("deskripsi", txtDeskripsi.Text);
                cmd.Parameters.AddWithValue("updated_at", DateTime.Now);

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
        private void txtIdObat_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtNamaObat_Focus(object sender, RoutedEventArgs e)
        {

            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void cmbKategori_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, ((68 * 2)), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void cmbSatuan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, (68 * 3), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtTglExpired_Focus(object sender, KeyboardFocusChangedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, (68 * 4), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtStok_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, (68 * 5), 0, 0);
            GridCursor2.Visibility = Visibility.Hidden;
        }

        private void txtHargaBeli_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, (68 * 0), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtHargaJual_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, (68 * 1), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtHet_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, (68 * 2), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtProdusen_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Margin = new Thickness(0, (68 * 3), 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtDeskripsi_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Visibility = Visibility.Visible;
            GridCursor2.Height = 85;
            GridCursor2.Margin = new Thickness(0, (68 * 4) + 10, 0, 0);
            GridCursor.Visibility = Visibility.Hidden;
        }

        private void txtDeskripsi_LostFocus(object sender, RoutedEventArgs e)
        {
            GridCursor2.Height = 30;
        }

        private void txtNamaObat_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaObat.Text, lblNamaObat);
        }

        private void cmbKategori_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.comboRequired(cmbKategori, lblKategori);
        }

        private void cmbSatuan_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.comboRequired(cmbSatuan, lblSatuan);
        }

        private void txtTglExpired_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRequired(txtTglExpired.Text, lblTglExpired);
        }

        private void txtStok_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtStok.Text, lblStok);
        }

        private void txtHargaBeli_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtHargaBeli.Text, lblHargaBeli);
        }
        private void txtHargaJual_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtHargaJual.Text, lblHargaJual);
        }

        private void txtHet_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtHet.Text, lblHet);
        }

        private void txtProdusen_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtProdusen.Text, lblProdusen);
        }
        
        private void txtDeskripsi_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtDeskripsi.Text, lblDeskripsi);
        }

        private bool validateAll()
        {
            bool NamaObat = Kenko.fieldRequired(txtNamaObat.Text, lblNamaObat);
            bool Kategori = Kenko.comboRequired(cmbKategori, lblKategori);
            bool Satuan = Kenko.comboRequired(cmbSatuan, lblSatuan);
            bool TglExpired = Kenko.fieldRequired(txtTglExpired.Text, lblTglExpired);
            bool Stok = Kenko.fieldRequired(txtStok.Text, lblStok);
            bool HargaBeli = Kenko.fieldRequired(txtHargaBeli.Text, lblHargaBeli);
            bool HargaJual = Kenko.fieldRequired(txtHargaJual.Text, lblHargaJual);
            bool Het = Kenko.fieldRequired(txtHet.Text, lblHet);
            bool Produsen = Kenko.fieldRequired(txtProdusen.Text, lblProdusen);
            bool Deskripsi = Kenko.fieldRequired(txtDeskripsi.Text, lblDeskripsi);


            if (NamaObat && Kategori && Satuan && TglExpired && Stok && HargaBeli
                && HargaJual && Het && Produsen && Deskripsi)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtAlamat_LostFocus(object sender, RoutedEventArgs e)
        {
            GridCursor.Height = 30;
        }

        private void txtHet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private void txtHargaJual_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }
        
        private void txtHargaBeli_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private void txtStok_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

    }
}
