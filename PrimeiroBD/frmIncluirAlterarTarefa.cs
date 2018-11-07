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
    public partial class frmIncluirAlterarTarefa : Form
    {

        private Tarefa tarefa;

        public frmIncluirAlterarTarefa(Tarefa tarefa = null)
        {
            this.tarefa = tarefa;
            InitializeComponent();
        }

        public frmIncluirAlterarTarefa()
        {
            InitializeComponent();
        }

        private void frmIncluirAlterarTarefa_Load(object sender, EventArgs e)
        {
            if (this.tarefa != null)
            {
                txbTitulo.Text = this.tarefa.titulo;
                txbDescricao.Text = this.tarefa.descricao;
                txbTempo.Text = this.tarefa.tempo.ToString();
                ckbStatus.Checked = this.tarefa.status;
            }
            else
            {
                txbTitulo.Text = string.Empty;
                txbDescricao.Text = string.Empty;
                txbTempo.Text = string.Empty;
                ckbStatus.Checked = false;
            }

            txbTitulo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            TarefaDAO tarefaDao = new TarefaDAO();

            if (this.tarefa == null)
            {
                Tarefa tarefa = new Tarefa
                {
                    titulo = txbTitulo.Text,
                    descricao = txbDescricao.Text,
                    tempo = Convert.ToInt32(txbTempo.Text),
                    status = ckbStatus.Checked
                };

                tarefaDao.Inserir(tarefa);
            }
            else
            {
                this.tarefa.titulo = txbTitulo.Text;
                this.tarefa.descricao = txbDescricao.Text;
                this.tarefa.tempo = Convert.ToInt32(txbTempo.Text);
                this.tarefa.status = ckbStatus.Checked;

                tarefaDao.Atualizar(this.tarefa);
            }

            this.Close();
        }
    }
}
