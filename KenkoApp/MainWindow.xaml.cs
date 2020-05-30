using MaterialDesignThemes.Wpf;
using KenkoApp.uc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KenkoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void WindowMouseLeft(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}

        //private void btnMaximize_Close(object sender, RoutedEventArgs e)
        //{
        //    if (WindowState == WindowState.Maximized)
        //    {
        //        buttonAppStack.Margin = new Thickness(0);
        //        WindowState = WindowState.Normal;
        //        maximizeIcon.Kind = PackIconKind.WindowMaximize;
        //    } else
        //    {
        //        buttonAppStack.Margin = new Thickness(3, 3, 5, 0);
        //        ContentArea.Margin = new Thickness(10, 0, 0, 0);
        //        WindowState = WindowState.Maximized;
        //        maximizeIcon.Kind = PackIconKind.WindowRestore;
        //    }
        //}

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
