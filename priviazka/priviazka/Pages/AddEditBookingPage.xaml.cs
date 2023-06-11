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

        public partial class AddEditBookingPage : Page
        {
            private int scheduleId;
            private string connectionString = "noiseroomEntities"; // Замените на свою строку подключения к базе данных

            public AddEditBookingPage(int scheduleId)
            {
                InitializeComponent();
                this.scheduleId = scheduleId;
                LoadScheduleDetails();
            }

            private void LoadScheduleDetails()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Запрос для получения данных о расписании
                        string query = "SELECT * FROM schedules WHERE schedule_id = @ScheduleId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Заполнение полей данными о расписании
                            DateTime scheduleDate = (DateTime)reader["schedule_date"];
                            TimeSpan scheduleStartTime = (TimeSpan)reader["schedule_start_time"];
                            TimeSpan scheduleEndTime = (TimeSpan)reader["schedule_end_time"];

                            ScheduleDatePicker.SelectedDate = scheduleDate;
                            StartTimeTextBox.Text = scheduleStartTime.ToString();
                            EndTimeTextBox.Text = scheduleEndTime.ToString();
                        }

                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при загрузке данных о расписании: " + ex.Message);
                }
            }

            private void UpdateSchedule()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Обновление данных о расписании
                        string query = "UPDATE schedules SET schedule_date = @ScheduleDate, schedule_start_time = @ScheduleStartTime, schedule_end_time = @ScheduleEndTime WHERE schedule_id = @ScheduleId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ScheduleDate", ScheduleDatePicker.SelectedDate);
                        command.Parameters.AddWithValue("@ScheduleStartTime", TimeSpan.Parse(StartTimeTextBox.Text));
                        command.Parameters.AddWithValue("@ScheduleEndTime", TimeSpan.Parse(EndTimeTextBox.Text));
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Расписание успешно обновлено!");

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при обновлении расписания: " + ex.Message);
                }
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
                UpdateSchedule();
            }
        }
    }

