using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace BankApplication
{
    class Conexion
    {
        
        public static SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection("SERVER=MARCORIOS;DATABASE=BANCO;integrated security=true;");
            cn.Open();
            return cn;
        }

        public string existeTransaccion(string numeroCuenta,string textodecaja)
        {
            string emp = "";

            SqlConnection cn = new SqlConnection("SERVER=MARCORIOS;DATABASE=BANCO;integrated security=true;");
            cn.Open();
            string query1 = "SELECT ID_CUENTA FROM CUENTA_BANCARIA WHERE NUM_CUENTA=@NUM_CUENTA";
            SqlCommand command1 = new SqlCommand(query1, Conexion.Conectar());
            command1.Parameters.AddWithValue("@NUM_CUENTA", textodecaja);
            int lastId1 = Convert.ToInt32(command1.ExecuteScalar());

            string numCuenta = "";
            string query = "SELECT ID_TRANSACCION FROM TRANSACCION WHERE ID_CUENTA=@ID_CUENTA";
            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID_CUENTA", lastId1);
                cn.Open();
                emp = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                cn.Close();
            }
            return emp;
        }
    }
}
