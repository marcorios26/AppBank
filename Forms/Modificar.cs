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
    public partial class Modificar : Form
    {
       
        public Modificar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
            int lastId1 = Convert.ToInt32(command1.ExecuteScalar());
            SqlDataReader leer1 = command1.ExecuteReader();
            if (leer1.Read()==true)
            {
                /*********verifica transacciones******/
                string query = "SELECT ID_TRANSACCION FROM TRANSACCION WHERE ID_CUENTA=@ID_CUENTA";

                SqlCommand cmd = new SqlCommand(query, Conexion.Conectar());
                cmd.Parameters.AddWithValue("@ID_CUENTA", lastId1);

                SqlDataReader leer = cmd.ExecuteReader();
                /**********************/
                if (leer.Read() == true)
                {
                    MessageBox.Show("No puede cambiar datos");
                }
                else
                {
                    string query2 = "SELECT TITULAR FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                    SqlCommand command2 = new SqlCommand(query2, Conexion.Conectar());
                    command2.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                    string titulardb = Convert.ToString(command2.ExecuteScalar());
                    textBox2.Text = titulardb;

                    string query3 = "SELECT MONEDA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                    SqlCommand command3 = new SqlCommand(query3, Conexion.Conectar());
                    command3.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                    string monedadb = Convert.ToString(command3.ExecuteScalar());
                    comboBox1.Text = monedadb;

                    string query4 = "SELECT DEPARTAMENTO FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                    SqlCommand command4 = new SqlCommand(query4, Conexion.Conectar());
                    command4.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                    string departamentodb = Convert.ToString(command4.ExecuteScalar());
                    comboBox2.Text = departamentodb;



                }
            }
            else
            {
                MessageBox.Show("LA CUENTA INGRESADA NO EXISTE");
            }
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkfields())
            {

                string query1 = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                command1.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                int lastId1 = Convert.ToInt32(command1.ExecuteScalar());
                ////**********************************
                string x = "";
                string code = Convert.ToString(lastId1);
                int limite = 6 - code.Length;
                if (x.Length < 7)
                {

                    for (int i = 0; i < limite; i++)
                    {
                        x = x + "0";
                    }
                    x = x + code;
                }
                String CuentaB = NumCuenta() + x;
                /**********************************************/
                Conexion.Conectar();

                string cambiar = "UPDATE CUENTA_BANCARIA SET TITULAR=@TITULAR,MONEDA=@MONEDA,DEPARTAMENTO=@DEPARTAMENTO,NUM_CUENTA=@NUM_CUENTA  WHERE ID_CUENTA=@ID_CUENTA";
                SqlCommand commandChange = new SqlCommand(cambiar, Conexion.Conectar());
                commandChange.Parameters.AddWithValue("@TITULAR", textBox2.Text);
                commandChange.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                commandChange.Parameters.AddWithValue("@DEPARTAMENTO", comboBox2.Text);
                commandChange.Parameters.AddWithValue("@NUM_CUENTA", CuentaB);
                commandChange.Parameters.AddWithValue("@ID_CUENTA", lastId1);
                commandChange.ExecuteNonQuery();
                MessageBox.Show("CUENTA MODIFICADA EXITOSAMENTE");
            }
            else
            {
                MessageBox.Show("EXISTE UN ERROR AL LLENAR LOS CAMPOS");
            }

        }

        private String NumCuenta()
        {
            String NumCuenta = "";
            if (comboBox1.Text.Equals("BOLIVIANOS"))
            {
                NumCuenta = NumCuenta + "201" + "-";

            }
            else
            {
                NumCuenta = NumCuenta + "202" + "-";
            }
            switch (comboBox2.Text)
            {
                case "LA PAZ":
                    NumCuenta = NumCuenta + "01" + "-";
                    break;
                case "ORURO":
                    NumCuenta = NumCuenta + "02" + "-";
                    break;
                case "POTOSI":
                    NumCuenta = NumCuenta + "03" + "-";
                    break;
                case "PANDO":
                    NumCuenta = NumCuenta + "04" + "-";
                    break;
                case "SANTA CRUZ":
                    NumCuenta = NumCuenta + "05" + "-";
                    break;
                case "BENI":
                    NumCuenta = NumCuenta + "06" + "-";
                    break;
                case "COCHABAMBA":
                    NumCuenta = NumCuenta + "07" + "-";
                    break;
                case "TARIJA":
                    NumCuenta = NumCuenta + "08" + "-";
                    break;
                case "CHUQUISACA":
                    NumCuenta = NumCuenta + "09" + "-";
                    break;
            }
            return NumCuenta;

        }

        private bool checkfields()
        {
            if (textBox2.Text != " "  & comboBox1.Text != "" & comboBox2.Text != "")
            {
                //titular de cuenta

                int count = 0;
                for (int i = 0; i < textBox2.Text.Length; i++)
                {

                    if (textBox2.Text[i].Equals(' '))
                    {
                        count++;
                    }
                }
                if (count == 1)
                {
                    //moneda
                        if (comboBox1.Text.Equals("BOLIVIANOS") || comboBox1.Text.Equals("DOLARES"))
                        {
                            if (comboBox2.Text.Equals("LA PAZ") || comboBox2.Text.Equals("COCHABAMBA") ||
                                comboBox2.Text.Equals("POTOSI") || comboBox2.Text.Equals("ORURO") ||
                                comboBox2.Text.Equals("PANDO") || comboBox2.Text.Equals("SANTA CRUZ") ||
                                comboBox2.Text.Equals("TARIJA") || comboBox2.Text.Equals("BENI") ||
                                comboBox2.Text.Equals("CHUQUISACA"))

                            {
                                return true;
                            }
                        }
                    
                }


            }
            return false;
        }
    }   
}
