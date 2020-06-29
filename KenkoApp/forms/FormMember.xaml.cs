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
using System.Windows.Controls.Primitives;

namespace KenkoApp.forms
{
    /// <summary>
    /// Interaction logic for FormMember.xaml
    /// </summary>
    public partial class FormMember : Window
    {
        private string idMember;
        private string formType = "Tambah";
        public FormMember()
        {
            InitializeComponent();
            txtIdMember.Visibility = Visibility.Collapsed;
            lblIdMember.Visibility = Visibility.Collapsed;
            btnSave.Click += btnSave_Click;
        }


        public FormMember(string type, string idMember)
        {
            InitializeComponent();
            if (type == "Edit")
            {
                this.idMember = idMember;
                lblTitle.Text = "Edit Data Member";
                formType = "Edit";
                lblSubtitle.Text = "Form ini untuk mengedit data member";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                txtIdMember.Visibility = Visibility.Visible;
                lblIdMember.Visibility = Visibility.Hidden;
                btnSave.Click += btnEdit_Click;
            }
            else
            {
                new FormMember();
            }
        }

        private void FormMember_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

                SqlCommand cmd = new SqlCommand("sp_Member_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_member", Kenko.generateMemberId());
                cmd.Parameters.AddWithValue("nik", txtNIK.Text);
                cmd.Parameters.AddWithValue("nama", txtNamaMember.Text);
                cmd.Parameters.AddWithValue("jenis_kelamin", Kenko.getJenkel(rdLaki));
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);
                cmd.Parameters.AddWithValue("tgl_bergabung", DateTime.Now.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("poin", 0);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    Close();

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

                SqlCommand cmd = new SqlCommand("sp_Member_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_member", idMember);
                cmd.Parameters.AddWithValue("nik", txtNIK.Text);
                cmd.Parameters.AddWithValue("nama", txtNamaMember.Text);
                cmd.Parameters.AddWithValue("jenis_kelamin", Kenko.getJenkel(rdLaki));
                cmd.Parameters.AddWithValue("no_telp", txtNoTelp.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal disimpan : " + ex.Message, "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtIdMember_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(0, 0);
        }

        private void txtNIK_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(1, 0);
        }

        private void txtNamaMember_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(2, 0);
        }

        private void jenkelToggle_Click(object sender, RoutedEventArgs e)
        {
            toggleJenkel((ToggleButton)sender);
            lblJenkel.Visibility = Visibility.Hidden;
            GridLocation(3, 10);
        }

        private void txtNoTelp_Focus(object sender, RoutedEventArgs e)
        {
            GridLocation(4, 10);
        }

        private void txtNIK_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldMin(txtNIK.Text, lblNIK, 16);
        }

        private void txtNamaMember_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaMember.Text, lblNamaMember);
        }

        private void txtNoTelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldMin(txtNoTelp.Text, lblNoTelp, 11);
        }

        private void txtNoTelp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private bool validateAll()
        {
            bool nik = Kenko.fieldMin(txtNIK.Text, lblNIK, 16);
            bool namaMember = Kenko.fieldRequired(txtNamaMember.Text, lblNamaMember);
            bool jenkel = Kenko.toggleRequired(lblJenkel, (bool)rdLaki.IsChecked, (bool)rdPerempuan.IsChecked);
            bool notelp = Kenko.fieldMin(txtNoTelp.Text, lblNoTelp, 11);

            if (nik && namaMember && jenkel && notelp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtNamaMember_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.alphabetOnlyInput(e);
        }

        private void txtNIK_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.numberOnlyInput(e);
        }

        private void GridLocation(int i, int plus)
        {
            if (formType == "Tambah")
            {
                GridCursor.Margin = new Thickness(0, ((68 * (i-1)) + plus), 0, 0);
            }
            else
            {
                GridCursor.Margin = new Thickness(0, ((68 * i) + plus), 0, 0);
            }
        }

        private void toggleJenkel(ToggleButton toggleButton)
        {
            rdLaki.IsChecked = false;
            rdPerempuan.IsChecked = false;

            toggleButton.IsChecked = true;
        }
    }
}
