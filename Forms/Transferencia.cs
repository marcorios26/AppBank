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
    public partial class Transferencia : Form
    {
        public Transferencia()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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
        
        private void button1_Click(object sender, EventArgs e)
        {

            if (verifica())
            {

                Conexion.Conectar();


                string cuentaD = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd = new SqlCommand(cuentaD, Conexion.Conectar());
                cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                int idcuentaD = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();

                string monedaD = "SELECT MONEDA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd1 = new SqlCommand(monedaD, Conexion.Conectar());
                cmd1.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                string idmonedaD = Convert.ToString(cmd1.ExecuteScalar());
                cmd1.ExecuteNonQuery();

                string saldoD = "SELECT SALDO FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd4 = new SqlCommand(saldoD, Conexion.Conectar());
                cmd4.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                string idsaldoDB = Convert.ToString(cmd4.ExecuteScalar());
                cmd4.ExecuteNonQuery();

                idsaldoDB = idsaldoDB.Replace(",", ".");

                double idsaldoD = this.TextoaDouble(idsaldoDB);
                /*********************************************************************************/

                string cuentaA = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd3 = new SqlCommand(cuentaA, Conexion.Conectar());
                cmd3.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                int idcuentaA = Convert.ToInt32(cmd3.ExecuteScalar());
                cmd3.ExecuteNonQuery();

                string monedaA = "SELECT MONEDA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd2 = new SqlCommand(monedaA, Conexion.Conectar());
                cmd2.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                string idmonedaA = Convert.ToString(cmd2.ExecuteScalar());
                cmd2.ExecuteNonQuery();


                string saldoA = "SELECT SALDO FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd5 = new SqlCommand(saldoA, Conexion.Conectar());
                cmd5.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                string idsaldoAB = Convert.ToString(cmd5.ExecuteScalar());
                cmd5.ExecuteNonQuery();

                idsaldoAB = idsaldoAB.Replace(",", ".");
                double idsaldoA = this.TextoaDouble(idsaldoAB);
                /***************************************************************************/
                double monto = this.TextoaDouble(textBox2.Text);


                if (comboBox1.Text.Equals("DOLARES"))
                {
                    if (idmonedaD.Equals("DOLARES"))
                    {
                        double saldo1 = idsaldoD - monto;
                        string query = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                        SqlCommand command = new SqlCommand(query, Conexion.Conectar());
                        command.Parameters.AddWithValue("@SALDO", saldo1);
                        command.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                        command.ExecuteNonQuery();
                        /************************************************************/

                        if (idmonedaA.Equals("DOLARES"))
                        {
                            double saldo2 = idsaldoA + monto;
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }
                        else
                        {
                            double saldo2 = idsaldoA + (monto * 6.84);
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }

                        string transaccion = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd10 = new SqlCommand(transaccion, Conexion.Conectar());
                        cmd10.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd10.Parameters.AddWithValue("@ID_CUENTA", idcuentaD);
                        cmd10.Parameters.AddWithValue("@TIPO_TRANSACCION", "DEBITO");
                        cmd10.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd10.ExecuteNonQuery();

                        string transaccion2 = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd11 = new SqlCommand(transaccion2, Conexion.Conectar());
                        cmd11.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd11.Parameters.AddWithValue("@ID_CUENTA", idcuentaA);
                        cmd11.Parameters.AddWithValue("@TIPO_TRANSACCION", "ABONO");
                        cmd11.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd11.ExecuteNonQuery();


                        MessageBox.Show("SE REALIZO LA TRANSFERENCIA CON EXITO");
                    }
                    else
                    {
                        double saldo1 = idsaldoD - (monto * 6.84);
                        string query = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                        SqlCommand command = new SqlCommand(query, Conexion.Conectar());
                        command.Parameters.AddWithValue("@SALDO", saldo1);
                        command.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                        command.ExecuteNonQuery();
                        /************************************************************/

                        if (idmonedaA.Equals("DOLARES"))
                        {
                            double saldo2 = idsaldoA + monto;
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }
                        else
                        {
                            double saldo2 = idsaldoA + (monto * 6.84);
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }


                        string transaccion = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd10 = new SqlCommand(transaccion, Conexion.Conectar());
                        cmd10.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd10.Parameters.AddWithValue("@ID_CUENTA", idcuentaD);
                        cmd10.Parameters.AddWithValue("@TIPO_TRANSACCION", "DEBITO");
                        cmd10.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd10.ExecuteNonQuery();

                        string transaccion2 = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd11 = new SqlCommand(transaccion2, Conexion.Conectar());
                        cmd11.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd11.Parameters.AddWithValue("@ID_CUENTA", idcuentaA);
                        cmd11.Parameters.AddWithValue("@TIPO_TRANSACCION", "ABONO");
                        cmd11.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd11.ExecuteNonQuery();




                        MessageBox.Show("SE REALIZO LA TRANSFERENCIA CON EXITO");
                    }
                }
                else
                {
                    if (idmonedaD.Equals("BOLIVIANOS"))
                    {
                        double saldo1 = idsaldoD - monto;
                        string query = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                        SqlCommand command = new SqlCommand(query, Conexion.Conectar());
                        command.Parameters.AddWithValue("@SALDO", saldo1);
                        command.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                        command.ExecuteNonQuery();
                        /************************************************************/

                        if (idmonedaA.Equals("BOLIVIANOS"))
                        {
                            double saldo2 = idsaldoA + monto;
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }
                        else
                        {
                            double saldo2 = idsaldoA + (monto / 6.97);
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }

                        string transaccion = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd10 = new SqlCommand(transaccion, Conexion.Conectar());
                        cmd10.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd10.Parameters.AddWithValue("@ID_CUENTA", idcuentaD);
                        cmd10.Parameters.AddWithValue("@TIPO_TRANSACCION", "DEBITO");
                        cmd10.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd10.ExecuteNonQuery();

                        string transaccion2 = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd11 = new SqlCommand(transaccion2, Conexion.Conectar());
                        cmd11.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd11.Parameters.AddWithValue("@ID_CUENTA", idcuentaA);
                        cmd11.Parameters.AddWithValue("@TIPO_TRANSACCION", "ABONO");
                        cmd11.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd11.ExecuteNonQuery();

                        MessageBox.Show("SE REALIZO LA TRANSFERENCIA CON EXITO");
                    }
                    else
                    {
                        double saldo1 = idsaldoD - (monto / 6.97);
                        string query = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                        SqlCommand command = new SqlCommand(query, Conexion.Conectar());
                        command.Parameters.AddWithValue("@SALDO", saldo1);
                        command.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                        command.ExecuteNonQuery();
                        /************************************************************/

                        if (idmonedaA.Equals("BOLIVIANOS"))
                        {
                            double saldo2 = idsaldoA + monto;
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }
                        else
                        {
                            double saldo2 = idsaldoA + (monto / 6.97);
                            string query1 = "UPDATE CUENTA_BANCARIA SET SALDO=@SALDO  WHERE NUM_CUENTA=@NUM_CUENTA";
                            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                            command1.Parameters.AddWithValue("@SALDO", saldo2);
                            command1.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                            command1.ExecuteNonQuery();
                        }

                        string transaccion = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd10 = new SqlCommand(transaccion, Conexion.Conectar());
                        cmd10.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd10.Parameters.AddWithValue("@ID_CUENTA", idcuentaD);
                        cmd10.Parameters.AddWithValue("@TIPO_TRANSACCION", "DEBITO");
                        cmd10.Parameters.AddWithValue("@MONTO", textBox2.Text);
                        cmd10.ExecuteNonQuery();

                        string transaccion2 = "INSERT INTO TRANSACCION(MONEDA, ID_CUENTA, TIPO_TRANSACCION, MONTO)VALUES(@MONEDA, @ID_CUENTA, @TIPO_TRANSACCION, @MONTO)";
                        SqlCommand cmd11 = new SqlCommand(transaccion2, Conexion.Conectar());
                        cmd11.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                        cmd11.Parameters.AddWithValue("@ID_CUENTA", idcuentaA);
                        cmd11.Parameters.AddWithValue("@TIPO_TRANSACCION", "ABONO");
                        cmd11.Parameters.AddWithValue("@MONTO", textBox2.Text);

                        cmd11.ExecuteNonQuery();
                        MessageBox.Show("SE REALIZO LA TRANSFERENCIA CON EXITO");
                    }
                }
            }
            else
            {
                MessageBox.Show("existe uno de los siguientes problemas : no existen una 0 ambas cuentas, o el monto es mas alto de lo permitido de acuerdo al saldo");
            }
            
           

        }
       
        public bool verifica()
        {
            string cuentaD = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand cmd = new SqlCommand(cuentaD, Conexion.Conectar());
            cmd.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
            int idcuentaD = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();

            SqlDataReader leer = cmd.ExecuteReader();


            string cuentaA = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand cmd3 = new SqlCommand(cuentaA, Conexion.Conectar());
            cmd3.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
            int idcuentaA = Convert.ToInt32(cmd3.ExecuteScalar());
            cmd3.ExecuteNonQuery();
            SqlDataReader leer2 = cmd3.ExecuteReader();
            /**********************/
            if (leer.Read() == true && leer2.Read()== true)
            {
                string monedaD = "SELECT MONEDA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd1 = new SqlCommand(monedaD, Conexion.Conectar());
                cmd1.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                string idmonedaD = Convert.ToString(cmd1.ExecuteScalar());
                cmd1.ExecuteNonQuery();

                string saldoD = "SELECT SALDO FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd4 = new SqlCommand(saldoD, Conexion.Conectar());
                cmd4.Parameters.AddWithValue("@NUM_CUENTA", textBox1.Text);
                string idsaldoDB = Convert.ToString(cmd4.ExecuteScalar());
                cmd4.ExecuteNonQuery();

                idsaldoDB = idsaldoDB.Replace(",", ".");

                double idsaldoD = this.TextoaDouble(idsaldoDB);
                /****************************************************************/
                string monedaA = "SELECT MONEDA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd2 = new SqlCommand(monedaA, Conexion.Conectar());
                cmd2.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                string idmonedaA = Convert.ToString(cmd2.ExecuteScalar());
                cmd2.ExecuteNonQuery();


                string saldoA = "SELECT SALDO FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand cmd5 = new SqlCommand(saldoA, Conexion.Conectar());
                cmd5.Parameters.AddWithValue("@NUM_CUENTA", textBox3.Text);
                string idsaldoAB = Convert.ToString(cmd5.ExecuteScalar());
                cmd5.ExecuteNonQuery();

                idsaldoAB = idsaldoAB.Replace(",", ".");
                double idsaldoA = this.TextoaDouble(idsaldoAB);
                /***********************************************************/
                double monto = this.TextoaDouble(textBox2.Text);
                if (comboBox1.Text.Equals("DOLARES"))
                {
                    if (idmonedaD.Equals("DOLARES"))
                    {
                        if (idsaldoD>=monto)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (idsaldoD>=(monto*6.84))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (idmonedaD.Equals("BOLIVIANOS"))
                    {
                        if (idsaldoD >= monto)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (idsaldoD >= (monto/6.97))
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
            else
            {
                return false;
            }
        }
    }
}
