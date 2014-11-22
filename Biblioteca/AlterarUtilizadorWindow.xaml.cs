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
    /// Interaction logic for AlterarUtilizadorWindow.xaml
    /// </summary>
    public partial class AlterarUtilizadorWindow : Window
    {
        private ListagemUtilizador ut;
        public AlterarUtilizadorWindow(ListagemUtilizador ut)
        {
            InitializeComponent();
            this.ut = ut;

        }

       //Conforme o estado do utilizador, a descrição do button é alterada conforme a ação necessária
        private void Button_desativarUtilizador_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Estado.Text == "Inativo")
            {
                //Chama o metodo na ListagemUtilizador
                ut.alteraEstadoUtilizador(long.Parse(TextBox_IdUtilizador.Text), "Ativo");
            }
            else
            {
                //Chama o metodo na ListagemUtilizador
                ut.alteraEstadoUtilizador(long.Parse(TextBox_IdUtilizador.Text), "Inativo");
            }
            this.Close();
        }

        private void Button_subMeter_Click(object sender, RoutedEventArgs e)
        {
            Validacoes validacao = new Validacoes();
            if (validacao.validarCinquentaChars(TextBox_NomeUtilizador.Text) && validacao.validarTelefone(TextBox_Telefone.Text) && validacao.validarEmail(TextBox_Email.Text) && validacao.validarPassword(TextBox_PassWord.Text) && validacao.validarCinquentaChars(TextBox_Morada.Text) && validacao.validarCodigoPostal(TextBox_CodPostal.Text) && validacao.validarCinquentaChars(TextBox_Localidade.Text))
            {
                ut.alerarDadosUtilizador(long.Parse(TextBox_IdUtilizador.Text), TextBox_NomeUtilizador.Text, TextBox_Telefone.Text, TextBox_Email.Text, TextBox_PassWord.Text, TextBox_Morada.Text, TextBox_CodPostal.Text, TextBox_Localidade.Text, TextBox_Estado.Text);
                this.Close();
            }
        }
        //Informa a ListagemUtilizador a janela foi fechada
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ListagemUtilizador.WindowsAlterarUtilizadorAberta = false;
        }

      
    }
}
