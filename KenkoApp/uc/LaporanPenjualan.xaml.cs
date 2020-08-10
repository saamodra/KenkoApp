using Microsoft.Reporting.WinForms;
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
            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("id-ID");
            txtTglAwal.Language = lang;
            txtTglAkhir.Language = lang;
            _reportViewer.Load += ReportViewer_Load;
        }


        private void ReportViewer_Load(object sender, EventArgs e)
        {
            RefreshDataGrid(txtTglAwal.SelectedDate, txtTglAkhir.SelectedDate);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid(txtTglAwal.SelectedDate, txtTglAkhir.SelectedDate);
        }

        private void RefreshDataGrid(DateTime? tglawal, DateTime? tglakhir)
        {

            ReportDataSource reportDataSource1 = new ReportDataSource();

            DSKenko dataSet1 = new DSKenko();
            dataSet1.BeginInit();
            dataSet1.EnforceConstraints = false;

            reportDataSource1.Name = "DSKenko";
            //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = dataSet1.sp_LaporanPenjualan;

            _reportViewer.Reset();

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("tglAwal", tglawal.HasValue ? tglawal.Value.ToString("dd/MM/yyyy") : "");
            param[1] = new ReportParameter("tglAkhir", tglakhir.HasValue ? tglakhir.Value.ToString("dd/MM/yyyy") : "");
            param[2] = new ReportParameter("namaManager", Application.Current.Properties["nama"].ToString());


            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            _reportViewer.ZoomMode = ZoomMode.PageWidth;
            _reportViewer.LocalReport.ReportPath = "../../rdlc/LaporanPenjualanDesigner.rdlc";
            _reportViewer.LocalReport.SetParameters(param);
            dataSet1.EndInit();


            DSKenkoTableAdapters.sp_LaporanPenjualanTableAdapter t = new DSKenkoTableAdapters.sp_LaporanPenjualanTableAdapter();
            t.ClearBeforeFill = true;
            t.Fill(dataSet1.sp_LaporanPenjualan, tglawal, tglakhir);
            _reportViewer.RefreshReport();
        }
    }
}
