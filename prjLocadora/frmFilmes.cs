using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLocadora
{
    public partial class frmFilmes : Form
    {

        int registroAtual = 0;
        int totalRegistros = 0;
        bool novo;
        DataTable dtFilme = new DataTable();
        DataTable dtProdutoras = new DataTable();
        String connectionString = @"Server=darnassus\motorhead;Database=db_230650; User Id=230650; Password=";

        public frmFilmes()
        {
            InitializeComponent();
        }

        private void navegar()
        {
            carregaComboProdutoras();
            txtCodFilme.Text = dtFilme.Rows[registroAtual][0].ToString();
            txtTituloFilme.Text = dtFilme.Rows[registroAtual][1].ToString();
            txtAnoFilme.Text = dtFilme.Rows[registroAtual][2].ToString();
            //cbbProdutora.ValueMember = dtFilme.Rows[registroAtual][3].ToString();
            cbbGenero.Text = dtFilme.Rows[registroAtual][4].ToString();
        }

        private void carregaTudoProdutoras()
        {
            dtProdutoras = new DataTable();
            string sql = "SELECT * from tblProdutora";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutoras.Load(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            cbbProdutora.DataSource = dtProdutoras;
            cbbProdutora.DisplayMember = "nomeProd";
            cbbProdutora.ValueMember = "codProd";
        }

        private void carregaComboProdutoras()
        {
            dtProdutoras = new DataTable();
            string sql = "SELECT * from tblProdutora WHERE codProd=" + dtFilme.Rows[registroAtual][3].ToString();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutoras.Load(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            cbbProdutora.DataSource = dtProdutoras;
            cbbProdutora.DisplayMember = "nomeProd";
            cbbProdutora.ValueMember = "codProd";

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if(registroAtual < totalRegistros - 1)
            {
                registroAtual++;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (registroAtual < totalRegistros - 1)
            {
                registroAtual = totalRegistros - 1;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual--;
                navegar();
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual = 0;
                navegar();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmFilmes_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodFilme.Enabled = false;
            txtTituloFilme.Enabled = false;
            txtAnoFilme.Enabled = false;
            cbbProdutora.Enabled = false;
            cbbGenero.Enabled = false;
            string sql = "SELECT * FROM tblFilme";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtFilme.Load(reader);
                    totalRegistros = dtFilme.Rows.Count;
                    registroAtual = 0;
                    navegar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            txtCodFilme.Enabled = false;
            txtCodFilme.Clear();
            txtTituloFilme.Enabled = true;
            txtTituloFilme.Clear();
            txtAnoFilme.Enabled = true;
            txtAnoFilme.Clear();
            cbbProdutora.Enabled = true;
            cbbProdutora.SelectedIndex = -1;
            cbbGenero.Enabled = true;
            cbbGenero.SelectedIndex = -1;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            txtTituloFilme.Focus();
            carregaTudoProdutoras();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            txtTituloFilme.Enabled = true;
            txtAnoFilme.Enabled = true;
            cbbGenero.Enabled = true;
            cbbProdutora.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnAnterior.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            carregaTudoProdutoras();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO tblFilme (tituloFilme, anoFilme, codProd, generoFilme) VALUES('" +txtTituloFilme.Text+"',"+txtAnoFilme.Text+", "+cbbProdutora.SelectedValue.ToString()+", '"+cbbGenero.Text+"')";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Filme cadastrado com sucesso!");
                        this.frmFilmes_Load(this, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {

            }
        }
    }
}
