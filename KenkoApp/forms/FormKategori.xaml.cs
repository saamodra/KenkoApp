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
    /// Interaction logic for FormKategori.xaml
    /// </summary>
    public partial class FormKategori : Window
    {
        private int idKategori;

        public FormKategori()
        {
            InitializeComponent();

            btnSave.Click += btnSave_Click;
        }

        public FormKategori(string type, int idKategori)
        {
            InitializeComponent();
            if (type == "Edit")
            {
                this.idKategori = idKategori;
                lblTitle.Text = "Edit Data Kategori";
                lblSubtitle.Text = "Form ini untuk mengedit data kategori";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            }
            else
            {
                new FormKategori();
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

                SqlCommand cmd = new SqlCommand("sp_Kategori_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("nama_kategori", txtNamaKategori.Text);
                cmd.Parameters.AddWithValue("singkatan", String.IsNullOrEmpty(txtSingkatan.Text) ? DBNull.Value : (object)txtSingkatan.Text);
                cmd.Parameters.AddWithValue("keterangan", String.IsNullOrEmpty(txtKeterangan.Text) ? DBNull.Value : (object)txtKeterangan.Text);

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

                SqlCommand cmd = new SqlCommand("sp_Kategori_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_kategori", idKategori);
                cmd.Parameters.AddWithValue("nama_kategori", txtNamaKategori.Text);
                cmd.Parameters.AddWithValue("singkatan", String.IsNullOrEmpty(txtSingkatan.Text) ? DBNull.Value : (object)txtSingkatan.Text);
                cmd.Parameters.AddWithValue("keterangan", String.IsNullOrEmpty(txtKeterangan.Text) ? DBNull.Value : (object)txtKeterangan.Text);

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

        private void txtNamaKategori_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 0), 0, 0);
        }

        private void txtSingkatan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 1), 0, 0);
        }

        private void txtKeterangan_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, (68 * 2), 0, 0);
        }

        private void txtNamaKategori_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaKategori.Text, lblNamaKategori);
        }


        private bool validateAll()
        {
            bool Kategori = Kenko.fieldRequired(txtNamaKategori.Text, lblNamaKategori);

            if (Kategori)
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
