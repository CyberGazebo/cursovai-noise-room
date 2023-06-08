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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введенные данные для регистрации
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Проверяем совпадение паролей
            if (password == confirmPassword)
            {
                // Создаем файл "Аккаунт.txt" и записываем в него данные
                string accountData = $"{login}\n{password}\n{confirmPassword}";
                File.WriteAllText("Аккаунт.txt", accountData);

                forConectioninPages.mainFraime.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show("Пароли не совпадают. Попробуйте еще раз.");
            }
        }
    }
}
