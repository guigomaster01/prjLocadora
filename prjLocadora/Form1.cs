﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLocadora
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void produtoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProdutoras produtoras = new frmProdutoras();
            produtoras.MdiParent = this;
            produtoras.Show();
        }

        private void filmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFilmes filmes = new frmFilmes();
            filmes.MdiParent = this;
            filmes.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
