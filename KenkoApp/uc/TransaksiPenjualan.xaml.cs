using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Schema;

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for TransaksiPenjualan.xaml
    /// </summary>
    public partial class TransaksiPenjualan : UserControl
    {
        private DataTable dtPenjualan;

        public TransaksiPenjualan()
        {
            InitializeComponent();
        }

        private void TransaksiPenjualan_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = Kenko.getData("sp_Kategori_Read", "");

            listKategori.ItemsSource = dt.DefaultView;
            listKategori.DisplayMemberPath = "nama_kategori";
            listKategori.SelectedValuePath = "id_kategori";

            dtPenjualan = new DataTable();
            dtPenjualan.Columns.Add("id_obat");
            dtPenjualan.Columns.Add("nama_obat");
            dtPenjualan.Columns.Add("jumlah");
            dtPenjualan.Columns.Add("harga");

            txtKasir.Text = "Samodra";
            txtTglTransaksi.Text = DateTime.Now.ToString("dd-MM-yyyy");

        }

        private void listKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listKategori.SelectedIndex;
            string idkategori = listKategori.SelectedValue.ToString();
            SelectedMenuChange(index);

            dataMaster.ItemsSource = Kenko.getData("sp_Obat_GetKategori", idkategori).DefaultView;
        }

        private void SelectedMenuChange(int index)
        {
            TrainsitioningContentSlide.OnApplyTemplate();
            GridCursor.Visibility = Visibility.Visible;

            GridCursor.Margin = new Thickness(0, ((38 * index)), 0, 0);
        }

        private void txtJumlah_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private double subtotal = 0;
        private int jumlahBarang = 0;

        private void btnTambahGrid_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

            ContentPresenter jml = dataMaster.Columns[5].GetCellContent(dataRowView) as ContentPresenter;
            var myTemplate = jml.ContentTemplate;
            if(myTemplate != null)
            {
                TextBox jumlah = myTemplate.FindName("txtJumlah", jml) as TextBox;

                if(jumlah.Text == "")
                {
                    MessageBox.Show("Silahkan isi jumlah beli terlebih dahulu");
                } else
                {
                    double total = Double.Parse(jumlah.Text) * Double.Parse(dataRowView[9].ToString());

                    if(dtPenjualan.Rows.Count > 0)
                    {
                        bool found = false;
                        for(int i = 0; i < dtPenjualan.Rows.Count; i++)
                        {
                            DataRow row = dtPenjualan.Rows[i];
                            if (row["id_obat"].ToString() == dataRowView[1].ToString())
                            {
                                row["jumlah"] = Convert.ToInt32(row["jumlah"].ToString()) + Convert.ToInt32(jumlah.Text);
                                row["harga"] = Convert.ToDouble(row["harga"]) + total;

                                jumlahBarang = jumlahBarang + Convert.ToInt32(jumlah.Text);
                                subtotal = subtotal + total;
                                lblJumlahBarang.Content = jumlahBarang;
                                lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                                lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                                found = true;
                            }
                            
                        }

                        if(!found)
                        {
                            jumlahBarang = jumlahBarang + Convert.ToInt32(jumlah.Text);
                            subtotal = subtotal + total;
                            dtPenjualan.Rows.Add(dataRowView[1].ToString(), dataRowView[2].ToString(), jumlah.Text, total);
                            lblJumlahBarang.Content = jumlahBarang;
                            lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                            lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                        }
                    } else
                    {
                        jumlahBarang = jumlahBarang + Convert.ToInt32(jumlah.Text);
                        subtotal = subtotal + total;
                        dtPenjualan.Rows.Add(dataRowView[1].ToString(), dataRowView[2].ToString(), jumlah.Text, total);
                        lblJumlahBarang.Content = jumlahBarang;
                        lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                        lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                        //string resultString = Regex.Match(Kenko.formatCurrency(102), @"\d+").Value;
                        //MessageBox.Show(resultString);
                    }

                    dataPenjualan.ItemsSource = dtPenjualan.DefaultView;
                    jumlah.Text = "0";
                }
            } else
            {
                MessageBox.Show("Silahkan isi jumlah beli terlebih dahulu");
            }

        }

        private void btnHapusGrid_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            subtotal = subtotal - double.Parse(dataRowView[3].ToString());
            jumlahBarang = jumlahBarang - int.Parse(dataRowView[2].ToString());
            lblSubtotal.Content = Kenko.formatCurrency(subtotal);
            lblJumlahBarang.Content = jumlahBarang;
            dtPenjualan.Rows.Remove(dataRowView.Row);
            dataPenjualan.ItemsSource = dtPenjualan.DefaultView;
        }

        private void txtJumlah_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((TextBox)e.Source).DataContext;

            ContentPresenter jml = dataMaster.Columns[5].GetCellContent(dataRowView) as ContentPresenter;
            var myTemplate = jml.ContentTemplate;
            if (myTemplate != null)
            {
                TextBox jumlah = myTemplate.FindName("txtJumlah", jml) as TextBox;

                if(jumlah.Text == "0")
                {
                    jumlah.Text = "";
                }
            }

        }

        private void btnCariMember_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = Kenko.getData("sp_Member_GetMember", txtIdMember.Text);

            if(dt.Rows.Count > 0)
            {
                txtIdMember.IsReadOnly = true;
                txtNamaMember.Text = dt.Rows[0]["nama"].ToString();
                txtJumlahPoin.Text = dt.Rows[0]["poin"].ToString();
                txtPoin.IsEnabled = true;
            } else
            {
                MessageBox.Show("Data tidak ditemukan", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        private void txtPoin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(int.Parse(txtJumlahPoin.Text) < int.Parse(txtPoin.Text))
                {
                    MessageBox.Show("Poin tidak mencukupi", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    double total = subtotal - double.Parse(txtPoin.Text);
                    lblTotalPembayaran.Text = Kenko.formatCurrency(total);
                    MessageBox.Show(txtPoin.Text + " telah berhasil digunakan", "Berhasil", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void txtBayar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                double totalPembayaran = Convert.ToDouble(Kenko.getNumber(lblTotalPembayaran.Text));
                double bayar = double.Parse(txtBayar.Text);
                if(bayar < totalPembayaran)
                {
                    MessageBox.Show("Uang pembayaran tidak mencukupi", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    double kembalian = double.Parse(txtBayar.Text) - totalPembayaran;
                    lblKembalian.Content = Kenko.formatCurrency(kembalian);

                    btnBayar.IsEnabled = true;
                    btnCetak.IsEnabled = true;
                }
            }
        }

        private void btnBayar_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

            SqlCommand cmd = new SqlCommand("sp_Transaksi_Penjualan", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            string no_penjualan = Kenko.generateId("PJ", "sp_Transaksi_Penjualan_GetLast", "no_penjualan");
            cmd.Parameters.AddWithValue("no_penjualan", no_penjualan);
            cmd.Parameters.AddWithValue("tgl_beli", DateTime.Now);
            cmd.Parameters.AddWithValue("total_harga", Convert.ToDouble(Kenko.getNumber(lblTotalPembayaran.Text)));
            cmd.Parameters.AddWithValue("id_member", (txtPoin.IsEnabled == true) ? (object)txtIdMember.Text : DBNull.Value);
            cmd.Parameters.AddWithValue("poin_terpakai", txtPoin.Text);
            cmd.Parameters.AddWithValue("id_user", 2);


            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                connection.Close();

                foreach (DataRow row in dtPenjualan.Rows)
                {
                    cmd = new SqlCommand("sp_Penjualan_Detail", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MessageBox.Show(row["id_obat"].ToString());
                    cmd.Parameters.AddWithValue("no_penjualan", no_penjualan);
                    cmd.Parameters.AddWithValue("id_obat", row["id_obat"].ToString());
                    cmd.Parameters.AddWithValue("jumlah", row["jumlah"].ToString());
                    cmd.Parameters.AddWithValue("total_harga", row["harga"].ToString());

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                }


                MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                NewTransaksi();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Baru()
        {
            txtIdMember.IsReadOnly = false;
            txtIdMember.Text = "";
            txtNamaMember.Text = "";
            txtJumlahPoin.Text = "";
        }

        private void NewTransaksi()
        {
            Baru();
            dtPenjualan.Rows.Clear();
            lblSubtotal.Content = 0;
            lblJumlahBarang.Content = 0;
            txtPoin.Text = "";
            lblTotalPembayaran.Text = "0";
            txtBayar.Text = "";
            lblKembalian.Content = 0;

        }
    }
}
