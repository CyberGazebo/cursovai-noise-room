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
        private Clients selectedClient = new Clients();

        public AddEditClientPage()
        {
            InitializeComponent();

            isEditMode = false;
            selectedClient = null;
        }

        public AddEditClientPage(int clientId) // Изменен тип параметра на int
        {
            InitializeComponent();

            isEditMode = true;

            using (noiseroomEntities context = noiseroomEntities.GetContext())
            {
                selectedClient = context.Clients.Find(clientId); // Получение объекта Clients по идентификатору

                if (selectedClient != null)
                {
                    // Заполнение полей данными выбранного клиента
                    FirstNameTextBox.Text = selectedClient.client_name;
                    PhoneTextBox.Text = selectedClient.client_phone;
                    EmailTextBox.Text = selectedClient.client_email;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                using (noiseroomEntities context = noiseroomEntities.GetContext())
                {
                    if (isEditMode)
                    {
                        // Редактирование существующего клиента
                        selectedClient.client_name = FirstNameTextBox.Text;
                        selectedClient.client_phone = PhoneTextBox.Text;
                        selectedClient.client_email = EmailTextBox.Text;

                        context.Entry(selectedClient).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        // Создание нового клиента
                        Clients newClient = new Clients
                        {
                            client_name = FirstNameTextBox.Text,
                            client_phone = PhoneTextBox.Text,
                            client_email = EmailTextBox.Text
                        };

                        context.Clients.Add(newClient);
                    }

                    // Сохранение изменений в базе данных
                    context.SaveChanges();

                    MessageBox.Show("Данные клиента сохранены успешно!");

                    // Переход на страницу со списком клиентов
                    NavigationService?.Navigate(new AllClientPage());
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите ФИО клиента.");
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
