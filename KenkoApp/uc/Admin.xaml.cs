using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        public Admin()
        {
            InitializeComponent();
            UserControl usc = null;
            usc = new Dashboard();
            GridMain.Children.Add(usc);

            string envImage = Environment.CurrentDirectory;
            string imageUrl = Directory.GetParent(envImage).Parent.FullName;

            var uri = new Uri(imageUrl + "\\images\\default.jpg");
            profilePhoto.ImageSource = new BitmapImage(uri);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            Logo.Visibility = Visibility.Visible;
            Username.Visibility = Visibility.Visible;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            Logo.Visibility = Visibility.Collapsed;
            Username.Visibility = Visibility.Collapsed;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            int index = ListViewMenu.SelectedIndex;
            
            SelectedMenuChange(index);

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemDashboard":
                    usc = new Dashboard();
                    PageTitle.Text = "Dashboard";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemUser":
                    usc = new MasterUser();
                    GridMain.Children.Add(usc);
                    PageTitle.Text = "Master User";
                    break;
                case "ItemSupplier":
                    usc = new MasterSupplier();
                    PageTitle.Text = "Master Supplier";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemMember":
                    usc = new MasterMember();
                    PageTitle.Text = "Master Member";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemDokter":
                    usc = new MasterDokter();
                    PageTitle.Text = "Master Dokter";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemPasien":
                    usc = new MasterPasien();
                    PageTitle.Text = "Master Pasien";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemSatuan":
                    usc = new MasterSatuan();
                    PageTitle.Text = "Master Satuan";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemKategori":
                    usc = new MasterKategori();
                    PageTitle.Text = "Master Kategori";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemObat":
                    usc = new MasterObat();
                    PageTitle.Text = "Master Obat";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemPembelian":
                    usc = new TransaksiPembelian();
                    PageTitle.Text = "Transaksi Pembelian";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemPenjualan":
                    usc = new TransaksiPenjualan();
                    PageTitle.Text = "Transaksi Penjualan";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemAppointment":
                    usc = new TransaksiReservasi();
                    PageTitle.Text = "Transaksi Reservasi";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemResep":
                    usc = new TransaksiReservasi();
                    PageTitle.Text = "Transaksi Resep";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemLaporanPenjualan":
                    usc = new LaporanPenjualan();
                    PageTitle.Text = "Laporan Penjualan";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemLaporanPembelian":
                    usc = new LaporanPembelian();
                    PageTitle.Text = "Laporan Pembelian";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemLaporanObat":
                    usc = new LaporanObat();
                    PageTitle.Text = "Laporan Obat";
                    GridMain.Children.Add(usc);
                    break;
                case "ItemLaporanReservasi":
                    usc = new LaporanReservasi();
                    PageTitle.Text = "Laporan Reservasi";
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void SelectedMenuChange(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            int diff = 0;
            if(index > 12)
            {
                diff = 38;
            } else if(index > 8)
            {
                diff = 19;
            } 
            GridCursor.Margin = new Thickness(0, ((40 * index) - diff), 0, 0);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Login();
        }
    }
}
