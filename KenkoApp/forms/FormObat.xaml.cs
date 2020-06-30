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
using Microsoft.SqlServer.Server;
using System.Globalization;

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
            cmbKategori.ItemsSource = Kenko.getData("sp_Kategori_Read").DefaultView;

            cmbSatuan.ItemsSource = Kenko.getData("sp_Satuan_Read").DefaultView;

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

                SqlCommand cmd = new SqlCommand("sp_Obat_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("id_obat", Kenko.generateId("OB", "sp_Obat_GetLast"));
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
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void txtIdObat_Focus(object sender, RoutedEventArgs e)
        {
        }

        private void txtNamaObat_Focus(object sender, RoutedEventArgs e)
        {

            GridLocation(0, 0, 0);
        }

        private void cmbKategori_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(1, 0, 0);
        }

        private void cmbSatuan_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(2, 0, 0);
        }

        private void txtTglExpired_Focus(object sender, KeyboardFocusChangedEventArgs e)
        {
            GridLocation(3, 0, 0);
        }

        private void txtStok_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(4, 0, 0);
        }

        private void txtHargaBeli_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(5, 0, 0);
        }

        private void txtHargaJual_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(0, 0, 1);
        }

        private void txtHet_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(1, 0, 1);
        }

        private void txtProdusen_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(2, 0, 1);
        }

        private void txtDeskripsi_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(3, 10, 1);
            GridCursor2.Height = 85;
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
            //if (!string.IsNullOrEmpty(txtHargaJual.Text))
            //{
            //    Kenko.fieldRequired(txtHargaJual.Text, lblHargaJual);
            //    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            //    int valueBefore = Int32.Parse(txtHargaJual.Text, System.Globalization.NumberStyles.AllowThousands);
            //    txtHargaJual.Text = String.Format(culture, "{0:N0}", valueBefore);
            //    txtHargaJual.Select(txtHargaJual.Text.Length, 0);
            //}
            
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

        private void txtHargaBeli_Keyup(object sender, KeyEventArgs e)
        {
            //txtHargaBeli.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("id-ID"), "{0:#,##0.00}", double.Parse(txtHargaBeli.Text));
        }

        private void GridLocation(int i, int plus, int section)
        {
            if(section == 0)
            {
                GridCursor.Visibility = Visibility.Visible;
                GridCursor.Margin = new Thickness(0, ((68 * i) + plus), 0, 0);
                GridCursor2.Visibility = Visibility.Hidden;
            } else
            {
                GridCursor.Visibility = Visibility.Hidden;
                GridCursor2.Margin = new Thickness(0, ((68 * i) + plus), 0, 0);
                GridCursor2.Visibility = Visibility.Visible;
            }
            
        }

        private void txtHargaJual_KeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
