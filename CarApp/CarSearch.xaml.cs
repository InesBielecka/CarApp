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

namespace CarApp
{
    /// <summary>
    /// Interaction logic for CarSearch.xaml
    /// </summary>
    public partial class CarSearch : Window
    {
        public CarSearch()
        {
            InitializeComponent();
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand filldatagridwithcars = new SqlCommand("Select * from tblCars", con);
                con.Open();
                SqlDataReader reader;
                reader = filldatagridwithcars.ExecuteReader();
                dataGrid.ItemsSource = reader;
            }
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
