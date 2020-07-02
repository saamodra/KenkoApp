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
    /// Interaction logic for TransaksiResep.xaml
    /// </summary>
    public partial class TransaksiResep : UserControl
    {
        private DataTable dtResep;
        private string id_pasien = "";
        private string id_reservasi = "";
        private string id_dokter = Application.Current.Properties["id"].ToString();

        public TransaksiResep()
        {
            InitializeComponent();
        }

        private void TransaksiResep_Loaded(object sender, RoutedEventArgs e)
        {
            string id_dokter = Application.Current.Properties["id"].ToString();

            DataTable dtReservasi = Kenko.getData("sp_Transaksi_Reservasi_GetData", id_dokter);
            dataReservasi.ItemsSource = dtReservasi.DefaultView;

            DataTable dtObat = Kenko.getData("sp_Obat_Read", "");
            dataObat.ItemsSource = dtObat.DefaultView;

            dtResep = new DataTable();
            dtResep.Columns.Add("id_obat");
            dtResep.Columns.Add("nama_obat");
            dtResep.Columns.Add("jumlah");
            dtResep.Columns.Add("keterangan");

        }


        private void txtJumlah_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }


        private void btnTambahGrid_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

            ContentPresenter jml = dataObat.Columns[5].GetCellContent(dataRowView) as ContentPresenter;
            var myTemplate = jml.ContentTemplate;
            if (myTemplate != null)
            {
                TextBox jumlah = myTemplate.FindName("txtJumlah", jml) as TextBox;

                if (jumlah.Text == "")
                {
                    MessageBox.Show("Isi jumlah obat terlebih dahulu.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    txtIdObat.Text = dataRowView[1].ToString();
                    txtNamaObat.Text = dataRowView[2].ToString();
                    txtJumlahObat.Text = jumlah.Text;
                    txtSatuan.Text = dataRowView[21].ToString();
                    jumlah.Text = "0";
                }
            }
            else
            {
                MessageBox.Show("Isi jumlah obat terlebih dahulu.", "Berhasil", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void txtJumlah_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((TextBox)e.Source).DataContext;

            ContentPresenter jml = dataObat.Columns[5].GetCellContent(dataRowView) as ContentPresenter;
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

        private void btnTambahResep_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            if(txtKeterangan.Text == "")
            {
                MessageBox.Show("Silahkan isi kolom keterangan dosis terlebih dahulu", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                foreach (DataRow row in dtResep.Rows)
                {
                    if (row["id_obat"].ToString() == txtIdObat.Text)
                    {
                        MessageBox.Show("Obat sudah ditambahkan ke resep", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                        found = true;
                    }
                }

                if (!found)
                {
                    dtResep.Rows.Add(txtIdObat.Text, txtNamaObat.Text, txtJumlahObat.Text, txtKeterangan.Text);
                    dataResep.ItemsSource = dtResep.DefaultView;
                }
            }
        }

        private void btnHapusResep_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            dtResep.Rows.Remove(dataRowView.Row);

            dataResep.ItemsSource = dtResep.DefaultView;
        }
        private void txtNumeric_Keydown(object sender, KeyEventArgs e)
        {

        }

        private void btnTambahReservasi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            
            id_pasien = dataRowView[3].ToString();
            id_reservasi = dataRowView[1].ToString();
            txtNoSip.Text = dataRowView[13].ToString();
            txtNamaDokter.Text = dataRowView[14].ToString();
            txtSpesialisasi.Text = dataRowView[15].ToString();

            txtNamaPasien.Text = dataRowView[9].ToString();
            txtJenkel.Text = dataRowView[10].ToString();
            txtUmur.Text = dataRowView[16].ToString();
            txtPekerjaan.Text = dataRowView[12].ToString();
            txtGol.Text = dataRowView[11].ToString();

            txtKeluhan.Text = dataRowView[6].ToString();
        }

        private void btnBuatResep_Click(object sender, RoutedEventArgs e)
        {
            if(id_pasien == "")
            {
                MessageBox.Show("Pilih antrian terlebih dahulu.", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if(dtResep.Rows.Count < 1)
            {
                MessageBox.Show("Silahkan tambahkan obat terlebih dahulu.", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_Transaksi_Resep", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                string id_resep = Kenko.generateId("RS", "sp_Transaksi_Resep_GetLast");
                string id_dokter = Application.Current.Properties["id"].ToString();
                cmd.Parameters.AddWithValue("id_resep", id_resep);
                cmd.Parameters.AddWithValue("id_pasien", id_pasien);
                cmd.Parameters.AddWithValue("id_dokter", id_dokter);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    connection.Close();

                    foreach (DataRow row in dtResep.Rows)
                    {
                        cmd = new SqlCommand("sp_Transaksi_Resep_Detail", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("id_resep", id_resep);
                        cmd.Parameters.AddWithValue("id_obat", row["id_obat"].ToString());
                        cmd.Parameters.AddWithValue("jumlah", row["jumlah"].ToString());
                        cmd.Parameters.AddWithValue("keterangan", row["keterangan"].ToString());

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                    cmd = new SqlCommand("sp_Transaksi_Reservasi_UpdateStatus", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("no_reservasi", id_reservasi);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    ClearForm();

                    MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void ClearForm()
        {
            txtIdObat.Text = "";
            id_pasien = "";
            id_reservasi = "";
            txtNamaDokter.Text = "";
            txtSpesialisasi.Text = "";
            txtNoSip.Text = "";
            txtNamaPasien.Text = "";
            txtUmur.Text = "";
            txtJenkel.Text = "";
            txtGol.Text = "";
            txtKeluhan.Text = "";
            txtPekerjaan.Text = "";
            txtNamaObat.Text = "";
            txtJumlahObat.Text = "";
            txtSatuan.Text = "";
            txtKeterangan.Text = "";

            dtResep.Rows.Clear();
            dataResep.ItemsSource = dtResep.DefaultView;

            DataTable dtReservasi = Kenko.getData("sp_Transaksi_Reservasi_GetData", id_dokter);
            dataReservasi.ItemsSource = dtReservasi.DefaultView;

            DataTable dtObat = Kenko.getData("sp_Obat_Read", "");
            dataObat.ItemsSource = dtObat.DefaultView;
        }
    }
}
