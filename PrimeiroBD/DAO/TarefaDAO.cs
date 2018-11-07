using PrimeiroBD.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiroBD.DAO
{
    public class TarefaDAO
    {
        public DataTable GetTarefas()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetCommand(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM TAREFAS";
            DbDataReader reader = DAOUtils.GetDataReader(comando);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Título", typeof(String));
            dataTable.Columns.Add("Descrição", typeof(String));
            dataTable.Columns.Add("Tempo", typeof(Int32));
            dataTable.Columns.Add("Status", typeof(String));

            while (reader.Read())
            {
                DataRow newRow = dataTable.NewRow();
                newRow[0] = reader.GetValue(0).ToString();
                newRow[1] = reader.GetValue(1).ToString();
                newRow[2] = reader.GetValue(2).ToString();
                newRow[3] = reader.GetValue(3).ToString();
                if (reader.GetValue(4).ToString().Equals("True"))
                {
                    newRow[4] = "Concluído";
                }
                else
                {
                    newRow[4] = "Pendente";
                }

                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }

        public void Excluir(int id)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetCommand(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE FROM TAREFAS WHERE ID = @id";
            comando.Parameters.Add(DAOUtils.GetParametro("@id", id));
            comando.ExecuteNonQuery();
        }

        public void Inserir(Tarefa tarefa)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetCommand(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO TAREFAS (TITULO, DESCRICAO, TEMPO, STATUS) VALUES (@titulo, @descricao, @tempo, @status)";
            comando.Parameters.Add(DAOUtils.GetParametro("@titulo", tarefa.titulo));
            comando.Parameters.Add(DAOUtils.GetParametro("@descricao", tarefa.descricao));
            comando.Parameters.Add(DAOUtils.GetParametro("@tempo", tarefa.tempo));
            comando.Parameters.Add(DAOUtils.GetParametro("@status", tarefa.status));
            comando.ExecuteNonQuery();
        }

        public void Atualizar(Tarefa tarefa)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetCommand(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE TAREFAS SET TITULO = @titulo, DESCRICAO = @descricao, TEMPO = @tempo, STATUS = @status WHERE ID = @id";
            comando.Parameters.Add(DAOUtils.GetParametro("@id", tarefa.id));
            comando.Parameters.Add(DAOUtils.GetParametro("@titulo", tarefa.titulo));
            comando.Parameters.Add(DAOUtils.GetParametro("@descricao", tarefa.descricao));
            comando.Parameters.Add(DAOUtils.GetParametro("@tempo", tarefa.tempo));
            comando.Parameters.Add(DAOUtils.GetParametro("@status", tarefa.status));
            comando.ExecuteNonQuery();
        }

        public int ContarTarefas()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetCommand(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT COUNT(*) FROM TAREFAS";
            return Convert.ToInt32(comando.ExecuteScalar());
        }
    }
}
