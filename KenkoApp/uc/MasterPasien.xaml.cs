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
    /// Interaction logic for MasterPasien.xaml
    /// </summary>
    public partial class MasterPasien : UserControl
    {
        public MasterPasien()
        {
            InitializeComponent();
        }

        private void MasterPasien_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid(string cari = "")
        {
            dataMaster.ItemsSource = Kenko.getData("sp_Pasien_Read", cari).DefaultView;
        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {

            FormPasien formPasien = new FormPasien();
            formPasien.ShowDialog();

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
                FormPasien formPasien = new FormPasien("Edit", Convert.ToInt32(dataRowView[1].ToString()));

                formPasien.txtNamaPasien.Text = dataRowView[2].ToString();
                if(dataRowView[3].ToString() == "Laki-Laki")
                {
                    formPasien.rdLaki.IsChecked = true;
                } else
                {
                    formPasien.rdPerempuan.IsChecked = true;
                }

                formPasien.txtTglLahir.SelectedDate = DateTime.Parse(dataRowView[4].ToString());
                formPasien.txtAlamat.Text = dataRowView[5].ToString();
                formPasien.txtNoTelp.Text = dataRowView[6].ToString();
                checkGol(formPasien, dataRowView[7].ToString());
                formPasien.txtPekerjaan.Text = dataRowView[8].ToString();

                formPasien.ShowDialog();

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

            SqlCommand delete = new SqlCommand("sp_Pasien_Delete", connection);
            delete.CommandType = CommandType.StoredProcedure;

            delete.Parameters.AddWithValue("id_pasien", dataRowView[1].ToString());
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

        private void checkGol(FormPasien formPasien, string golDar)
        {
            Color white = (Color)ColorConverter.ConvertFromString("#fff");
            if (golDar == "A")
            {
                formPasien.golA.IsChecked = true;
                formPasien.golA.Foreground = new SolidColorBrush(white);
            }
            else if(golDar == "B")
            {
                formPasien.golB.IsChecked = true;
                formPasien.golB.Foreground = new SolidColorBrush(white);
            }
            else if(golDar == "AB")
            {
                formPasien.golAB.IsChecked = true;
                formPasien.golAB.Foreground = new SolidColorBrush(white);
            } else if(golDar == "O")
            {
                formPasien.golO.IsChecked = true;
                formPasien.golO.Foreground = new SolidColorBrush(white);
            }
        }
    }
}
