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
    /*
    private void salvarArquivo()
        {
            try
            {
                // PEGA O NOME DO ARQUIVO PARA SALVAR
                if (this.svfdEditor.ShowDialog() == DialogResult.OK)
                {
                    // ABRE UM STREAM PARA ESCRITA E CRIA UM STREAMWRITER PARA IMPLEMENTAR O STREAM
                    FileStream fs = new FileStream(svfdEditor.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter m_streamWriter = new StreamWriter(fs);
                    m_streamWriter.Flush();

                    // ESCREVE PARA O ARQUIVO USANDO A CLASSE STREAMWRITER
                    m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                    // ESCREVE NO CONTROLE DO RICHTEXTBOX
                    m_streamWriter.Write(this.rtxtbPrincipal.Text);

                    // FECHA O ARQUIVO
                    m_streamWriter.Flush();
                    m_streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    */

        private void salvarArquivo()
        {

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
            // DEFINE AS PROPRIEDADES DO ARQUIVO
            //OpenFileDialog
            this.pfdEditor.Multiselect = true;
            this.pfdEditor.Title = "Selecionar Arquivo";
            pfdEditor.InitialDirectory = @"C:\Dados\";

            // FILTRA PARA EXIBIR SOMENTE ARQUIVOS DE TEXTOS
            pfdEditor.Filter = "Images (*.TXT)|*.TXT|" + "All files (*.*)|*.*";
            pfdEditor.CheckFileExists = true;
            pfdEditor.CheckPathExists = true;
            pfdEditor.FilterIndex = 1;
            pfdEditor.RestoreDirectory = true;
            pfdEditor.ReadOnlyChecked = true;
            pfdEditor.ShowReadOnly = true;
            DialogResult dr = this.pfdEditor.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(pfdEditor.FileName, FileMode.Open, FileAccess.Read);
                    StreamReader m_streamReader = new StreamReader(fs);

                    // LE O ARQUIVO USANDO A CLASSE StreamReader
                    m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                    // LE CADA LINHA DO STREAM E FAZ O PARSE ATE A ULTIMA LINHA
                    this.rtxtbPrincipal.Text = "";
                    string strLine = m_streamReader.ReadLine();
                    while (strLine != null)
                    {
                        this.rtxtbPrincipal.Text += strLine + "\n";
                        strLine = m_streamReader.ReadLine();
                    }

                    // FECHA O STREAM
                    m_streamReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    private void copiarArquivo()
        {
            if(rtxtbPrincipal.SelectionLength > 0)
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
            StringReader leitor = new StringReader(rtxtbPrincipal.Text);
            try
            {
                PrintPreviewDialog ppdEditor = new PrintPreviewDialog();
                var prn = ppdEditor;
                prn.Document = this.pdocumentEditor;
                prn.Text = "Helen - visualizando a impressão";
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
    }
}
