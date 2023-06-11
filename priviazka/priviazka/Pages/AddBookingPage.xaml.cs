using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace priviazka
{
    public partial class AddBookingPage : Page
    {
        private string connectionString = "noiseroomEntities"; 

        private List<Clients> clients; // Список клиентов для комбобокса

        public AddBookingPage()
        {
            InitializeComponent();

            LoadClientsAsync(); 
        }

        private async void LoadClientsAsync()
        {
            clients = new List<Clients>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Запрос на получение списка клиентов
                    string query = "SELECT client_id, ClientName FROM Clients ORDER BY client_name";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int clientId = (int)reader["client_id"];
                        string clientName = (string)reader["client_name"];
                    }

                    connection.Close();
                }

                // Заполнение комбобокса клиентами
                ClientComboBox.ItemsSource = clients;
                ClientComboBox.DisplayMemberPath = "ClientName";
                ClientComboBox.SelectedValuePath = "ClientId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке списка клиентов: " + ex.Message);
            }
        }

        private async void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного клиента
            Clients selectedClient = ClientComboBox.SelectedItem as Clients;
            if (selectedClient == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            // Получение выбранной даты и времени начала и окончания бронирования
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;
            TimeSpan startTime = TimeSpan.Parse(StartTimeTextBox.Text);
            TimeSpan endTime = TimeSpan.Parse(EndTimeTextBox.Text);

            // Создание объекта бронирования
            Schedule booking = new Schedule
            {
                schedule_date = selectedDate,
                schedule_start_time = startTime,
                schedule_end_time = endTime,
                client_id = selectedClient.client_id
            };

            // Сохранение бронирования в базе данных
            await SaveBookingToDatabaseAsync(booking);

            MessageBox.Show("Бронь успешно добавлена!");

            // Очистка полей формы
            DatePicker.SelectedDate = null;
            StartTimeTextBox.Text = string.Empty;
            EndTimeTextBox.Text = string.Empty;
            ClientComboBox.SelectedItem = null;

            // Переход на страницу бронирования
            NavigationService?.Navigate(new BookingPage());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу бронирования
            NavigationService?.GoBack();
        }

        private async Task SaveBookingToDatabaseAsync(Schedule booking)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Вставка данных бронирования в таблицу
                    string query = "INSERT INTO Orders (schedule_date, schedule_start_time, schedule_end_time, client_id) " +
                        "VALUES (@ScheduleDate, @StartTime, @EndTime, @ClientId)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ScheduleDate", booking.schedule_date);
                    command.Parameters.AddWithValue("@StartTime", booking.schedule_start_time);
                    command.Parameters.AddWithValue("@EndTime", booking.schedule_end_time);
                    command.Parameters.AddWithValue("@ClientId", booking.client_id);

                    await command.ExecuteNonQueryAsync();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении бронирования: " + ex.Message);
            }
        }
    }
}
