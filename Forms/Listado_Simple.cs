using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace BankApplication.Forms
{
    public partial class Listado_Simple : Form
    {
        public Listado_Simple()
        {
            InitializeComponent();
        }

        private void Listado_Simple_Load(object sender, EventArgs e)
        {
            

            

        }

        public DataTable llenar_grid()

        {
            Conexion.Conectar();
            
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM CUENTA_BANCARIA WHERE NUM_CUENTA = '"+textBox2.Text+"'";
            
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            dataGridView1.DataSource = llenar_grid();
        }
    }
}
