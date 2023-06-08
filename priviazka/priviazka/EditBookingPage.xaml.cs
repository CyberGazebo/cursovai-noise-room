using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для EditBookingPage.xaml
    /// </summary>
    namespace priviazka
    {
        public partial class EditBookingPage : Page
        {
            private int scheduleId;
            private string connectionString = "YourConnectionString"; // Замените на свою строку подключения к базе данных

            public EditBookingPage(int scheduleId)
            {
                InitializeComponent();
                this.scheduleId = scheduleId;
                LoadBookingDetails();
            }

            private void LoadBookingDetails()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Запрос для получения данных о бронировании
                        string query = "SELECT * FROM bookings WHERE schedule_id = @ScheduleId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Заполнение полей данными о бронировании
                            DateTime bookingDate = (DateTime)reader["booking_date"];
                            TimeSpan bookingStartTime = (TimeSpan)reader["booking_start_time"];
                            TimeSpan bookingEndTime = (TimeSpan)reader["booking_end_time"];

                            BookingDatePicker.SelectedDate = bookingDate;
                            StartTimePicker.SelectedTime = bookingStartTime;
                            EndTimePicker.SelectedTime = bookingEndTime;
                        }

                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при загрузке данных о бронировании: " + ex.Message);
                }
            }

            private void UpdateBooking()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Обновление данных о бронировании
                        string query = "UPDATE bookings SET booking_date = @BookingDate, booking_start_time = @BookingStartTime, booking_end_time = @BookingEndTime WHERE schedule_id = @ScheduleId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@BookingDate", BookingDatePicker.SelectedDate);
                        command.Parameters.AddWithValue("@BookingStartTime", StartTimePicker.SelectedTime);
                        command.Parameters.AddWithValue("@BookingEndTime", EndTimePicker.SelectedTime);
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Бронирование успешно обновлено!");

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при обновлении бронирования: " + ex.Message);
                }
            }

            private void DeleteBooking()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Удаление бронирования
                        string query = "DELETE FROM bookings WHERE schedule_id = @ScheduleId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Бронирование успешно удалено!");

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении бронирования: " + ex.Message);
                }
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
                UpdateBooking();
            }

            private void DeleteButton_Click(object sender, RoutedEventArgs e)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить это бронирование?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteBooking();
                    // Вернуться на страницу бронирования после удаления
                    NavigationService.Navigate(new BookingPage());
                }
            }
        }
    }
}
