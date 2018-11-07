using PrimeiroBD.Classes;
using PrimeiroBD.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeiroBD
{
    public partial class frmAgenda : Form
    {
        public frmAgenda()
        {
            InitializeComponent();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int id = (int) dgvAgenda.CurrentRow.Cells[0].Value;
            TarefaDAO tarefaDao = new TarefaDAO();
            tarefaDao.Excluir(id);
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            TarefaDAO tarefaDao = new TarefaDAO();
            DataTable dataTable = tarefaDao.GetTarefas();

            dgvAgenda.DataSource = dataTable;
            dgvAgenda.Refresh();
            statusStrip.Items[0].Text = tarefaDao.ContarTarefas()+" tarefa(s)";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            frmIncluirAlterarTarefa form = new frmIncluirAlterarTarefa();
            form.ShowDialog();
            CarregarGrid();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            bool statusB = true;

            if (dgvAgenda.CurrentRow.Cells[4].Value.Equals("Pendente"))
            {
                statusB = false;
            }

            Tarefa tarefa = new Tarefa
            {
                id = (int)dgvAgenda.CurrentRow.Cells[0].Value,
                titulo = dgvAgenda.CurrentRow.Cells[1].Value.ToString(),
                descricao = dgvAgenda.CurrentRow.Cells[2].Value.ToString(),
                tempo = (int)dgvAgenda.CurrentRow.Cells[3].Value,
                status = statusB
            };

            frmIncluirAlterarTarefa form = new frmIncluirAlterarTarefa(tarefa);
            form.ShowDialog();
            CarregarGrid();
        }

        private void dgvAgenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TarefaDAO tarefaDao = new TarefaDAO();

            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                bool statusB = false;

                if (dgvAgenda.CurrentRow.Cells[4].Value.Equals("Pendente"))
                {
                    statusB = true;
                }

                Tarefa tarefa = new Tarefa
                {
                    id = (int)dgvAgenda.CurrentRow.Cells[0].Value,
                    titulo = dgvAgenda.CurrentRow.Cells[1].Value.ToString(),
                    descricao = dgvAgenda.CurrentRow.Cells[2].Value.ToString(),
                    tempo = (int)dgvAgenda.CurrentRow.Cells[3].Value,
                    status = statusB
                };

                tarefaDao.Atualizar(tarefa);
                CarregarGrid();
            }
        }
    }
}
