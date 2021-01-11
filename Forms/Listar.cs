using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace BankApplication.Forms
{
    public partial class Listar : Form
    {
        public Listar()
        {
            InitializeComponent();
        }

        private void Listar_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            dataGridView1.DataSource = llenar_grid();
        }
        public DataTable llenar_grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM CUENTA_BANCARIA";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
