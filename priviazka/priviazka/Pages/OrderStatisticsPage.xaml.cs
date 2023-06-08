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
    /// Логика взаимодействия для OrderStatisticsPage.xaml
    /// </summary>
        public partial class OrderStatisticsPage : Page
        {
            private string connectionString = "YourConnectionString"; // Замените на свою строку подключения к базе данных
            private List<Order> orders; // Список заказов

            public OrderStatisticsPage()
            {
                InitializeComponent();
                orders = new List<Order>();
                LoadOrdersFromDatabase();
            }

            private void LoadOrdersFromDatabase()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Запрос для загрузки заказов из базы данных
                        string query = "SELECT * FROM orders";

                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            DateTime orderDate = (DateTime)reader["order_date"];
                            orders.Add(new Order { OrderDate = orderDate });
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при загрузке заказов: " + ex.Message);
                }
            }

            private void UpdateChart()
            {
                DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
                DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;

                // Фильтрация заказов по выбранному периоду дат
                List<Order> filteredOrders = new List<Order>();
                foreach (var order in orders)
                {
                    if (order.OrderDate >= startDate && order.OrderDate <= endDate)
                    {
                        filteredOrders.Add(order);
                    }
                }

                // Получение количества заказов для каждой даты
                Dictionary<DateTime, int> orderCounts = new Dictionary<DateTime, int>();
                foreach (var order in filteredOrders)
                {
                    if (orderCounts.ContainsKey(order.OrderDate))
                    {
                        orderCounts[order.OrderDate]++;
                    }
                    else
                    {
                        orderCounts[order.OrderDate] = 1;
                    }
                }

                // Очистка диаграммы
                Chart.Series.Clear();

                // Создание серии диаграммы
                Series series = new Series("OrderCounts");
                series.ChartType = SeriesChartType.Column;

                // Добавление данных в серию
                foreach (var entry in orderCounts)
                {
                    series.Points.AddXY(entry.Key.ToShortDateString(), entry.Value);
                }

                // Добавление серии в диаграмму
                Chart.Series.Add(series);
            }

            private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                UpdateChart();
            }

            private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                UpdateChart();
            }

            private void ExportToExcel_Click(object sender, RoutedEventArgs e)
            {
                // Создание нового экземпляра приложения Excel
                var excelApp = new Microsoft.Office.Interop.Excel.Application();

                // Добавление новой рабочей книги
                var workbook = excelApp.Workbooks.Add();

                // Получение активного листа
                var sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;

                // Запись данных в таблицу Excel
                int row = 1;
                int col = 1;
                sheet.Cells[row, col] = "Дата";
                sheet.Cells[row, col + 1] = "Количество заказов";

                foreach (var entry in orderCounts)
                {
                    row++;
                    sheet.Cells[row, col] = entry.Key.ToShortDateString();
                    sheet.Cells[row, col + 1] = entry.Value;
                }

                // Сохранение файла Excel
                workbook.SaveAs("OrderStatistics.xlsx");

                // Закрытие приложения Excel
                workbook.Close();
                excelApp.Quit();

                // Освобождение ресурсов
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                System.Windows.MessageBox.Show("Данные успешно экспортированы в Excel.");
            }

            private void ExportToWord_Click(object sender, RoutedEventArgs e)
            {
                // Создание нового экземпляра приложения Word
                var wordApp = new Microsoft.Office.Interop.Word.Application();

                // Добавление нового документа
                var document = wordApp.Documents.Add();

                // Получение активного раздела
                var section = document.ActiveWindow.Selection;

                // Запись данных в таблицу Word
                section.TypeText("Дата\tКоличество заказов\n");

                foreach (var entry in orderCounts)
                {
                    section.TypeText($"{entry.Key.ToShortDateString()}\t{entry.Value}\n");
                }

                // Сохранение файла Word
                document.SaveAs2("OrderStatistics.docx");

                // Закрытие приложения Word
                document.Close();
                wordApp.Quit();

                // Освобождение ресурсов
                System.Runtime.InteropServices.Marshal.ReleaseComObject(section);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);

                System.Windows.MessageBox.Show("Данные успешно экспортированы в Word.");
            }
        }
}
