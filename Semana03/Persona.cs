using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Semana03
{
    public partial class Persona : Form
    {
        SqlConnection conn;
        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                String sql = "SELECT * FROM tbl_usuario";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
            }
            else
            {
                MessageBox.Show("La conexión está cerrada");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           
            if (conn.State == ConnectionState.Open)
            {
                /*
                String FirstName = txtNombre.Text;

                SqlCommand cmd =  new SqlCommand();
                cmd.CommandText = "BuscarPersonaNombre";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@FirstName";
                param.Value = FirstName;

                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
                */
                //Forma Conectada Procedimiento Almacenado -- DATAREADER
                List<User> people = new List<User>();

                SqlCommand command = new SqlCommand("BuscarPersonaNombre", conn);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Size = 50;
                parameter1.Value = txtNombre.Text.Trim();
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
                dgvListado.DataSource = people;
                dataReader.Close();
            }          
        }
    }
}
