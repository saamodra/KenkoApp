using KenkoApp.forms;
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
            DataTable dt = Kenko.getData("sp_Kategori_Read", "");

            listKategori.ItemsSource = dt.DefaultView;
            listKategori.DisplayMemberPath = "nama_kategori";
            listKategori.SelectedValuePath = "id_kategori";

            dtKeranjang = new DataTable();
            dtKeranjang.Columns.Add("id_obat");
            dtKeranjang.Columns.Add("nama_obat");
            dtKeranjang.Columns.Add("tgl_exp");
            dtKeranjang.Columns.Add("jumlah");
            dtKeranjang.Columns.Add("harga_total");
            dtKeranjang.Columns.Add("harga_beli");
            dtKeranjang.Columns.Add("harga_jual");

            txtKasir.Text = "Samodra";
            txtTglTransaksi.Text = DateTime.Now.ToString("dd-MM-yyyy");

            DataTable dtSupplier = Kenko.getData("sp_Supplier_Read", "");
            cmbSupplier.ItemsSource = dtSupplier.DefaultView;
            cmbSupplier.DisplayMemberPath = "nama_supplier";
            cmbSupplier.SelectedValuePath = "id_supplier";

            DataTable dtObat = Kenko.getData("sp_Obat_Read", "");
            dataMaster.ItemsSource = dtObat.DefaultView;

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

            FormBeliObat fbo = new FormBeliObat(dataRowView[2].ToString(), dataRowView[1].ToString());
            fbo.ShowDialog();

            
            if(fbo.result)
            {
                double total = fbo.jumlah_beli * fbo.harga_beli;

                if (dtKeranjang.Rows.Count > 0)
                {
                    bool found = false;
                    for (int i = 0; i < dtKeranjang.Rows.Count; i++)
                    {
                        DataRow row = dtKeranjang.Rows[i];
                        if (row["id_obat"].ToString() == dataRowView[1].ToString() && row["tgl_exp"].ToString() == fbo.tgl_exp)
                        {
                            row["jumlah"] = Convert.ToInt32(row["jumlah"].ToString()) + Convert.ToInt32(fbo.jumlah_beli);
                            row["harga_total"] = Kenko.formatCurrency(Convert.ToDouble(Kenko.getNumber(row["harga_total"].ToString())) + total);

                            
                            jumlahBarang = jumlahBarang + Convert.ToInt32(fbo.jumlah_beli);
                            subtotal = subtotal + total;
                            lblJumlahBarang.Content = jumlahBarang;
                            lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                            lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                            found = true;
                        }

                    }

                    if (!found)
                    {
                        jumlahBarang = jumlahBarang + Convert.ToInt32(fbo.jumlah_beli);
                        subtotal = subtotal + total;
                        dtKeranjang.Rows.Add(fbo.id_obat, fbo.nama_obat, fbo.tgl_exp, fbo.jumlah_beli, Kenko.formatCurrency(total), fbo.harga_beli, fbo.harga_jual);
                        lblJumlahBarang.Content = jumlahBarang;
                        lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                        lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                    }
                }
                else
                {
                    jumlahBarang = jumlahBarang + Convert.ToInt32(fbo.jumlah_beli);
                    subtotal = subtotal + total;
                    dtKeranjang.Rows.Add(fbo.id_obat, fbo.nama_obat, fbo.tgl_exp, fbo.jumlah_beli, Kenko.formatCurrency(total), fbo.harga_beli, fbo.harga_jual);
                    lblJumlahBarang.Content = jumlahBarang;
                    lblSubtotal.Content = Kenko.formatCurrency(subtotal);
                    lblTotalPembayaran.Text = Kenko.formatCurrency(subtotal);
                }

                dataKeranjang.ItemsSource = dtKeranjang.DefaultView;
            }
            
        }

        private void btnHapusGrid_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;


            subtotal = subtotal - double.Parse(Kenko.getNumber(dataRowView[4].ToString()));
            jumlahBarang = jumlahBarang - int.Parse(dataRowView[3].ToString());
            lblSubtotal.Content = Kenko.formatCurrency(subtotal);
            lblJumlahBarang.Content = jumlahBarang;
            dtKeranjang.Rows.Remove(dataRowView.Row);
            dataKeranjang.ItemsSource = dtKeranjang.DefaultView;
            lblTotalPembayaran.Text = "0";
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
        
        private void NewTransaksi()
        {
            dtKeranjang.Rows.Clear();
            lblSubtotal.Content = 0;
            lblJumlahBarang.Content = 0;
            
            lblTotalPembayaran.Text = "0";
            cmbSupplier.SelectedIndex = -1;

            DataTable dtObat = Kenko.getData("sp_Obat_Read", "");
            dataMaster.ItemsSource = dtObat.DefaultView;
        }


        private void btnBaru_Click(object sender, RoutedEventArgs e)
        {
            NewTransaksi();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSupplier.SelectedIndex < 0)
            {
                MessageBox.Show("Silahkan pilih supplier terlebih dahulu", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (dtKeranjang.Rows.Count <= 0) {
                MessageBox.Show("Silahkan isi keranjang pembelian.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_Transaksi_Pembelian", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                string no_pembelian = Kenko.generateId("PB", "sp_Transaksi_Pembelian_GetLast");

                cmd.Parameters.AddWithValue("no_pembelian", no_pembelian);
                cmd.Parameters.AddWithValue("total_harga", Kenko.getNumber(lblTotalPembayaran.Text));
                cmd.Parameters.AddWithValue("id_supplier", cmbSupplier.SelectedValue);
                cmd.Parameters.AddWithValue("id_user", 2);


                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    connection.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                foreach (DataRow row in dtKeranjang.Rows)
                {
                    saveObat(row, no_pembelian);

                }

                MessageBox.Show("Data pembelian berhasil ditambahkan", "Berhasil", MessageBoxButton.OK, MessageBoxImage.Information);

                NewTransaksi();
            }
           
        }


        private void saveObat(DataRow row, string no_pembelian)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

            SqlCommand cmd = new SqlCommand("sp_Transaksi_Beli_Obat", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            string id_obat2 = Kenko.generateId("OB", "sp_Obat_GetLast");

            cmd.Parameters.AddWithValue("no_pembelian", no_pembelian);
            cmd.Parameters.AddWithValue("id_obat1", row["id_obat"].ToString());
            cmd.Parameters.AddWithValue("id_obat2", id_obat2);
            cmd.Parameters.AddWithValue("nama_obat", row["nama_obat"].ToString());
            cmd.Parameters.AddWithValue("tgl_expired", row["tgl_exp"].ToString());
            cmd.Parameters.AddWithValue("jumlah_beli", row["jumlah"].ToString());
            cmd.Parameters.AddWithValue("harga_beli", row["harga_beli"].ToString());
            cmd.Parameters.AddWithValue("harga_jual", row["harga_jual"].ToString());
            cmd.Parameters.AddWithValue("total_harga", Kenko.getNumber(row["harga_total"].ToString()));


            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

