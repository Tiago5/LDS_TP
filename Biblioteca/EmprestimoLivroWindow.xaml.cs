using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biblioteca
{
    /// <summary>
    /// Interaction logic for EmprestimoLivroWindow.xaml
    /// </summary>
    public partial class EmprestimoLivroWindow : Window
    {
        private MainWindow mainWindow;
        private string mensagemLivro = "";
        private string mensagemUtilizador = "";
        private long idL;
        private long idU;

        public EmprestimoLivroWindow(MainWindow mw)
        {
            InitializeComponent();
            this.mainWindow = mw;
        }


        //button para pesquisar livro na lista de livros da biblioteca de trabalho
        private void Button_PesquisarIdLivro_Click(object sender, RoutedEventArgs e)
        {
            Validacoes validacao = new Validacoes();
            if (validacao.validarId(TextBox_IdResultado.Text))
            {
                mensagemLivro = "";
                mensagemLivro = mainWindow.procurarLivroParaEmprestimo(long.Parse(TextBox_IdResultado.Text));
                Label_Resultado.Content = mensagemLivro;
                if (mensagemLivro == "Disponivel")
                {
                    //idl =idlivro
                    idL = long.Parse(TextBox_IdResultado.Text);
                    TextBox_DataEntrega.Text = CalcularDataEntrega();
                }
            }

        }
        //button para gravar emprestimo
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.atualizarListaViewLivros();
            mainWindow.Button_Emprestimo.IsEnabled = true;
        }


        private void Button_SubmeterLivro_Click_1(object sender, RoutedEventArgs e)
        {
            if (mensagemLivro == "Disponivel" && (mensagemUtilizador != "Inativo" && mensagemUtilizador != "O utilizador não existe"))
            {
                // vai alterar o estado do livro
                mainWindow.alterarestadoLivro(long.Parse(TextBox_IdResultado.Text), "Indisponivel");
                string s = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
                //vai registar o emprestimo
                mainWindow.inserirEmprestimo(idU, long.Parse(TextBox_IdBiblioteca.Text), idL, s, CalcularDataEntrega(), "");
                MainWindow.windowEmpretimoAberta = false;

                this.Close();

            }
        }
        /// <summary>
        /// Calcula a data de entrega do livro
        /// </summary>
        /// <returns>Data Entrega</returns>
        private string CalcularDataEntrega()
        {
            int ano;
            int mes;
            int dia;
            int duracao;
            int[] meses = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            dia = DateTime.Now.Day;
            mes = DateTime.Now.Month;
            ano = DateTime.Now.Year;
            //verifica ano bissexto
            if ((ano % 4 == 0 && ano % 100 != 0) || ano % 400 == 0)
            {
                meses[1] = 29;
            }
            //verifica duração de emprestimo
            if (RButton_Expresso.IsChecked == true)
            {
                duracao = 2;
            }
            else
            {
                duracao = 10;
            }
            if (dia + duracao < meses[mes - 1])
            {
                dia = dia + duracao;
            }
            else
            {
                dia = duracao - (meses[mes - 1] - dia);
                if (mes == 12)
                {
                    mes = 1;
                    ano++;
                }
                else
                {
                    mes++;
                }
            }
            return dia.ToString() + "-" + mes.ToString() + "-" + ano.ToString();
        }
        //Button para verificar se o utilizador existe
        private void Button_ProcurarIdUtilizador_Click(object sender, RoutedEventArgs e)
        {
            Validacoes validacao = new Validacoes();
            if (validacao.validarId(TextBox_IdUtilizador.Text))
            {
                idU = long.Parse(TextBox_IdUtilizador.Text);
                mensagemUtilizador = mainWindow.prucuraUtilizadorParaEmprestimo(long.Parse(TextBox_IdUtilizador.Text));
                Label_ResultadoUtilizador.Content = mensagemUtilizador;
            }
        }
    }
}
