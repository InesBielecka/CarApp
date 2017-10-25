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
using System.Configuration;
using System.Data.SqlClient;

namespace CarApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillBrandNameComboBox();
            FillBodyColourComboBox();
            FillEngineCapacityComboBox();
            FillProductionYearComboBox();
            BrandNameComboBox.SelectedIndex = 0;

        }

        private void FillBrandNameComboBox()
        {
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand fillcomboboxwithbrands = new SqlCommand("Select [Brand name] from tblCarBrands", con);
                con.Open();
                SqlDataReader reader;
                reader = fillcomboboxwithbrands.ExecuteReader();
                while (reader.Read())
                {
                    BrandNameComboBox.Items.Add(reader[0]);
                }
            }
        }

        private void FillProductionYearComboBox()
        {
            for (int year = 1980; year <= DateTime.Now.Year; year++)
            {
                ProductionYearComboBox.Items.Add(year);
            }
        }

        private void FillEngineCapacityComboBox()
        {
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand fillcomboboxwithcapacity = new SqlCommand("Select [Engine capacity] from tblEngine", con);
                con.Open();
                SqlDataReader reader;
                reader = fillcomboboxwithcapacity.ExecuteReader();
                while (reader.Read())
                {
                    EngineCapacityComboBox.Items.Add(reader[0]);
                }
            }
        }

        private void FillBodyColourComboBox()
        {
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand fillcomboboxwithcolours = new SqlCommand("Select [Body colour] from tblBodyColour", con);
                con.Open();
                SqlDataReader reader;
                reader = fillcomboboxwithcolours.ExecuteReader();
                while (reader.Read())
                {
                    BodyColourComboBox.Items.Add(reader[0]);
                }
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand addcmd = new SqlCommand("AddNewCar", con);
                addcmd.CommandType = System.Data.CommandType.StoredProcedure;

                addcmd.Parameters.AddWithValue("@CarBrand", BrandNameComboBox.Text);
                addcmd.Parameters.AddWithValue("@ProductionYear", ProductionYearComboBox.Text);
                addcmd.Parameters.AddWithValue("@Mileage", MileageTextBox.Text);
                addcmd.Parameters.AddWithValue("@Engine", float.Parse(EngineCapacityComboBox.Text));
                addcmd.Parameters.AddWithValue("@BodyColour", BodyColourComboBox.Text);
                addcmd.Parameters.AddWithValue("@AC", ACCheckBox.IsChecked);
                addcmd.Parameters.AddWithValue("@Radio", RadioCheckBox.IsChecked);
                addcmd.Parameters.AddWithValue("@CentralLock", CentralLockCheckBox.IsChecked);
                addcmd.Parameters.AddWithValue("@ElectronicWindows", ElectronicWindowsCheckBox.IsChecked);

                SqlParameter IDParameter = new SqlParameter();
                IDParameter.ParameterName = "@ID";
                IDParameter.SqlDbType = System.Data.SqlDbType.Int;
                IDParameter.Direction = System.Data.ParameterDirection.Output;
                addcmd.Parameters.Add(IDParameter);

                con.Open();
                addcmd.ExecuteNonQuery();


            }
            messagelbl.Content = "Add car successfully";
        }

        private void ACCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BrandNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Mileage_GotFocus(object sender, RoutedEventArgs e)
        {
            MileageTextBox.Text = string.Empty;
        }

        private void MileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CarSearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new CarSearch();
            searchWindow.Show();
        }
    }
}
