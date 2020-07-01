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

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for TransaksiPembelian.xaml
    /// </summary>
    public partial class TransaksiPembelian : UserControl
    {
        private DataTable dtKeranjang;
        public TransaksiPembelian()
        {
            InitializeComponent();
        }

        private void TransaksiPembelian_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = Kenko.getData("sp_Dokter_Read", "");

            listKategori.ItemsSource = dt.DefaultView;
            listKategori.DisplayMemberPath = "nama_kategori";
            listKategori.SelectedValuePath = "id_kategori";

            dtKeranjang = new DataTable();
            dtKeranjang.Columns.Add("id_obat");
            dtKeranjang.Columns.Add("nama_obat");
            dtKeranjang.Columns.Add("jumlah");
            dtKeranjang.Columns.Add("harga");

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
            if (myTemplate != null)
            {
                TextBox jumlah = myTemplate.FindName("txtJumlah", jml) as TextBox;

                if (jumlah.Text == "")
                {
                    MessageBox.Show("Silahkan isi jumlah beli terlebih dahulu");
                }
                else
                {
                    double total = Double.Parse(jumlah.Text) * Double.Parse(dataRowView[9].ToString());

                    if (dtKeranjang.Rows.Count > 0)
                    {
                        bool found = false;
                        for (int i = 0; i < dtKeranjang.Rows.Count; i++)
                        {
                            DataRow row = dtKeranjang.Rows[i];
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

                        if (!found)
                        {
                            jumlahBarang = jumlahBarang + Convert.ToInt32(jumlah.Text);
                            subtotal = subtotal + total;
                            dtKeranjang.Rows.Add(dataRowView[1].ToString(), dataRowView[2].ToString(), jumlah.Text, total);
                            lblJumlahBarang.Content = jumlahBarang;
                            lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                            lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                        }
                    }
                    else
                    {
                        jumlahBarang = jumlahBarang + Convert.ToInt32(jumlah.Text);
                        subtotal = subtotal + total;
                        dtKeranjang.Rows.Add(dataRowView[1].ToString(), dataRowView[2].ToString(), jumlah.Text, total);
                        lblJumlahBarang.Content = jumlahBarang;
                        lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                        lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                    }

                    dataKeranjang.ItemsSource = dtKeranjang.DefaultView;
                    jumlah.Text = "0";
                }
            }
            else
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
            dtKeranjang.Rows.Remove(dataRowView.Row);
            dataKeranjang.ItemsSource = dtKeranjang.DefaultView;
        }

        private void txtJumlah_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((TextBox)e.Source).DataContext;

            ContentPresenter jml = dataMaster.Columns[5].GetCellContent(dataRowView) as ContentPresenter;
            var myTemplate = jml.ContentTemplate;
            if (myTemplate != null)
            {
                TextBox jumlah = myTemplate.FindName("txtJumlah", jml) as TextBox;

                if (jumlah.Text == "0")
                {
                    jumlah.Text = "";
                }
            }

        }


        private void txtBayar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                double totalPembayaran = Convert.ToDouble(Kenko.getNumber(lblTotalPembayaran.Text));
                double bayar = double.Parse(txtBayar.Text);
                if (bayar < totalPembayaran)
                {
                    MessageBox.Show("Uang pembayaran tidak mencukupi", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
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

            string no_penjualan = Kenko.generateId("PJ", "sp_Transaksi_Penjualan_GetLast");
            cmd.Parameters.AddWithValue("no_penjualan", no_penjualan);
            cmd.Parameters.AddWithValue("tgl_beli", DateTime.Now);
            cmd.Parameters.AddWithValue("total_harga", Convert.ToDouble(Kenko.getNumber(lblTotalPembayaran.Text)));

            cmd.Parameters.AddWithValue("id_user", 2);


            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                connection.Close();

                foreach (DataRow row in dtKeranjang.Rows)
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

        private void NewTransaksi()
        {
            dtKeranjang.Rows.Clear();
            lblSubtotal.Content = 0;
            lblJumlahBarang.Content = 0;
            txtPoin.Text = "";
            lblTotalPembayaran.Text = "0";
            txtBayar.Text = "";
            lblKembalian.Content = 0;

        }

        private void btnBaru_Click(object sender, RoutedEventArgs e)
        {
            NewTransaksi();
        }
    }
}
