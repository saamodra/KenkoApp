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
    /// Interaction logic for MasterSupplier.xaml
    /// </summary>
    public partial class MasterSupplier : UserControl
    {
        public MasterSupplier()
        {
            InitializeComponent();
        }

        private void MasterSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid(string cari = "")
        {
            dataMaster.ItemsSource = Kenko.getData("sp_Supplier_Read", cari).DefaultView;
        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {

            FormSupplier formSupplier = new FormSupplier();
            formSupplier.ShowDialog();

            RefreshDataGrid();
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                FormSupplier formSupplier = new FormSupplier("Edit", Convert.ToInt32(dataRowView[1].ToString()));

                formSupplier.txtNamaSupplier.Text = dataRowView[2].ToString();
                formSupplier.txtNamaKontak.Text = dataRowView[3].ToString();
                formSupplier.txtAlamat.Text = dataRowView[4].ToString();
                formSupplier.txtNoTelp.Text = dataRowView[5].ToString();
                formSupplier.txtBank.Text = dataRowView[6].ToString();
                formSupplier.txtNoRekening.Text = dataRowView[7].ToString();
                formSupplier.txtEmail.Text = dataRowView[8].ToString();
                formSupplier.txtKeterangan.Text = dataRowView[9].ToString();

                formSupplier.ShowDialog();

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

            SqlCommand delete = new SqlCommand("sp_Supplier_Delete", connection);
            delete.CommandType = CommandType.StoredProcedure;

            delete.Parameters.AddWithValue("id_supplier", dataRowView[1].ToString());
            MessageBoxResult messageBoxResult = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    connection.Open();
                    delete.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil dihapus.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();

                    RefreshDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal dihapus : " + ex.Message);
                }
            }
        }

    }
}
