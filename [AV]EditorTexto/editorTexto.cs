using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

/*
Desenvolvedor: Helen Brandão;
Versão: 1.2;
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

    // TEXTO
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

    private void alterarCorFonte()
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                rtxtbPrincipal.SelectionColor = colorDialog1.Color;
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

    // ARQUIVO

    private void salvarArquivo()
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.InitialDirectory = @"c:\C:";
            svd.Filter = "Images (*.TXT)|*.TXT|" + "pdf (*.pdf)|*.pdf";
            svd.RestoreDirectory = true;
            if (svd.ShowDialog() == DialogResult.OK)
            {
                rtxtbPrincipal.SaveFile(svd.FileName, RichTextBoxStreamType.PlainText);
            }
        }

    private void chamarSalvarArquivo()
        {
            if (!string.IsNullOrEmpty(rtxtbPrincipal.Text))
            {
                if ((MessageBox.Show("Deseja Salvar o arquivo ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    salvarArquivo();
                }
            }
        }

    private void abrirArquivo()
        {
            //OPENFILEDIALOG
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.Title = "Selecionar Arquivos";
            openFileDialog1.InitialDirectory = @"C:\";

            // FILTRO
            openFileDialog1.Filter = "Images (*.TXT)|*.TXT|" + "All files (*.*)|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            DialogResult dr = this.openFileDialog1.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    StreamReader mStreamReader = new StreamReader(fs);

                    // LE O ARQUIVO USANDO STREAMREADER
                    mStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                    // LE CADA LINHA DO STREAM
                    this.rtxtbPrincipal.Text = "";
                    string strLine = mStreamReader.ReadLine();
                    while(strLine!= null)
                    {
                        this.rtxtbPrincipal.Text += strLine + "\n";
                        strLine = mStreamReader.ReadLine();
                    }

                    mStreamReader.Close();
                }catch(Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    private void copiarArquivo()
        {
            if (rtxtbPrincipal.SelectionLength > 0)
            {
                rtxtbPrincipal.Copy();
            }
        }

    private void colarArquivo()
        {
            if(rtxtbPrincipal.SelectionLength > 0)
            {
                rtxtbPrincipal.Paste();
            }
        }

    private void sairAplicativo()
        {
            if (!string.IsNullOrEmpty(rtxtbPrincipal.Text))
            {
                if ((MessageBox.Show("Deseja Sair do aplicativo?", "Sair do aplicativo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    Application.Exit();
                }
            }
        }

    // IMPRESSAO
    private void condiguracoesImpressora()
        {
            try
            {
                this.pdEditor.Document = this.pdocumentEditor;
                pdEditor.ShowDialog();
            }catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    private void visualizarImpressao()
        {
            try
            {
                string strTexto = rtxtbPrincipal.Text;
                StringReader leitor = new StringReader(strTexto);
                PrintPreviewDialog ppdEditor = new PrintPreviewDialog();
                var prn = ppdEditor;
                prn.Document = this.pdocumentEditor;
                prn.Text = "Visualizando a impressão";
                prn.WindowState = FormWindowState.Maximized;
                prn.PrintPreviewControl.Zoom = 1;
                prn.FormBorderStyle = FormBorderStyle.Fixed3D;
                prn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    // ACESSOS
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

        private void salvarTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvarArquivo();
            rtxtbPrincipal.Clear();
            rtxtbPrincipal.Focus();
        }

        private void mnCopiar_Click(object sender, EventArgs e)
        {
            copiarArquivo();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colarArquivo();
        }

        private void abrirTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirArquivo();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sairAplicativo();
        }

        private void configuraçõesImpressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            condiguracoesImpressora();
        }

        private void visualizarImpressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            visualizarImpressao();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // imprimir();
        }

        private void altereACorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alterarCorFonte();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtbPrincipal.Clear();
            rtxtbPrincipal.Focus();
        }
    }
}
