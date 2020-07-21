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
    /// Interaction logic for LaporanObat.xaml
    /// </summary>
    public partial class LaporanObat : UserControl
    {
        public LaporanObat()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;

        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            RefreshDataGrid(cmbStatusObat.Text);
        }

        private void RefreshDataGrid(string statusObat)
        {

            ReportDataSource reportDataSource1 = new ReportDataSource();

            DSKenko dataSet1 = new DSKenko();
            dataSet1.BeginInit();

            reportDataSource1.Name = "DSKenko";
            //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = dataSet1.sp_LaporanObat;

            _reportViewer.Reset();

            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("statusObat", statusObat == "Semua" ? "" : statusObat);


            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            _reportViewer.ZoomMode = ZoomMode.PageWidth;
            _reportViewer.LocalReport.ReportPath = "../../rdlc/LaporanObatDesigner.rdlc";
            _reportViewer.LocalReport.SetParameters(param);
            dataSet1.EndInit();

            //fill data into WpfApplication4DataSet
            DSKenkoTableAdapters.sp_LaporanObatTableAdapter t = new DSKenkoTableAdapters.sp_LaporanObatTableAdapter();
            t.ClearBeforeFill = true;
            t.Fill(dataSet1.sp_LaporanObat, statusObat);
            _reportViewer.RefreshReport();

        }

        private void LaporanObat_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid(cmbStatusObat.Text);
        }
    }
}
