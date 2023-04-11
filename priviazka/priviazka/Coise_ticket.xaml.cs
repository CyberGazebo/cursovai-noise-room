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
    /// Логика взаимодействия для Coise_ticket.xaml
    /// </summary>
    public partial class Coise_ticket : Page
    {
        public Coise_ticket()
        {
            InitializeComponent();
            //if ((forConectioninPages.dataAdmin = "admin") (forConectioninPages.dataPassAdmin="admin"))
            //{
            //    Adminlist.Visibility= Visibility.Visible;
            //}
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChoisen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы успешно зарегистрировались на мероприятие","", MessageBoxButton.OK) ; ;
        }
    }
}
