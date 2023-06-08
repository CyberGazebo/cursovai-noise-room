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
    /// Логика взаимодействия для AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        private bool isEditMode;
        private clients selectedClient;

        public AddEditClientPage()
        {
            InitializeComponent();

            isEditMode = false;
            selectedClient = null;
        }

        public AddEditClientPage(clients client)
        {
            InitializeComponent();

            isEditMode = true;
            selectedClient = client;

            // Заполнение полей данными выбранного клиента
            FirstNameTextBox.Text = client.client_first_name;
            LastNameTextBox.Text = client.client_last_name;
            PhoneTextBox.Text = client.client_phone;
            EmailTextBox.Text = client.client_email;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                using (NoizeRoomEntities context = NoizeRoomEntities.GetContext())
                {
                    if (isEditMode)
                    {
                        // Редактирование существующего клиента
                        selectedClient.client_first_name = FirstNameTextBox.Text;
                        selectedClient.client_last_name = LastNameTextBox.Text;
                        selectedClient.client_phone = PhoneTextBox.Text;
                        selectedClient.client_email = EmailTextBox.Text;

                        context.Entry(selectedClient).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        // Создание нового клиента
                        clients newClient = new clients
                        {
                            client_first_name = FirstNameTextBox.Text,
                            client_last_name = LastNameTextBox.Text,
                            client_phone = PhoneTextBox.Text,
                            client_email = EmailTextBox.Text
                        };

                        context.clients.Add(newClient);
                    }

                    // Сохранение изменений в базе данных
                    context.SaveChanges();

                    MessageBox.Show("Данные клиента сохранены успешно!");

                    // Переход на страницу со списком клиентов
                    ClientsListPage clientsListPage = new ClientsListPage();
                    NavigationService.Navigate(clientsListPage);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя клиента.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите фамилию клиента.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер телефона клиента.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите адрес электронной почты клиента.");
                return false;
            }

            return true;
        }
    }
}
