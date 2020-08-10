using KenkoApp.rdlc;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using Separator = LiveCharts.Wpf.Separator;

namespace KenkoApp.uc
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {


        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            loadSummaryData();
            refreshGrafikKeuangan();
            refreshGrafikJumlahObat();
        }

        private void chartPenjualan_Loaded(object sender, RoutedEventArgs e)
        {
        }

        
        private void refreshGrafikKeuangan()
        {
            ChartKeuangan chartPenjualan = GetChartKeuangan("sp_chartPenjualan");
            ChartKeuangan chartPembelian = GetChartKeuangan("sp_chartPembelian");

            CartesianChart ch = new CartesianChart();
            ch.Foreground = new SolidColorBrush(Colors.Black);
            ch.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Penjualan",
                    Values = new ChartValues<double>(chartPenjualan.keuangan)
                },
                new ColumnSeries
                {
                    Title = "Pembelian",
                    Values = new ChartValues<double>(chartPembelian.keuangan)
                }
            };

            ch.AxisX.Add(new Axis
            {
                Title = "Bulan",
                Labels = chartPenjualan.bulan,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            ch.AxisY.Add(new Axis
            {
                Title = "Keuangan",
                LabelFormatter = value => value.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"))
            });


            keuanganChart.Children.Add(ch);
        }

        private void refreshGrafikJumlahObat()
        {
            ChartJumlahObat chartPenjualan = GetChartJumlahObat("sp_chartPenjualan");
            ChartJumlahObat chartPembelian = GetChartJumlahObat("sp_chartPembelian");

            CartesianChart ch = new CartesianChart();
            ch.Foreground = new SolidColorBrush(Colors.Black);
            ch.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Penjualan",
                    Values = new ChartValues<int>(chartPenjualan.jumlah)
                },
                new ColumnSeries
                {
                    Title = "Pembelian",
                    Values = new ChartValues<int>(chartPembelian.jumlah)
                }
            };

            ch.AxisX.Add(new Axis
            {
                Title = "Bulan",
                Labels = chartPenjualan.bulan,
                Separator = new Separator
                {
                    Step = 1
                }
            });

            ch.AxisY.Add(new Axis
            {
                Title = "Jumlah Obat",
            });


            jumlahObatChart.Children.Add(ch);

        }


        private ChartKeuangan GetChartKeuangan(string sp)
        {
            ChartKeuangan chartKeuangan = new ChartKeuangan();
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader RDR = cmd.ExecuteReader();
            while (RDR.Read())
            {
                chartKeuangan.keuangan.Add(Convert.ToDouble(RDR["total"]));
                chartKeuangan.bulan.Add(RDR["bulan"].ToString());
            }
            connection.Close();

            return chartKeuangan;
        }

        private ChartJumlahObat GetChartJumlahObat(string sp)
        {
            ChartJumlahObat chartJumlahObat = new ChartJumlahObat();

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);
            connection.Open();
            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader RDR = cmd.ExecuteReader();
            while (RDR.Read())
            {
                chartJumlahObat.jumlah.Add(Convert.ToInt32(RDR["jumlah"]));
                chartJumlahObat.bulan.Add(RDR["bulan"].ToString());
            }
            connection.Close();

            return chartJumlahObat;
        }

        private void loadSummaryData()
        {
            lblUsername2.Text = Application.Current.Properties["nama"].ToString();
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConString"]);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_Dashboard_CountPenjualan", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader RDR = cmd.ExecuteReader();
            while (RDR.Read())
            {
                lblPenjualan.Text = RDR["Jumlah_Penjualan"].ToString() + " / " + RDR["JumlahObatTerjual"].ToString();
                lblPenjualan.ToolTip = RDR["Jumlah_Penjualan"].ToString() + " Transaksi / " + RDR["JumlahObatTerjual"].ToString() + " Obat";
            }
            connection.Close();

            connection.Open();
            cmd = new SqlCommand("sp_Dashboard_CountPembelian", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            RDR = cmd.ExecuteReader();
            while (RDR.Read())
            {
                lblPembelian.Text = RDR["Jumlah_Pembelian"].ToString() + " / " + RDR["JumlahObatDibeli"].ToString();
                lblPembelian.ToolTip = RDR["Jumlah_Pembelian"].ToString() + " Transaksi / " + RDR["JumlahObatDibeli"].ToString() + " Obat";
            }
            connection.Close();

            connection.Open();
            cmd = new SqlCommand("sp_Dashboard_CountReservasi", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            RDR = cmd.ExecuteReader();
            while (RDR.Read())
            {
                lblReservasi.Text = RDR["Jumlah_Reservasi"].ToString();
            }
            connection.Close();

            connection.Open();
            cmd = new SqlCommand("sp_Dashboard_CountObat", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            RDR = cmd.ExecuteReader();
            while (RDR.Read())
            {
                lblResep.Text = RDR["Jumlah_Obat"].ToString();
            }
            connection.Close();
        }
    }
}
