using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiroBD.DAO
{
    public class DAOUtils
    {
        public static DbConnection GetConexao()
        {
            DbConnection conexao;

            string server = ConfigurationManager.AppSettings["server"].ToString();
            string port = ConfigurationManager.AppSettings["port"].ToString();
            string dataBase = ConfigurationManager.AppSettings["database"].ToString();
            string user = ConfigurationManager.AppSettings["user"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();

            string stringConnection = "";
            if (ConfigurationManager.AppSettings["provider"].ToString().Equals("MSSQL"))
            {
                // Conexão Microsoft Azure
                stringConnection = @"Server=" + server + "," + port + ";Initial Catalog=" + dataBase + ";Persist Security Info=False;User ID=" + user + ";Password=" + password + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30000;";
                conexao = new SqlConnection(stringConnection);
            }
            else
            {
                // Conexão Umbler
                stringConnection = @"Host=" + server + ";Port=" + port + ";Database=" + dataBase + ";Uid=" + user + ";Pwd=" + password + ";SSL Mode=none;";
                conexao = new MySqlConnection(stringConnection);
            }

            conexao.Open();

            return conexao;
        }

        public static DbCommand GetCommand(DbConnection conexao)
        {
            DbCommand comando = conexao.CreateCommand();
            return comando;
        }

        public static DbDataReader GetDataReader(DbCommand comando)
        {
            return comando.ExecuteReader();
        }

        public static DbParameter GetParametro(string nome, object valor)
        {
            DbParameter parametro = null;

            if (ConfigurationManager.AppSettings["provider"].ToString().Equals("MSSQL"))
            {
                parametro = new SqlParameter(nome, valor);
            }
            else
            {
                parametro = new MySqlParameter(nome, valor);
            }

            return parametro;
        }
    }
}
