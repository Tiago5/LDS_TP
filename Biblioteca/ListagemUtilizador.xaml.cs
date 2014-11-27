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
using System.Collections.ObjectModel;

namespace Biblioteca
{
    /// <summary>
    /// Interaction logic for ListagemUtilizadorWindow.xaml
    /// </summary>
    public partial class ListagemUtilizador : Window
    {
        ObservableCollection<Utilizador> utilizadores = new ObservableCollection<Utilizador>();
        ObservableCollection<Utilizador> utilizadoresPesquisados = new ObservableCollection<Utilizador>();
        private Sqlite_Helper dbListaUtilizadores;
        private Utilizadores inserirUtilizadorWindow;
        private Boolean viewPesquisa = false;
        private AlterarUtilizadorWindow alterarUtilizadorWindow;
        public static Boolean WindowsAlterarUtilizadorAberta = false;
        public static Boolean windowsInserirUtilizadorAberta = false;
        private MainWindow mainWindow;

        public ListagemUtilizador(MainWindow main)
        {
            InitializeComponent();
            dbListaUtilizadores = new Sqlite_Helper(this);
            dbListaUtilizadores.carregarUtilizadores();
            ListView_Utilizadores.ItemsSource = utilizadores;
            this.mainWindow = main;
        }
        //Adiciona utilizadores a lista
        public void adicionaUtilizadores(long idU, string nome, string telefone, string email, string passWord, string morada, string cp, string localidade, string estado)
        {
            utilizadores.Add(new Utilizador
            {
                IdUtilizador = idU,
                NomeUtilizador = nome,
                TelefoneUtilizador = telefone,
                EMail = email,
                PassWord = passWord,
                MoradaUtilizador = morada,
                CodigoPostal = cp,
                Localidade = localidade,
                Estado = estado
            });
        }



        // Ao fechar fecha todas as jalelas aberta por esta
        private void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Fecha a janela Lista utilizadores e indica á Mainwindow que a janela foi fechada
            MainWindow.windowsUtilizadoresAberta = false;

            if (windowsInserirUtilizadorAberta == true)
            {
                inserirUtilizadorWindow.Close();
            }
            // Fecha a janela Lista utilizadores e indica á Mainwindow que a janela foi fechada/////////
            if (WindowsAlterarUtilizadorAberta == true)
            {
                alterarUtilizadorWindow.Close();
            }

        }
        //Button para inserir Utilizador
        private void Button_InserirUtilizador_Click(object sender, RoutedEventArgs e)
        {
                Button_InserirUtilizador.IsEnabled = false;

                //Indica que foi aberta
                windowsInserirUtilizadorAberta = true;
                //Nota: na class Utilizadores não intancia a class Sqlite_helper
                //      o metodo para gravar o utilizador é chamado a partir desta class
                inserirUtilizadorWindow = new Utilizadores(this);
                inserirUtilizadorWindow.Show();

        }


        // Chama o metodo para gravar um utilizador na BD
        // É chamado na class Utilizador onde são inseridos os dados dos utilizadores
        public void gravaUtilizador(string NomeUtilizador, string TelefoneUtilizador, string EMail, string PassWord, string MoradaUtilizador, string CodigoPostal, string Localidade)
        {
            dbListaUtilizadores.insertUtilizador(NomeUtilizador, TelefoneUtilizador, EMail, PassWord, MoradaUtilizador, CodigoPostal, Localidade);
            //vai actualizar a lista                        
            atualizarListaViewUtilizadores();
        }
        //Chama o metodo para alterar o estado do utilizador e chama o metodo para
        // atualizar a listagem dos utilizadores disponivel no ecra
        public void alteraEstadoUtilizador(long idUtilizador, string estado)
        {
            //Envia o id do utilizador e o estado do utilizador que esta visualisado na class AlterarUtilizadorWindow
            //é chamado a partir da class AlterarUtilizadorWindow
            dbListaUtilizadores.alteraEstadoUtilizador(idUtilizador, estado);
            atualizarListaViewUtilizadores();

        }

        // Chama o metodo para gravar os dados alterados de um utilizador na BD
        // É chamado na class AlterarUtilizadorWindow onde são inseridos os dados dos utilizadores
        public void alerarDadosUtilizador(long idU, string nome, string telefone, string email, string passWord, string morada, string cp, string localidade, string estado)
        {
            dbListaUtilizadores.AlterarUtilizador(idU, nome, telefone, email, passWord, morada, cp, localidade, estado);
            //vai actualizar a lista                        
            atualizarListaViewUtilizadores();
        }


        // Atualiza a listagem dos utilizadores disponivel no ecra
        public void atualizarListaViewUtilizadores()
        {
            viewPesquisa = false;
            utilizadores.Clear();
            utilizadoresPesquisados.Clear();
            dbListaUtilizadores.carregarUtilizadores();
            ListView_Utilizadores.ItemsSource = utilizadores;
            Textbox_PesqUtilizador.Text = "";
        }

        // visualiza os dados do utilizador para poderem ser alterados
        private void ListView_Utilizadores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (WindowsAlterarUtilizadorAberta == false)
                {
                    // visualiza o utilizador da lista total de utilizadores ou apenas de uma pesquida 
                    if (viewPesquisa == false)
                    {

                        alterarUtilizadorWindow = new AlterarUtilizadorWindow(this);
                        alterarUtilizadorWindow.Show();
                        alterarUtilizadorWindow.TextBox_IdUtilizador.Text = utilizadores[ListView_Utilizadores.SelectedIndex].IdUtilizador.ToString();
                        alterarUtilizadorWindow.TextBox_NomeUtilizador.Text = utilizadores[ListView_Utilizadores.SelectedIndex].NomeUtilizador;
                        alterarUtilizadorWindow.TextBox_PassWord.Text = utilizadores[ListView_Utilizadores.SelectedIndex].PassWord;
                        alterarUtilizadorWindow.TextBox_CodPostal.Text = utilizadores[ListView_Utilizadores.SelectedIndex].CodigoPostal;
                        alterarUtilizadorWindow.TextBox_Email.Text = utilizadores[ListView_Utilizadores.SelectedIndex].EMail;
                        alterarUtilizadorWindow.TextBox_Localidade.Text = utilizadores[ListView_Utilizadores.SelectedIndex].Localidade;
                        alterarUtilizadorWindow.TextBox_Telefone.Text = utilizadores[ListView_Utilizadores.SelectedIndex].TelefoneUtilizador;
                        alterarUtilizadorWindow.TextBox_Estado.Text = utilizadores[ListView_Utilizadores.SelectedIndex].Estado;
                        alterarUtilizadorWindow.TextBox_Morada.Text = utilizadores[ListView_Utilizadores.SelectedIndex].MoradaUtilizador;
                        //Conforme estiver o estado do utilizador altera a informaçao do button (Atiivar/Desativar) 
                        if (alterarUtilizadorWindow.TextBox_Estado.Text == "Inativo")
                        {
                            alterarUtilizadorWindow.Button_desativarUtilizador.Content = "Ativar";

                        }
                        else
                        {
                            alterarUtilizadorWindow.Button_desativarUtilizador.Content = "Desativar";
                        }
                    }
                    else
                    {
                        alterarUtilizadorWindow = new AlterarUtilizadorWindow(this);
                        alterarUtilizadorWindow.Show();
                        alterarUtilizadorWindow.TextBox_IdUtilizador.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].IdUtilizador.ToString();
                        alterarUtilizadorWindow.TextBox_NomeUtilizador.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].NomeUtilizador;
                        alterarUtilizadorWindow.TextBox_PassWord.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].PassWord;
                        alterarUtilizadorWindow.TextBox_CodPostal.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].CodigoPostal;
                        alterarUtilizadorWindow.TextBox_Email.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].EMail;
                        alterarUtilizadorWindow.TextBox_Localidade.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].Localidade;
                        alterarUtilizadorWindow.TextBox_Telefone.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].TelefoneUtilizador;
                        alterarUtilizadorWindow.TextBox_Estado.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].Estado;
                        alterarUtilizadorWindow.TextBox_Morada.Text = utilizadoresPesquisados[ListView_Utilizadores.SelectedIndex].MoradaUtilizador;
                        //Conforme estiver o estado do utilizador altera a informaçao do button (Atiivar/Desativar) 
                        if (alterarUtilizadorWindow.TextBox_Estado.Text == "Inativo")
                        {
                            alterarUtilizadorWindow.Button_desativarUtilizador.Content = "Ativar";

                        }
                        else
                        {
                            alterarUtilizadorWindow.Button_desativarUtilizador.Content = "Desativar";
                        }
                    }
                    // Indica que a Janela de alterarUtilizadorWindow está aberta
                    WindowsAlterarUtilizadorAberta = true;
                }
            }
            catch (Exception)
            {
                alterarUtilizadorWindow.Close();
                MessageBox.Show("Não Selecionou nenhum item!", "Excepção", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //Pesquisa dados na lista de utilizadores
        public void PesquisaDadosUtilizador(string dadosPesquisar)
        {
            //se dadosPesquisar tiver conteudo faz a pesquisa se não lista novamente todos os utilizadores
            if (dadosPesquisar != "")
            {
                viewPesquisa = true;
                utilizadoresPesquisados.Clear();
                foreach (Utilizador u in utilizadores)
                {
                    if (u.NomeUtilizador.ToLower().Contains(dadosPesquisar.ToLower()) || u.IdUtilizador.ToString().ToLower().Contains(dadosPesquisar.ToLower())
                        || u.TelefoneUtilizador.ToLower().Contains(dadosPesquisar.ToLower()) || u.EMail.ToLower().Contains(dadosPesquisar.ToLower()))
                    {
                        utilizadoresPesquisados.Add(u);
                    }
                }
                ListView_Utilizadores.ItemsSource = utilizadoresPesquisados;
                if (utilizadoresPesquisados.Count == 0)
                {
                    MessageBox.Show("NAO FORAM ENCONTRADOS UTILIZADORES");
                    viewPesquisa = false;
                    ListView_Utilizadores.ItemsSource = utilizadores;
                    Textbox_PesqUtilizador.Text = "";
                }
            }
            else
            {
                // o utilizador ve a lista total de utilizadores
                viewPesquisa = false;
                ListView_Utilizadores.ItemsSource = utilizadores;
            }
        }

        private void Button_PesqUtilizador_Click(object sender, RoutedEventArgs e)
        {
            PesquisaDadosUtilizador(Textbox_PesqUtilizador.Text);
        }
    }
}
