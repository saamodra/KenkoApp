using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using KenkoApp.uc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for FormUser.xaml
    /// </summary>
    public partial class FormUser : Window
    {
        private int idUser;
        private bool updateImage = false;

        public FormUser()
        {
            InitializeComponent();
            btnSave.Click += btnSave_Click;
        }

        public FormUser(string type, int idUser)
        {
            InitializeComponent();

            if (type == "Edit")
            {
                this.idUser = idUser;
                lblTitle.Text = "Edit Data User";
                lblSubtitle.Text = "Form ini untuk mengedit data user";
                FormIcon.Kind = PackIconKind.PencilBox;
                Title = "Edit Data";

                btnSave.Click += btnEdit_Click;
            } else
            {
                btnSave.Click += btnSave_Click;
            }
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

                SqlCommand cmd = new SqlCommand("sp_User_Create", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("nama", txtNama.Text);
                cmd.Parameters.AddWithValue("username", txtUsername.Text);
                cmd.Parameters.AddWithValue("password", txtPassword.Password);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);
                cmd.Parameters.AddWithValue("role", cmbRole.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("foto", Kenko.uploadImage(txtFileName.Text));

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
                MessageBox.Show("Data gagal diupdate", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_User_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_user", idUser);
                cmd.Parameters.AddWithValue("nama", txtNama.Text);
                cmd.Parameters.AddWithValue("username", txtUsername.Text);
                cmd.Parameters.AddWithValue("password", txtPassword.Password);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);
                cmd.Parameters.AddWithValue("role", cmbRole.SelectedValue.ToString());
                if(updateImage)
                {
                    cmd.Parameters.AddWithValue("foto", Kenko.uploadImage(txtFileName.Text, idUser.ToString()));
                } else
                {
                    cmd.Parameters.AddWithValue("foto", txtFileName.Text);
                }

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
                    MessageBox.Show("Data gagal diupdate : " + ex.Message);
                }
            }
        }

        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtFileName.Text = openFileDialog.FileName;
                updateImage = true;
            }
        }

        private void FormUser_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("id"), new DataColumn("role") });
            dt.Rows.Add(new object[] { 1, "Admin" });
            dt.Rows.Add(new object[] { 2, "Apoteker" });
            dt.Rows.Add(new object[] { 3, "Manager" });

            cmbRole.ItemsSource = dt.DefaultView;
        }

        private void txtNama_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 0)), 0, 0);
        }

        private void txtUsername_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 1)), 0, 0);
        }

        private void txtEmail_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 2)), 0, 0);
        }

        private void txtPassword_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 3)), 0, 0);
        }
        private void cmbRole_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 4)), 0, 0);
        }

        private void txtNama_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtNama.Text, lblNama);
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtUsername.Text, lblUsername);
        }

        private void cmbRole_LostFocus(object sender, RoutedEventArgs e)
        {
            Kenko.comboRequired(cmbRole, lblRole);
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kenko.fieldRequired(txtEmail.Text, lblEmail);
        }
        
        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRequired(txtPassword.Password, lblPassword);
        }

        private void txtNama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.alphabetOnlyInput(e);
        }

        private bool validateAll()
        {
            bool Nama = Kenko.fieldRequired(txtNama.Text, lblNama);
            bool Username = Kenko.fieldRequired(txtUsername.Text, lblUsername);
            bool Role = Kenko.comboRequired(cmbRole, lblRole);
            bool Email = Kenko.fieldRequired(txtEmail.Text, lblEmail);
            bool emailFormat = false;
            if (Email)
            {
                emailFormat = Kenko.emailInput(txtEmail.Text, lblEmail);
            }
            bool Password = Kenko.fieldRequired(txtPassword.Password, lblPassword);
            if (Nama && Username && Role && Email && emailFormat && Password)
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
