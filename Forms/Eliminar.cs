using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BankApplication.Forms
{
    public partial class Eliminar : Form
    {
        public Eliminar()
        {
            InitializeComponent();
        }

        private void Eliminar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "DELETE FROM CUENTA_BANCARIA WHERE NUM_CUENTA = @CODIGO";
            SqlCommand cmd3 = new SqlCommand(eliminar, Conexion.Conectar());
            cmd3.Parameters.AddWithValue("@CODIGO", textBox1.Text);
            cmd3.ExecuteNonQuery();
            MessageBox.Show("La cuenta fue eliminada exitosamente");

        }
    }
}
