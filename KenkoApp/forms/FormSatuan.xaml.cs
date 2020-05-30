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

namespace KenkoApp.forms
{
    /// <summary>
    /// Interaction logic for FormSatuan.xaml
    /// </summary>
    public partial class FormSatuan : Window
    {
        private int idSatuan;

        public FormSatuan()
        {
            InitializeComponent();

            btnSave.Click += btnSave_Click;
        }

        public FormSatuan(string type, int idSatuan)
        {
            InitializeComponent();
            if (type == "Edit")
            {
                this.idSatuan = idSatuan;
                lblTitle.Text = "Edit Data Satuan";
                lblSubtitle.Text = "Form ini untuk mengedit data satuan";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            }
            else
            {
                new FormSatuan();
            }
        }

        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

                SqlCommand cmd = new SqlCommand("sp_Satuan_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("satuan", txtSatuan.Text);
                cmd.Parameters.AddWithValue("keterangan", txtKeterangan.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    this.Close();

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

                SqlCommand cmd = new SqlCommand("sp_Satuan_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_satuan", idSatuan);
                cmd.Parameters.AddWithValue("satuan", txtSatuan.Text);
                cmd.Parameters.AddWithValue("keterangan", txtKeterangan.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diupdate!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtSatuan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 0), 0, 0);
        }

        private void txtKeterangan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 1), 0, 0);
        }

        private void txtSatuan_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtSatuan.Text, lblSatuan);
        }

        private void txtKeterangan_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private bool validateAll()
        {
            bool Satuan = Kenko.fieldRequired(txtSatuan.Text, lblSatuan);

            if (Satuan)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
