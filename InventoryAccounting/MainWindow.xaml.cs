using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
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

namespace InventoryAccounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OleDbDataAdapter adapter;
        public DataSet inventoryTable;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string request = "select * from [Основные средства]";

            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                adapter = new OleDbDataAdapter(request, connection);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
                inventoryTable = new DataSet();
                adapter.Fill(inventoryTable, "[Основные средства]");
                InventoryGrid.ItemsSource = inventoryTable.Tables["[Основные средства]"].DefaultView;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            addForm add = new addForm();
            add.Show();
        }
    }
}
