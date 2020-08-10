using KenkoApp.uc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for FormBeliObat.xaml
    /// </summary>
    public partial class FormBeliObat : Window
    {
        public string id_obat, nama_obat;
        public int jumlah_beli;
        public double harga_beli, harga_jual;
        public string tgl_exp;

        public bool result = false;

        public FormBeliObat()
        {
            InitializeComponent();
        }

        public FormBeliObat(string nama_obat, string id_obat)
        {
            InitializeComponent();
            this.id_obat = id_obat;
            this.nama_obat = nama_obat;
            txtNamaObat.Text = nama_obat;
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
                result = true;
                jumlah_beli = Convert.ToInt32(txtJumlahBeli.Text);
                harga_beli = Convert.ToDouble(Kenko.getNumber2(txtHargaBeli.Text));
                harga_jual = Convert.ToDouble(Kenko.getNumber2(txtHargaJual.Text));
                string newFormat = DateTime.ParseExact(txtTglExpired.Text, "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                tgl_exp = newFormat;
                Close();
            }
        }


        private void txtNamaObat_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 0), 0, 0);
        }

        private void txtTglExpired_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 1), 0, 0);
        }

        private void txtJumlahBeli_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 2), 0, 0);
        }

        private void txtHargaBeli_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 3), 0, 0);
        }

        private void txtHargaJual_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 4), 0, 0);
        }

        private void txtNamaObat_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaObat.Text, lblNamaObat);
        }

        private void txtJumlahBeli_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtJumlahBeli.Text, lblJumlahBeli);
        }

        private void txtHargaBeli_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtHargaBeli.Text, lblHargaBeli);
            Kenko.textFieldCurrencyFormat(txtHargaBeli, "Rp");
        }

        private void txtHargaJual_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtHargaJual.Text, lblHargaJual);
            Kenko.textFieldCurrencyFormat(txtHargaJual, "Rp");
        }


        private void txtTglExpired_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.dateRequired(txtTglExpired, lblTgl);
        }

        
        private void textHargaBeli_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.textFieldCurrencyFormat(txtHargaBeli, "Rp");
        }


        private bool validateAll()
        { 
            bool nama_obat = Kenko.fieldRequired(txtNamaObat.Text, lblNamaObat);
            bool tglexp = Kenko.dateRequired(txtTglExpired, lblTgl);
            bool jumlahBeli = Kenko.fieldRequired(txtJumlahBeli.Text, lblJumlahBeli);
            bool hargaSatuan = Kenko.fieldRequired(txtHargaBeli.Text, lblHargaJual);

            if (nama_obat && tglexp && jumlahBeli && hargaSatuan)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtNumeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

    }
}
