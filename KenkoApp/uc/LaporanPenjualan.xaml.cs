using System;
using System.Collections.Generic;
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
    /// Interaction logic for LaporanPenjualan.xaml
    /// </summary>
    public partial class LaporanPenjualan : UserControl
    {
        public LaporanPenjualan()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RefreshDataGrid(string cari = "")
        {
            //dataMaster.ItemsSource = Kenko.getData("sp_Member_Read", cari).DefaultView;
        }

        private void LaporanPenjalan_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
