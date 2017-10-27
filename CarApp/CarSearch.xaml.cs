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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CarApp
{
    /// <summary>
    /// Interaction logic for CarSearch.xaml
    /// </summary>
    public partial class CarSearch : Window
    {
        private DataTable _carsTable;

        public CarSearch()
        {
            InitializeComponent();
            
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand filldatagridwithcars = new SqlCommand("ShowCars", con);
                filldatagridwithcars.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(filldatagridwithcars);
                _carsTable = new DataTable("tblCars");
                da.Fill(_carsTable);
                dataGrid.ItemsSource = _carsTable.DefaultView;
            }
        }
       
        private void SortcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           _carsTable.DefaultView.Sort = SortcomboBox.SelectedValue.ToString();
        }

        private void SortcomboBox_Loaded(object sender, RoutedEventArgs e)
        {
            SortcomboBox.Items.Add("Car brand");
            SortcomboBox.Items.Add("Engine");
            SortcomboBox.Items.Add("Production Year");
            SortcomboBox.Items.Add("Mileage");
        }
    }
}

