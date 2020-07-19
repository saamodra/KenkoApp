using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
using KenkoApp.rdlc;
using Microsoft.Reporting.WinForms;

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for StrukPreview.xaml
    /// </summary>
    public partial class StrukPreview : Window
    {
        StrukDataset.PenjualanDataTable penjualans = new StrukDataset.PenjualanDataTable();
        StrukDataset.Detail_PenjualanDataTable detail_Penjualans = new StrukDataset.Detail_PenjualanDataTable();

        public StrukPreview()
        {
            InitializeComponent();
            PrintReport();
            
        }


        public StrukPreview(StrukDataset.PenjualanDataTable penjualans, StrukDataset.Detail_PenjualanDataTable detail_Penjualans)
        {
            InitializeComponent();
            this.penjualans = penjualans;
            this.detail_Penjualans = detail_Penjualans;
            PrintReport();
        }


        private void PrintReport()
        {
            ReportDataSource reportDataSource1 = new ReportDataSource();
            ReportDataSource reportDataSource2 = new ReportDataSource();
 

            reportDataSource1.Name = "Penjualan";
            reportDataSource2.Name = "StrukDataset";
            //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = penjualans;
            reportDataSource2.Value = detail_Penjualans;

            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            _reportViewer.LocalReport.DataSources.Add(reportDataSource2);

            _reportViewer.LocalReport.ReportPath = "../../rdlc/StrukDesign.rdlc";


            _reportViewer.RefreshReport();

        }

        private void _reportViewer_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            _reportViewer.PrintDialog();
        }
    }
}
