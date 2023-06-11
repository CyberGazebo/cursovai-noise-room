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
    /// Логика взаимодействия для ClientHistoryPage.xaml
    /// </summary>
    public partial class ClientHistoryPage: Page
    {
        private string connectionString;
        private int clientId;

        public ClientHistoryPage(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            LoadOrderHistory();
        }

        private void LoadOrderHistory()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для получения истории заказов клиента
                    string query = "SELECT * FROM Order WHERE client_id = @ClientId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ClientId", clientId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Устанавливаем источник данных для таблицы
                    OrderHistoryDataGrid.ItemsSource = dataTable.DefaultView;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке истории заказов клиента: " + ex.Message);
            }
        }
    }
}
