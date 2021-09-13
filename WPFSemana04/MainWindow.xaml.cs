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
using System.Data;
using System.Data.SqlClient;

namespace WPFSemana04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9UBQPUR\\SQLEXPRESS; Initial Catalog=db_lab04;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            List<User> people = new List<User>();

            SqlCommand command = new SqlCommand("BuscarPersonaNombre", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            //parameter1.Value = txtNombre.Text.Trim();
            parameter1.Value = "";
            parameter1.ParameterName = "@FirstName";

            command.Parameters.Add(parameter1);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new User
                {
                    usuario_id = dataReader[0].ToString(),
                    usuario_nombre = dataReader[1].ToString(),
                    usuario_password = dataReader[2].ToString(),
                    usuario_fecha_registro = dataReader[3].ToString(),
                });

            }

            conn.Close();
            dgvUsers.ItemsSource = people;
        }

        private void dgvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
