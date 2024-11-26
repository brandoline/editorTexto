using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
Desenvolvedor: Helen Brandão;
Versão: 1.0;
DataInicio: 25/11/2024
DataFim: 
Sobre o Código: 
 */

namespace _AV_EditorTexto
{
    public partial class editorTexto : Form
    {
        public editorTexto()
        {
            InitializeComponent();
        }
        
    private void negrito()
        {
            string nomeFonte = rtxtbPrincipal.Font.Name;
            float tamanhoFonte = rtxtbPrincipal.Font.Size;
            bool negrito = rtxtbPrincipal.SelectionFont.Bold;

            if(!negrito)
            {
                rtxtbPrincipal.SelectionFont = new Font(nomeFonte, tamanhoFonte,FontStyle.Bold);
            }
            else
            {
                rtxtbPrincipal.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Regular);
            }
        }

    private void italico()
        {
            string nomeFonte = rtxtbPrincipal.Font.Name;
            float tamanhoFonte = rtxtbPrincipal.Font.Size;
            bool italico = rtxtbPrincipal.SelectionFont.Italic;

            if (!italico)
            {
                rtxtbPrincipal.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Italic);
            }
            else
            {
                rtxtbPrincipal.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Regular);
            }
        }

    private void sublinhado()
        {
            string nomeFonte = rtxtbPrincipal.Font.Name;
            float tamanhoFonte = rtxtbPrincipal.Font.Size;
            bool sublinhado = rtxtbPrincipal.SelectionFont.Underline;

            if (!sublinhado)
            {
                rtxtbPrincipal.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Underline);
            }
            else
            {
                rtxtbPrincipal.SelectionFont = new Font(nomeFonte, tamanhoFonte, FontStyle.Regular);
            }
        }

    private void alterarFonte() 
        {
            DialogResult result = fntdlgEditor.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (rtxtbPrincipal.SelectionFont != null)
                {
                    rtxtbPrincipal.SelectionFont = fntdlgEditor.Font;
                }
            }
        } 

    private void alinharEsquerda()
        {
            rtxtbPrincipal.SelectionAlignment = HorizontalAlignment.Left;
        }
    
    private void alinharDireita() 
        {
            rtxtbPrincipal.SelectionAlignment = HorizontalAlignment.Right;
        }

    private void alinharCentro() 
        {
            rtxtbPrincipal.SelectionAlignment = HorizontalAlignment.Center;
        }


        private void negritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            negrito();
        }

        private void itálicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            italico();
        }

        private void sublinhadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sublinhado();
        }

        private void alterarFonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alterarFonte();
        }

        private void esquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alinharEsquerda();
        }

        private void direitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alinharDireita();
        }

        private void centralizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alinharCentro();
        }
    }
}
