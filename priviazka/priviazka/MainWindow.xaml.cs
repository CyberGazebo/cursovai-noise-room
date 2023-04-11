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

namespace priviazka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mainframe.Navigate(new ticketpage());
            forConectioninPages.mainFraime = Mainframe;
        }

        private void btnCLoseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            forConectioninPages.mainFraime.GoBack();
        }

        private void Mainframe_ContentRendered(object sender, EventArgs e)
        {
            if (forConectioninPages.mainFraime.CanGoBack)
            {
                this.ResizeMode = ResizeMode.CanResize;
                btnback.Visibility = Visibility.Visible;
            }
            else
            {
                this.ResizeMode=ResizeMode.NoResize;
                btnback.Visibility=Visibility.Collapsed;
            }
        }
    }
}
