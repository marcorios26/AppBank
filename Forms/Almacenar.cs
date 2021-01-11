using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BankApplication.Forms
{
    

    public partial class Almacenar : Form
    {
        
        public Almacenar()
        {
            InitializeComponent();
            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Almacenar_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkfields())
            {
                double saldo = this.TextoaDouble(textBox2.Text);
                saldo = Math.Round(saldo,2);
                
                
                textBox2.Text = Convert.ToString(saldo);
                textBox3.Text = NumCuenta();

                Conexion.Conectar();
                string insertar = "INSERT INTO CUENTA_BANCARIA (NUM_CUENTA,DEPARTAMENTO,TITULAR,MONEDA,SALDO)VALUES(@NUM_CUENTA,@DEPARTAMENTO,@TITULAR,@MONEDA,@SALDO)";
                SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());
                cmd1.Parameters.AddWithValue("@NUM_CUENTA", NumCuenta());
                cmd1.Parameters.AddWithValue("@DEPARTAMENTO", comboBox2.Text);
                cmd1.Parameters.AddWithValue("@TITULAR", textBox1.Text);
                cmd1.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                cmd1.Parameters.AddWithValue("@SALDO", saldo);

                cmd1.ExecuteNonQuery();
                // extraendo el valor de id_cuenta
                string query1 = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
                SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
                command1.Parameters.AddWithValue("@NUM_CUENTA", NumCuenta());
                int lastId1 = Convert.ToInt32(command1.ExecuteScalar());

                //NUMECUENTA
                string x="" ;
                string code = Convert.ToString(lastId1);
                int limite = 6 - code.Length;
                if (x.Length<7)
                {   
                    
                    for(int i=0; i < limite; i++)
                    {
                        x = x + "0";
                    }
                    x = x + code;
                }
                String CuentaB = NumCuenta() + x;
                textBox3.Text = CuentaB;

                Conexion.Conectar();
                string agregarCuenta = "UPDATE CUENTA_BANCARIA SET NUM_CUENTA=@NUM_CUENTA WHERE ID_CUENTA=@ID_CUENTA" ;
                SqlCommand comando = new SqlCommand(agregarCuenta, Conexion.Conectar());

                comando.Parameters.AddWithValue("@NUM_CUENTA",CuentaB);

                comando.Parameters.AddWithValue("@ID_CUENTA", lastId1);
                comando.ExecuteNonQuery();

                if (saldo>0)
                {
                    
                    Conexion.Conectar();
                    string insertar2 = "INSERT INTO TRANSACCION (MONEDA,ID_CUENTA,TIPO_TRANSACCION,MONTO)VALUES(@MONEDA,@ID_CUENTA,@TIPO_TRANSACCION,@MONTO)";
                    
                    //insertar
                    SqlCommand cmdM = new SqlCommand(insertar2, Conexion.Conectar());
                    cmdM.Parameters.AddWithValue("@MONEDA", comboBox1.Text);
                    cmdM.Parameters.AddWithValue("@ID_CUENTA",lastId1);
                    cmdM.Parameters.AddWithValue("@TIPO_TRANSACCION","ABONO");
                    cmdM.Parameters.AddWithValue("@MONTO",saldo);
                    cmdM.ExecuteNonQuery();
                    

                }




                MessageBox.Show("Cuenta creada exitosamente", " Mensaje");

            }
            else
            {
                MessageBox.Show("existe algun problema en los campos de datos por favor reviselos", " Mensaje");
            }
        }

        private  bool checkfields()
        {
            if (textBox1.Text!=" " & textBox2.Text!=" " & comboBox1.Text!="" & comboBox2.Text!="")
            {
                //titular de cuenta

                int count = 0;
                for (int i=0; i < textBox1.Text.Length; i++ )
                {
                    
                    if (textBox1.Text[i].Equals(' '))
                    {
                        count++;
                    }
                }
                if (count==1)
                {
                    //saldo
                    if (Convert.ToDouble(textBox2.Text) < 10000000000)
                    {//moneda
                        if (comboBox1.Text.Equals("BOLIVIANOS") || comboBox1.Text.Equals("DOLARES"))
                        {
                            if (comboBox2.Text.Equals("LA PAZ") || comboBox2.Text.Equals("COCHABAMBA") ||
                                comboBox2.Text.Equals("POTOSI") || comboBox2.Text.Equals("ORURO") ||
                                comboBox2.Text.Equals("PANDO") || comboBox2.Text.Equals("SANTA CRUZ") ||
                                comboBox2.Text.Equals("TARIJA") || comboBox2.Text.Equals("BENI") ||
                                comboBox2.Text.Equals("CHUQUISACA"))

                            {
                                if (TextoaDouble(textBox2.Text)>=0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                
               
            }
            return false;
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
        private String NumCuenta()
        {
            String NumCuenta = "";
            if (comboBox1.Text.Equals("BOLIVIANOS"))
            {
                NumCuenta = NumCuenta + "201"+"-";

            }
            else
            {
                NumCuenta = NumCuenta + "202" + "-";
            }
            switch (comboBox2.Text)
            {
                case "LA PAZ":
                    NumCuenta = NumCuenta+"01"+"-";
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

    }
}
