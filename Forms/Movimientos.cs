using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Globalization;

namespace BankApplication.Forms
{
    public partial class Movimientos : Form
    {

       // public System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        public Movimientos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();

            string cuenta = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand comcuenta = new SqlCommand(cuenta, Conexion.Conectar());
            comcuenta.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
            int idcuenta = Convert.ToInt32(comcuenta.ExecuteScalar());

            string query1 = "SELECT SALDO FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
            string montoactualC = Convert.ToString(command1.ExecuteScalar());
            montoactualC = montoactualC.Replace(",",".");

            double montoactual = TextoaDouble(montoactualC);
            double monto = TextoaDouble(textBox2.Text);

        

            string query3 = "SELECT MONEDA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand command3 = new SqlCommand(query3, Conexion.Conectar());
            command3.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
            string monedareg = Convert.ToString(command3.ExecuteScalar());


            double saldo;
            if (comboBox1.Text.Equals("ABONO"))
            {
                if (comboBox2.Text.Equals(monedareg))
                {

                    saldo = (montoactual + monto);
                    string query2 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                    SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());
                    cmd.Parameters.AddWithValue("@SALDO",saldo);
                    cmd.Parameters.AddWithValue("@NUM_CUENTA",textBox1.Text);
                    cmd.ExecuteNonQuery();
                    
                }
                else
                {
                    if (monedareg.Equals("BOLIVIANOS") && comboBox2.Text.Equals("DOLARES"))
                    {
                        saldo = (montoactual + (monto * 6.84));
                        string query2 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                        SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());
                        cmd.Parameters.AddWithValue("@SALDO", saldo);
                        cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (monedareg.Equals("DOLARES") && comboBox2.Text.Equals("BOLIVIANOS"))
                        {
                            saldo = (montoactual + (monto / 6.97));
                            string query2 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());
                            cmd.Parameters.AddWithValue("@SALDO", saldo);
                            cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                Conexion.Conectar();
                
                string transaccion = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                SqlCommand cmd10 = new SqlCommand(transaccion, Conexion.Conectar());
                cmd10.Parameters.AddWithValue("@MONEDA", comboBox2.Text);
                cmd10.Parameters.AddWithValue("@ID_CUENTA", idcuenta);
                cmd10.Parameters.AddWithValue("@TIPO_TRANSACCION",comboBox1.Text );
                cmd10.Parameters.AddWithValue("@MONTO",textBox2.Text);
                cmd10.ExecuteNonQuery();

                MessageBox.Show("se realizo el movimiento exitosamente");
            }
            else
            {
                if (monto<=montoactual)
                {
                    if (comboBox2.Text.Equals(monedareg))
                    {

                        saldo = (montoactual - monto);
                        string query2 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                        SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());
                        cmd.Parameters.AddWithValue("@SALDO", saldo);
                        cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (monedareg.Equals("BOLIVIANOS") && comboBox2.Text.Equals("DOLARES"))
                        {
                            saldo = (montoactual - (monto * 6.84));
                            string query2 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());
                            cmd.Parameters.AddWithValue("@SALDO", saldo);
                            cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            if (monedareg.Equals("DOLARES") && comboBox2.Text.Equals("BOLIVIANOS"))
                            {
                                saldo = (montoactual - (monto / 6.97));
                                string query2 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                                SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());
                                cmd.Parameters.AddWithValue("@SALDO", saldo);
                                cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                                cmd.ExecuteNonQuery();
                            }
                            
                        }
                    }
                    Conexion.Conectar();

                    string transaccion = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                    SqlCommand cmd10 = new SqlCommand(transaccion, Conexion.Conectar());
                    cmd10.Parameters.AddWithValue("@MONEDA", comboBox2.Text);
                    cmd10.Parameters.AddWithValue("@ID_CUENTA", idcuenta);
                    cmd10.Parameters.AddWithValue("@TIPO_TRANSACCION", comboBox1.Text);
                    cmd10.Parameters.AddWithValue("@MONTO", textBox2.Text);
                    cmd10.ExecuteNonQuery();

                    MessageBox.Show("se realizo el movimiento exitosamente");
                }
                else
                {
                    MessageBox.Show("NO CUENTA CON SUFICIENTE SALDO");
                }
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public double TextoaDouble(string s)
        {
            var clone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            clone.NumberFormat.NumberDecimalSeparator = ".";
            clone.NumberFormat.NumberGroupSeparator = ",";
            // ejemplo string s = "1,14535765" o string s="1.141516";
            double d = double.Parse(s, clone);

            return d;
        }

        public bool verifica()
        {
            string cuentaD = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand cmd = new SqlCommand(cuentaD, Conexion.Conectar());
            cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
            int idcuentaD = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();

            SqlDataReader leer = cmd.ExecuteReader();
            if (leer.Read()==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}
