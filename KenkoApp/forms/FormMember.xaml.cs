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
    /// Interaction logic for FormMember.xaml
    /// </summary>
    public partial class FormMember : Window
    {
        private string formType = "Tambah";
        private string idMember;

        public FormMember()
        {
            InitializeComponent();
            btnSave.Click += btnSave_Click;
        }


        public FormMember(string type, string idMember)
        {
            InitializeComponent();
            if (type == "Edit")
            {
                this.idMember = idMember;
                this.formType = "Edit";
                lblTitle.Text = "Edit Data Member";
                lblSubtitle.Text = "Form ini untuk mengedit data member";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            }
            else
            {
                new FormMember();
            }
        }

        private void FormMember_Loaded(object sender, RoutedEventArgs e)
        {
            if(formType != "Edit")
            {
                txtIdMember.Text = Kenko.generateMemberId();
            }
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

                cmd.Parameters.AddWithValue("id_member", txtIdMember.Text);
                cmd.Parameters.AddWithValue("nik", txtNIK.Text);
                cmd.Parameters.AddWithValue("nama", txtNamaMember.Text);
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
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
        }

        private void txtNIK_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);
        }

        private void txtNamaMember_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 2)), 0, 0);
        }

        private void txtNIK_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNIK.Text, lblNIK);
        }

        private void txtNamaMember_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNamaMember.Text, lblNamaMember);
        }

        private bool validateAll()
        {
            bool namaMember = Kenko.fieldRequired(txtNamaMember.Text, lblNamaMember);
            
            if (namaMember)
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
    }
}
