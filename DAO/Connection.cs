using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace ProjetoBiometria.DAO
{
    class Connection
    {
        static string serverName = "127.0.0.1";                              
        static string port = "5432";                                                            
        static string userName = "postgres";                                               
        static string password = "heitor";                                          
        static string databaseName = "biometria";
        public static string mensagem = "";
        public static bool verifica = false;
 

        public static NpgsqlConnection Conectar()
        {
            NpgsqlConnection mConn = null;

            string stringConexao = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                                serverName, port, userName, password, databaseName); 

            mConn = new NpgsqlConnection(stringConexao);

            try
            {
                mConn.Open();
            }
            catch (NpgsqlException ex)
            {
                mensagem = ex.ToString();
            }
            return mConn;
        }
        public static void CRUD(NpgsqlCommand comandoSQL)
        {
            try
            {
                NpgsqlConnection mConn = Conectar();
                comandoSQL.Connection = mConn;
                comandoSQL.ExecuteNonQuery();
                mConn.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show( ex.ToString(), "Banco de dados Inválido!!");
            }

        }

        public static NpgsqlDataReader Select(NpgsqlCommand comandoSQL)
        {

            NpgsqlConnection mConn = Conectar();

            comandoSQL.Connection = mConn;

            NpgsqlDataReader dr = null;

            try
            {
                if (mConn.State == System.Data.ConnectionState.Open)
                {

                    dr = comandoSQL.ExecuteReader();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dr;
        }
    }
}
