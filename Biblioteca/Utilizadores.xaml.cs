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
    /// Interaction logic for Utilizadores.xaml
    /// </summary>
    public partial class Utilizadores : Window
    {

        private ListagemUtilizador ut;
        public Utilizadores(ListagemUtilizador ut)
        {
            InitializeComponent();
            this.ut = ut;
        }
        // ao fechar a janela indica ao obj ListagemUtilizador que a janela foi fecha
        private void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ListagemUtilizador.windowsInserirUtilizadorAberta = false;
        }
        // executa o metodo da classe ListagemUtilizadores para enviar os dados do novo utilizador para a BD 
        private void Button_subMeter_Click(object sender, RoutedEventArgs e)
        {
            Validacoes validacao = new Validacoes();
            if (validacao.validarCinquentaChars(TextBox_NomeUtilizador.Text) && validacao.validarTelefone(TextBox_Telefone.Text) && validacao.validarEmail(TextBox_Email.Text) && validacao.validarPassword(TextBox_PassWord.Text) && validacao.validarCinquentaChars(TextBox_Morada.Text) && validacao.validarCodigoPostal(TextBox_CodPostal.Text) && validacao.validarCinquentaChars(TextBox_Localidade.Text))
            {
                ut.gravaUtilizador(TextBox_NomeUtilizador.Text, TextBox_Telefone.Text, TextBox_Email.Text, TextBox_PassWord.Text, TextBox_Morada.Text, TextBox_CodPostal.Text, TextBox_Localidade.Text);
                this.Close();
            }
        }

    }
}
