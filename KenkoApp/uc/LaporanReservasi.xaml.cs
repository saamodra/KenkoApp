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
    /// Interaction logic for LaporanReservasi.xaml
    /// </summary>
    public partial class LaporanReservasi : UserControl
    {
        public LaporanReservasi()
        {
            InitializeComponent(); _reportViewer.Load += ReportViewer_Load;

        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                KenkoDataSet2 dataset = new KenkoDataSet2();

                dataset.BeginInit();
                dataset.EnforceConstraints = false;

                reportDataSource1.Name = "Kenko4";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.LaporanReservasi;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = "../../LaporanReservasi.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                KenkoDataSet2TableAdapters.LaporanReservasiTableAdapter a = new KenkoDataSet2TableAdapters.LaporanReservasiTableAdapter();

                a.ClearBeforeFill = true;
                a.Fill(dataset.LaporanReservasi);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshDataGrid(string cari = "")
        {
            //dataMaster.ItemsSource = Kenko.getData("sp_Member_Read", cari).DefaultView;
        }

        private void LaporanReservasi_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
