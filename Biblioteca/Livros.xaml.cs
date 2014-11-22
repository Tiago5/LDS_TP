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
    /// Interaction logic for Livro.xaml
    /// </summary>
    public partial class Livros : Window
    {
        private MainWindow mainWindow;
        private string tipoEmprestimo;
        public Livros(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            TextBox_IdBiblioteca.Text = mainWindow.Combobox_Escolha_Biblioteca.SelectedItem.ToString();
        }
        //Ao fechar informa a MainWindow que a janela livro foi fechada
        private void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.windowsInserirLivroAberta = false;
        }

        private void Button_SubmeterLivro_Click(object sender, RoutedEventArgs e)
        {
            Validacoes validacao = new Validacoes();
            if (validacao.validarISBN(TextBox_ISBN.Text) && validacao.validarCinquentaChars(TextBox_Titulo.Text) && validacao.validarCinquentaChars(TextBox_Autor.Text) && validacao.validarCinquentaChars(Textbox_Editora.Text) && validacao.validarCinquentaChars(TextBox_Edicao.Text) && validacao.validarCemChars(TextBox_Descricao.Text))
            {
                mainWindow.gravarLivro(TextBox_ISBN.Text, TextBox_Titulo.Text, TextBox_Autor.Text, Textbox_Editora.Text, TextBox_Edicao.Text,
                    TextBox_Descricao.Text, tipoEmprestimo);
                MainWindow.windowsInserirLivroAberta = false;
                this.Close();
            }
        }

        private void RButton_Normal_Checked(object sender, RoutedEventArgs e)
        {
            tipoEmprestimo = "Normal";
        }

        private void RButton_Expresso_Checked(object sender, RoutedEventArgs e)
        {
            tipoEmprestimo = "Expresso";
        }
    }
}
