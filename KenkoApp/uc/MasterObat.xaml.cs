using KenkoApp.forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for MasterObat.xaml
    /// </summary>
    public partial class MasterObat : UserControl
    {
        public MasterObat()
        {
            InitializeComponent();
        }



        private void MasterKategori_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid(string cari = "")
        {
            dataMaster.ItemsSource = Kenko.getData("sp_Obat_Read", cari).DefaultView;
        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {

            FormObat formObat = new FormObat();
            formObat.ShowDialog();

            RefreshDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                FormObat formObat = new FormObat("Edit", dataRowView[1].ToString());

                formObat.txtNamaObat.Text = dataRowView[2].ToString();
                formObat.cmbKategori.SelectedValue = dataRowView[3].ToString();
                formObat.cmbSatuan.SelectedValue = dataRowView[4].ToString();
                formObat.txtTglExpired.SelectedDate = DateTime.Parse(dataRowView[6].ToString());
                formObat.txtStok.Text = dataRowView[7].ToString();
                formObat.txtHargaBeli.Text = Kenko.getNumber1(dataRowView[8].ToString());
                formObat.txtHargaJual.Text = Kenko.getNumber1(dataRowView[9].ToString());
                formObat.txtHet.Text = Kenko.getNumber1(dataRowView[10].ToString());
                formObat.txtProdusen.Text = dataRowView[11].ToString();
                formObat.txtDeskripsi.Text = dataRowView[12].ToString();

                formObat.ShowDialog();

                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnHapus_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;


            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

            SqlCommand delete = new SqlCommand("sp_Obat_Delete", connection);
            delete.CommandType = CommandType.StoredProcedure;

            delete.Parameters.AddWithValue("id_obat", dataRowView[1].ToString());
            MessageBoxResult messageBoxResult = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    connection.Open();
                    delete.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil dihapus!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();

                    RefreshDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal dihapus : " + ex.Message);
                }
            }
        }

        private void txtCari_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RefreshDataGrid(txtCari.Text);
            }
        }

        private void txtCari_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDataGrid(txtCari.Text);
        }
    }
}
