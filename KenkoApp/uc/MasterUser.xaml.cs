using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for MasterUser.xaml
    /// </summary>
    public partial class MasterUser : UserControl
    {
        public MasterUser()
        {
            InitializeComponent();
        }

        private void MasterUser_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid(string cari = "")
        {
            dataMaster.ItemsSource = Kenko.getData("sp_User_Read", cari).DefaultView;
        }


        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {

            FormUser formUser = new FormUser();
            formUser.ShowDialog();

            RefreshDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                FormUser formUser = new FormUser("Edit", Convert.ToInt32(dataRowView[1].ToString()));


                formUser.txtNama.Text = dataRowView[2].ToString();
                formUser.txtUsername.Text = dataRowView[3].ToString();
                formUser.txtEmail.Text = dataRowView[5].ToString();
                formUser.cmbRole.SelectedValue = dataRowView[6].ToString();
                formUser.txtFileName.Text = dataRowView[7].ToString();

                formUser.ShowDialog();

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

            string id = dataRowView[1].ToString();

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

            SqlCommand delete = new SqlCommand("sp_User_Delete", connection);
            delete.CommandType = CommandType.StoredProcedure;

            delete.Parameters.AddWithValue("id_user", id);
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
