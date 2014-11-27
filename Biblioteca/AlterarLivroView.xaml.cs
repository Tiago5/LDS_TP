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
    /// Interaction logic for AlterarLivroView.xaml
    /// </summary>
    public partial class AlterarLivroView : Window
    {
        private MainWindow mainWindow;
        private Boolean podeSubmeter = false;

        public AlterarLivroView(MainWindow mw)
        {
            InitializeComponent();
            this.mainWindow = mw;
        }

        private void Button_PesquisarIdLivro_Click(object sender, RoutedEventArgs e)
        {
            Validacoes validacao = new Validacoes();
            if (validacao.validarId(TextBox_IdResultado.Text))
            {
                if (!(mainWindow.procurarIdLivro(long.Parse(TextBox_IdResultado.Text))))
                {
                    Label_Resultado.Content = "O livro não existe na biblioteca";

                }
                else
                {
                    podeSubmeter = true;
                    Label_Resultado.Content = "";
                }
            }
        }

        private void Button_SubmeterLivro_Click(object sender, RoutedEventArgs e)
        {
            // Escolhe o tipo de emprestimo para o livro e guarda na variavel temp
            string temp = "";
            if (podeSubmeter)
            {

                if (RButton_Expresso.IsChecked == true)
                {
                    temp = "Expresso";
                }
                else
                {
                    temp = "Normal";
                }

                Validacoes validacao = new Validacoes();
                if (validacao.validarISBN(TextBox_ISBN.Text) && validacao.validarCinquentaChars(TextBox_Titulo.Text) && validacao.validarCinquentaChars(TextBox_Autor.Text) && validacao.validarCinquentaChars(Textbox_Editora.Text) && validacao.validarCinquentaChars(TextBox_Edicao.Text) && validacao.validarCemChars(TextBox_Descricao.Text))
                {
                    mainWindow.alteraDadosLivros(long.Parse(TextBox_IdResultado.Text), TextBox_ISBN.Text, TextBox_Titulo.Text, TextBox_Autor.Text, Textbox_Editora.Text, TextBox_Edicao.Text, TextBox_Descricao.Text, temp);

                    this.Close();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Button_EditarLivro.IsEnabled = true;
        }
    }
}
