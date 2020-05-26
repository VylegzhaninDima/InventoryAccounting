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
using System.Windows.Shapes;

namespace InventoryAccounting
{
    /// <summary>
    /// Логика взаимодействия для addForm.xaml
    /// </summary>
    public partial class addForm : Window
    {
        public addForm()
        {
            InitializeComponent();
            FillComboBox();
        }
        public void FillComboBox()
        {
            string queryAccount = "select * from [Счет]";
            string queryKFO = "select * from [КФО]";
            string queryKPS = "select * from [КПС]";
            string queryMOL = "select * from [МОЛ]";
           // string queryDepricationGroup = "select * from [Амортизационная группа]";
            string queryDepricationMethod = "select * from [Способ начисления амортизации]";
            string queryState = "select * from [Состояние]";
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();

                connection.Open();

                OleDbCommand accountCommand = new OleDbCommand(queryAccount, connection);
                OleDbCommand KFOCommand = new OleDbCommand(queryKFO, connection);
                OleDbCommand KPSCommand = new OleDbCommand(queryKPS, connection);
                OleDbCommand MOLCommand = new OleDbCommand(queryMOL, connection);
               // OleDbCommand depricationGroupCommand = new OleDbCommand(queryDepricationGroup, connection);
                OleDbCommand depricationMethodCommand = new OleDbCommand(queryDepricationMethod, connection);
                OleDbCommand stateCommand = new OleDbCommand(queryState, connection);


                OleDbDataReader accountReader = accountCommand.ExecuteReader();
                OleDbDataReader KFOReader = KFOCommand.ExecuteReader();
                OleDbDataReader KPSReader = KPSCommand.ExecuteReader();
                OleDbDataReader MOLReader = MOLCommand.ExecuteReader();
              //  OleDbDataReader depricationGroupReader = depricationGroupCommand.ExecuteReader();
                OleDbDataReader depricationMethodReader = depricationMethodCommand.ExecuteReader();
                OleDbDataReader stateReader = stateCommand.ExecuteReader();

                while (accountReader.Read()){
                    string account = accountReader.GetString(0);
                    AccountCBox.Items.Add(account);
                }

                while (KFOReader.Read())
                {
                    string KFO = KFOReader.GetString(0);
                    KFOCBox.Items.Add(KFO);
                }

                while (KPSReader.Read())
                {
                    string KPS = KPSReader.GetString(0);
                    KPSCBox.Items.Add(KPS);
                }

                while (MOLReader.Read())
                {
                    string MOL = MOLReader.GetString(0);
                    MOLCBox.Items.Add(MOL);
                }

                //while (depricationGroupReader.Read())
                //{
                //    string depricationGroup = depricationGroupReader.GetString(1);
                //    DepricationGroupCBox.Items.Add(depricationGroup);
                //}

                while (depricationMethodReader.Read())
                {
                    string depricationMethod = depricationMethodReader.GetString(0);
                    DepricationMethodCBox.Items.Add(depricationMethod);
                }
                while (stateReader.Read())
                {
                    string state = stateReader.GetString(0);
                    StateCBox.Items.Add(state);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void AddToDB_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into [Основные средства] (Счет,КФО,КПС,МОЛ,ОС," +
                "[Инвентарный номер],ОКОФ,[Амортизационная группа],[Способ начисления амортизации]," +
                "[Дата принятия к учету],Состояние,[Месячная норма %],[Срок полезного использования]," +
                "[Процент износа],Количество,[Балансовая стоимость]) values (@account,@kfo,@kps,@mol,@os," +
                "@inventoryNumber,@okof,@amGroup,@methodAm,@date,@state,@monthly,@usefull,@percent,@ammount,@bookValue)";
            try
            {
                OleDbConnection connection = new OleDbConnection();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand addCommand = new OleDbCommand(query,connection);

                addCommand.Parameters.Add("@account", OleDbType.VarChar).Value = AccountCBox.Text;
                addCommand.Parameters.Add("@kfo", OleDbType.VarChar).Value = KFOCBox.Text;
                addCommand.Parameters.Add("@kps", OleDbType.Integer).Value = KPSCBox.Text;
                addCommand.Parameters.Add("@mol", OleDbType.VarChar).Value = MOLCBox.Text;
                addCommand.Parameters.Add("@os", OleDbType.VarWChar).Value = FixedAssets.Text;
                addCommand.Parameters.Add("@inventoryNumber", OleDbType.Integer).Value = Convert.ToInt32(InventoryNumber.Text);
                addCommand.Parameters.Add("@okof", OleDbType.Integer).Value = Convert.ToInt32(OKOF.Text);
                addCommand.Parameters.Add("@amGroup", OleDbType.Integer).Value = Convert.ToInt32(DepricationGroup.Text);
                addCommand.Parameters.Add("@methodAm", OleDbType.VarChar).Value = DepricationMethodCBox.Text;
                addCommand.Parameters.Add("@date", OleDbType.DBDate).Value = DateOfAcceptance.Text;
                addCommand.Parameters.Add("@state", OleDbType.VarChar).Value = StateCBox.Text;
                addCommand.Parameters.Add("@monthly", OleDbType.Double).Value = Convert.ToDouble(MonthlyRate.Text);
                addCommand.Parameters.Add("@usefull", OleDbType.Integer).Value = Convert.ToInt32(UsefulInMonth.Text);
                addCommand.Parameters.Add("@percent", OleDbType.Integer).Value = Convert.ToInt32(PercentageOfWear.Text);
                addCommand.Parameters.Add("@ammount", OleDbType.Integer).Value = Convert.ToInt32(Quantity.Text);
                addCommand.Parameters.Add("@bookValue", OleDbType.Integer).Value = Convert.ToInt32(BookValue.Text);

                addCommand.ExecuteNonQuery();
                MessageBox.Show("Успешно");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }

        }
    }
}
