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
        private SqlConnection connection;

        public BookingPage()
        {
            InitializeComponent();
            connection = new SqlConnection("noiseroomEntities");
            LoadSchedules();
            StartTimer();
        }

        private void LoadSchedules()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Schedule WHERE schedule_availability_status = 1", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке расписаний: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
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
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Schedule SET schedule_availability_status = '1' WHERE schedule_availability_status = 'Active' AND schedule_end_time <= GETDATE()", connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Обновлены статусы завершенных бронирований.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обновлении статуса бронирований: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            DataRowView dataRow = button.Tag as DataRowView;
            if (dataRow != null)
            {
                int scheduleId = (int)dataRow["schedule_id"];
                AddEditBookingPage editBookingPage = new AddEditBookingPage(scheduleId);
                NavigationService.Navigate(editBookingPage);
            }
        }

        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            forConectioninPages.mainFraime.Navigate(new AddBookingPage());
        }
    }
}
