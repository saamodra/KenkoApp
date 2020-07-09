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
    /// Interaction logic for LaporanPembelian.xaml
    /// </summary>
    public partial class LaporanPembelian : UserControl
    {
        public LaporanPembelian()
        {
            InitializeComponent();
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglAwal.Language = lang;
            txtTglAkhir.Language = lang;
            _reportViewer.Load += ReportViewer_Load;

        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                KenkoDataSet1 dataset = new KenkoDataSet1();

                dataset.BeginInit();
                dataset.EnforceConstraints = false;

                reportDataSource1.Name = "Kenko2";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.LaporanPembelian;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = "../../LaporanPembelianDesigner.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                KenkoDataSet1TableAdapters.LaporanPembelianTableAdapter a = new KenkoDataSet1TableAdapters.LaporanPembelianTableAdapter();

                a.ClearBeforeFill = true;
                a.Fill(dataset.LaporanPembelian);
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



        private void LaporanPembelian_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();

        }
    }
}
