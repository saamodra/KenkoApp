using KenkoApp.uc;
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
using System.Windows.Shapes;

namespace KenkoApp.rdlc
{
    /// <summary>
    /// Interaction logic for TesStruk.xaml
    /// </summary>
    public partial class TesStruk : Window
    {
        StrukDataset.PenjualanDataTable penjualans = new StrukDataset.PenjualanDataTable();
        StrukDataset.Detail_PenjualanDataTable detail_Penjualans = new StrukDataset.Detail_PenjualanDataTable();
        TesDataset.MahasiswaDataTable mahasiswas = new TesDataset.MahasiswaDataTable();
        TesDataset.JurusanDataTable jurusans = new TesDataset.JurusanDataTable();

        public TesStruk()
        {
            InitializeComponent();

        }



        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            StrukPreview strukPreview = new StrukPreview();
            strukPreview.Close();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            //Environment.Exit(0);
        }
    }
}
