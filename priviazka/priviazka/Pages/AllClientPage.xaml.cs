using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace priviazka
{
    /// <summary>
    /// Логика взаимодействия для AllClientPage.xaml
    /// </summary>
    public partial class AllClientPage : Page
    {
        private string noiseroomEntities; // Замените на свою строку подключения к базе данных

        public AllClientPage()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(noiseroomEntities))
                {
                    connection.Open();

                    // Запрос для получения всех клиентов
                    string query = "SELECT * FROM Сlients";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Устанавливаем источник данных для таблицы
                    ClientsDataGrid.ItemsSource = dataTable.DefaultView;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке клиентов: " + ex.Message);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный клиент
            DataRowView selectedRow = (DataRowView)ClientsDataGrid.SelectedItem;
            if (selectedRow != null)
            {
                // Получаем данные клиента
                int clientId = (int)selectedRow["client_id"];

                // Переходим на страницу редактирования клиента
                NavigationService?.Navigate(new AddEditClientPage(clientId));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.");
            }
        }


        private void ViewHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный клиент
            DataRowView selectedRow = (DataRowView)ClientsDataGrid.SelectedItem;
            if (selectedRow != null)
            {
                // Получаем данные клиента
                int clientId = (int)selectedRow["client_id"];

                // Переходим на страницу истории заказов клиента
                NavigationService?.Navigate(new ClientHistoryPage(clientId));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для просмотра истории заказов.");
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            // Переходим на страницу добавления клиента
            NavigationService?.Navigate(new AddEditClientPage());
        }
    }
}
