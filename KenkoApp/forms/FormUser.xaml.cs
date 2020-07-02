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
        private string formtype = "Tambah";

        public FormUser()
        {
            InitializeComponent();
            txtOldPassword.Visibility = Visibility.Collapsed;
            lblOldPassword.Visibility = Visibility.Collapsed;
            txtFileName.Visibility = Visibility.Collapsed;
            btnChooseFile.Visibility = Visibility.Collapsed;

            btnSave.Click += btnSave_Click;
        }

        public FormUser(string type, int idUser)
        {
            InitializeComponent();

            if (type == "Edit")
            {
                this.idUser = idUser;
                formtype = type;
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
                MessageBox.Show("Data gagal diupdate", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);

                SqlCommand cmd = new SqlCommand("sp_User_Update", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_user", idUser);
                cmd.Parameters.AddWithValue("nama", txtNama.Text);
                cmd.Parameters.AddWithValue("username", txtUsername.Text);
                if(txtOldPassword.Password == "")
                {
                    cmd.Parameters.AddWithValue("password", DBNull.Value);
                } else
                {
                    cmd.Parameters.AddWithValue("password", txtPassword.Password);
                }
                
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
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data gagal diupdate : " + ex.Message);
                }
            }
        }

        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtFileName.Text = openFileDialog.FileName;
                updateImage = true;
            }

            if(txtFileName.Text == "")
            {
                updateImage = false;
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

        private void txtOldPassword_Focus(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(0, ((68 * 3)), 0, 0);
        }

        private void txtPassword_Focus(object sender, RoutedEventArgs e)
        {
            if(formtype == "Tambah")
            {
                GridCursor.Margin = new Thickness(0, ((68 * 3)), 0, 0);
            } else
            {
                GridCursor.Margin = new Thickness(0, ((68 * 4)), 0, 0);
            }
        }

        private void txtPasswordBaru_Focus(object sender, RoutedEventArgs e)
        {
            if (formtype == "Tambah")
            {
                GridCursor.Margin = new Thickness(0, ((68 * 4)), 0, 0);
            }
            else
            {
                GridCursor.Margin = new Thickness(0, ((68 * 5)), 0, 0);
            }
        }

        private void cmbRole_Focus(object sender, RoutedEventArgs e)
        {
            if (formtype == "Tambah")
            {
                GridCursor.Margin = new Thickness(0, ((68 * 5)), 0, 0);
            }
            else
            {
                GridCursor.Margin = new Thickness(0, ((68 * 6)), 0, 0);
            }
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

        private void txtOldPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRequired(txtOldPassword.Password, lblPassword);
        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRequired(txtPassword.Password, lblPassword);
        }

        private void txtNama_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Kenko.alphabetOnlyInput(e);
        }

        private void txtRetypePass_TextChanged(object sender, RoutedEventArgs e)
        {
            Kenko.fieldRetype(txtPassword.Password, txtRetypePass.Password, lblRetypePass);
        }

        private bool validateAll()
        {
            bool Nama = Kenko.fieldRequired(txtNama.Text, lblNama);
            bool Username = Kenko.fieldRequired(txtUsername.Text, lblUsername);
            bool Role = Kenko.comboRequired(cmbRole, lblRole);
            bool Email = Kenko.fieldRequired(txtEmail.Text, lblEmail);
            bool emailFormat = false;

            if (formtype == "Tambah")
            {

                bool Password = Kenko.fieldRequired(txtPassword.Password, lblPassword);
                bool retype = Kenko.fieldRetype(txtPassword.Password, txtRetypePass.Password, lblRetypePass);
                

                if (Email)
                {
                    emailFormat = Kenko.emailInput(txtEmail.Text, lblEmail);
                }
                if (Nama && Username && Role && Email && emailFormat && Password && retype)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                if (Email)
                {
                    emailFormat = Kenko.emailInput(txtEmail.Text, lblEmail);
                }

                if (txtOldPassword.Password == "")
                {
                    if (Nama && Username && Role && Email && emailFormat)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                } else
                {
                    bool Password = Kenko.fieldRequired(txtPassword.Password, lblPassword);
                    bool retype = Kenko.fieldRetype(txtPassword.Password, txtRetypePass.Password, lblRetypePass);
                    bool oldPass = Kenko.fieldRequired(txtOldPassword.Password, lblPassword);
                    bool checkpass = checkPass(idUser.ToString(), txtOldPassword.Password);
                    if (Nama && Username && Role && Email && emailFormat && Password && retype && oldPass && checkpass)
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

        private bool checkPass(string id_user, string password)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);
           
            SqlCommand cmd = new SqlCommand("sp_Check_UserPass", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id_user", id_user);
            cmd.Parameters.AddWithValue("password", password);

            var returnParameter = cmd.Parameters.Add("@returnCheck", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            conn.Open();
            cmd.ExecuteNonQuery();

            if((int)returnParameter.Value > 0 )
            {
                return true;
            } else
            {
                lblOldPassword.Visibility = Visibility.Visible;
                lblOldPassword.Content = "Password lama tidak sesuai.";
                return false;

            }
            
        }
    }
}
