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
    /// Interaction logic for DevolvelLivroWindow.xaml
    /// </summary>
    public partial class DevolvelLivroWindow : Window
    {
        MainWindow mainWindow;
        public DevolvelLivroWindow( MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.atualizarListaViewLivros();
            MainWindow.windowDevolverLivro = false;

        }

        private void Button_DevolverLivro_Click(object sender, RoutedEventArgs e)
        {

            Validacoes validacao = new Validacoes();
            if (validacao.validarId(TextBox_IdLivro.Text))
            {
                string s = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();

                if (mainWindow.pesquisaLibroDB(long.Parse(TextBox_IdLivro.Text), "Indisponivel"))
                {
                    //vai atualizar os emprestimos com a data de entrega
                    mainWindow.devolucaoLivro(long.Parse(TextBox_IdLivro.Text), s);

                    //vai atualizar o estado do livro
                    mainWindow.alterarestadoLivro(long.Parse(TextBox_IdLivro.Text), "Disponivel");
                    Label_Resultado.Content = "LIVRO DEVOLVIDO";

                }
                else
                {
                    Label_Resultado.Content = "ESTE LIVRO NÃO PODE SER DEVOLVIDO";
                }

            }
        }
    }
}
