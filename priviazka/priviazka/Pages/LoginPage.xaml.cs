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
using System.IO;

namespace priviazka
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введенные данные для авторизации
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            // Проверяем, существует ли файл "Аккаунт.txt"
            if (File.Exists("Аккаунт.txt"))
            {
                // Считываем данные из файла "Аккаунт.txt"
                string[] accountData = File.ReadAllLines("Аккаунт.txt");

                // Проверяем, совпадают ли введенные данные с данными из файла
                if (accountData.Length >= 3 && accountData[0] == login && accountData[1] == password)
                {
                    forConectioninPages.mainFraime.Navigate(new BookingPage());
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
                }
            }
            else
            {
                MessageBox.Show("Файл 'Аккаунт.txt' не найден. Пожалуйста, пройдите регистрацию.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            forConectioninPages.mainFraime.Navigate(new LoginPage());
        }
    }
}
