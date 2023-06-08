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
    /// Логика взаимодействия для AddBookingPage.xaml
    /// </summary>
    public partial class AddBookingPage : Page
    {
        private string connectionString = "YourConnectionString"; // Замените на свою строку подключения к базе данных

        public AddBookingPage()
        {
            InitializeComponent();
        }

        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
    {
        // Получение выбранной даты и времени начала и окончания бронирования
        DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.MinValue;
        TimeSpan startTime = StartTimePicker.SelectedTime ?? TimeSpan.Zero;
        TimeSpan endTime = EndTimePicker.SelectedTime ?? TimeSpan.Zero;

        // Создание объекта бронирования
        Booking booking = new Booking
        {
            ScheduleDate = selectedDate,
            StartTime = startTime,
            EndTime = endTime
        };

        // Сохранение бронирования в базе данных
        SaveBookingToDatabase(booking);

        MessageBox.Show("Бронь успешно добавлена!");

        // Очистка полей формы
        DatePicker.SelectedDate = null;
        StartTimePicker.SelectedTime = null;
        EndTimePicker.SelectedTime = null;

        // Переход на страницу бронирования
        NavigationService?.Navigate(new BookingPage());
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу бронирования
        NavigationService?.GoBack();
    }

}
