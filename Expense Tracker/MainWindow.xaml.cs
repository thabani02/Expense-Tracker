using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Expense_Tracker
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=THABANI123-PC\\SQLEXPRESS;Initial Catalog=ExpenseTracker;Integrated Security=True;TrustServerCertificate=True";

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase(); 
            LoadExpenses();       
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Expenses')
                        CREATE TABLE Expenses (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Category NVARCHAR(255) NOT NULL,
                            Amount DECIMAL(10,2) NOT NULL,
                            Date DATE NOT NULL DEFAULT GETDATE()
                        );";

                    using (var command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing database: {ex.Message}");
            }
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            string category = CategoryTextBox.Text;
            if (decimal.TryParse(AmountTextBox.Text, out decimal amount))
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand("EXEC AddExpense @Category, @Amount", connection))
                        {
                            command.Parameters.AddWithValue("@Category", category);
                            command.Parameters.AddWithValue("@Amount", amount);
                            command.ExecuteNonQuery();
                        }
                    }
                    LoadExpenses(); // Refresh the list after adding
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding expense: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Invalid amount. Please enter a valid number.");
            }
        }

        private void LoadExpenses()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Expenses";
                    using (var adapter = new SqlDataAdapter(selectQuery, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        ExpensesDataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expenses: {ex.Message}");
            }
        }

        

    }
}
