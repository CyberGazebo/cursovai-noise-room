using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace priviazka
{
    public partial class BookingPage : Page
    {
        private string connectionString = "YourConnectionString"; // Замените на свою строку подключения к базе данных
        private DispatcherTimer timer;

        public BookingPage()
        {
            InitializeComponent();
            LoadSchedules();
            StartTimer();
        }

        private void LoadSchedules()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для получения доступных расписаний
                    string query = "SELECT * FROM schedule WHERE schedule_availability_status = 1";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Устанавливаем источник данных для таблицы
                    ScheduleDataGrid.ItemsSource = dataTable.DefaultView;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке расписаний: " + ex.Message);
            }
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateBookingStatus();
        }

        private void UpdateBookingStatus()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE bookings SET booking_status = 'Completed' WHERE booking_status = 'Active' AND booking_end_time <= GETDATE()";

                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Обновлены статусы завершенных бронирований.");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обновлении статуса бронирований: " + ex.Message);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            DataRowView selectedRow = (DataRowView)button.Tag;
            if (selectedRow != null)
            {
                int scheduleId = (int)selectedRow["schedule_id"];
                // Открываем страницу редактирования бронирования с передачей идентификатора расписания
                EditBookingPage editBookingPage = new EditBookingPage(scheduleId);
                NavigationService.Navigate(editBookingPage);
            }
        }

        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            forConectioninPages.mainFraime.Navigate(new AddBookingPage());
        }
    }
}
