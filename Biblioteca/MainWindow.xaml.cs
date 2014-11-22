﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Biblioteca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Livro> livros = new ObservableCollection<Livro>();
        ObservableCollection<Livro> livrosPesquisados = new ObservableCollection<Livro>();
        ListagemUtilizador listagemUtilizadorWindow;
        Livros livrosInserirWindow;
        AlterarLivroView alterarLivroView;
        DetalhesLivroWindow detalhesLivrowindow;
        EmprestimoLivroWindow empretimoLivroWindow;
        DevolvelLivroWindow devolverLivroWindow;
        ListagemEmprestimos listagemEmprestimos;

        /// Para impedir a abertura de novas janelas
        public static Boolean windowsUtilizadoresAberta = false;
        public static Boolean windowsInserirLivroAberta = false;
        public static Boolean windowsEditarLivroAberta = false;
        public static Boolean windowDetalhesLivroAberta = false;
        public static Boolean windowEmpretimoAberta = false;
        public static Boolean windowDevolverLivro = false;
        public static Boolean windowListagemEmprestimosAberta = false;
        private Sqlite_Helper dbListaLivros;
        private Boolean viewPesquisa = false;

        public MainWindow()
        {
            InitializeComponent();
            dbListaLivros = new Sqlite_Helper(this);
            dbListaLivros.carregarComboBox();
            Combobox_Escolha_Biblioteca.SelectedIndex = 0;
            ///ListaView que recebe os livros conforme o indice escolhido na ComboBox
            ListViewLivros.ItemsSource = livros;
        }

        // button para abrir a listagem de utilizadores
        private void Button_Utilizador_Click(object sender, RoutedEventArgs e)
        {
            //Só abre a janela se ela estiver fechada.
            if (windowsUtilizadoresAberta == false)
            {
                windowsUtilizadoresAberta = true;
                listagemUtilizadorWindow = new ListagemUtilizador();
                listagemUtilizadorWindow.Show();
            }
        }

        // button para adicionar livro
        private void Button_InLivro_Click(object sender, RoutedEventArgs e)
        {
            if (Combobox_Escolha_Biblioteca.SelectedItem.ToString() != "0 - Todos os livros")
            {


                //Só abre a janela se ela estiver fechada
                if (windowsInserirLivroAberta == false)
                {
                    windowsInserirLivroAberta = true;
                    livrosInserirWindow = new Livros(this);
                    livrosInserirWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("POR FAVOR, SELECIONE UMA DAS QUATRO BIBLIOTECAS");
            }
        }

        //Procurar id na lista de livros da biblioteca de trabalho para alterar
        public Boolean procurarIdLivro(long idLivro)
        {
            foreach (Livro l in livros)
            {
                if (l.IdLivro == idLivro)
                {
                    alterarLivroView.TextBox_Autor.Text = l.Autor;
                    alterarLivroView.TextBox_Descricao.Text = l.Descricao;
                    alterarLivroView.Textbox_Editora.Text = l.Editora;
                    alterarLivroView.TextBox_ISBN.Text = l.ISBN;
                    alterarLivroView.TextBox_Titulo.Text = l.Titulo;
                    alterarLivroView.TextBox_IdBiblioteca.Text = l.IdBiblioteca.ToString();
                    alterarLivroView.TextBox_Edicao.Text = l.Edicao;
                    if (l.TipoEmprestimo == "Expresso")
                    {
                        alterarLivroView.RButton_Expresso.IsChecked = true;
                    }
                    else
                    {
                        alterarLivroView.RButton_Normal.IsChecked = true;
                    }
                    return true;
                }
            }

            alterarLivroView.TextBox_Autor.Text = "";
            alterarLivroView.TextBox_Descricao.Text = "";
            alterarLivroView.Textbox_Editora.Text = "";
            alterarLivroView.TextBox_ISBN.Text = "";
            alterarLivroView.TextBox_Titulo.Text = "";
            alterarLivroView.TextBox_IdBiblioteca.Text = "";
            alterarLivroView.TextBox_Edicao.Text = "";

            alterarLivroView.RButton_Expresso.IsChecked = false;

            alterarLivroView.RButton_Normal.IsChecked = false;


            return false;
        }

        // Prucura utilizador para empretimo 
        public string prucuraUtilizadorParaEmprestimo(long idUtilizador)
        {
            return dbListaLivros.psquisaUtilizadorPorId(idUtilizador);
        }

        // //Procurar id na lista de livros da biblioteca de trabalho para alterar
        public string procurarLivroParaEmprestimo(long idLivro)
        {

            foreach (Livro l in livros)
            {
                if (l.IdLivro == idLivro)
                {
                    empretimoLivroWindow.TextBox_Autor.Text = l.Autor;
                    empretimoLivroWindow.TextBox_Descricao.Text = l.Descricao;
                    empretimoLivroWindow.Textbox_Editora.Text = l.Editora;
                    empretimoLivroWindow.TextBox_ISBN.Text = l.ISBN;
                    empretimoLivroWindow.TextBox_Titulo.Text = l.Titulo;
                    empretimoLivroWindow.TextBox_IdBiblioteca.Text = l.IdBiblioteca.ToString();
                    empretimoLivroWindow.TextBox_Edicao.Text = l.Edicao;
                    if (l.TipoEmprestimo == "Expresso")
                    {
                        empretimoLivroWindow.RButton_Expresso.IsChecked = true;
                    }
                    else
                    {
                        empretimoLivroWindow.RButton_Normal.IsChecked = true;
                    }

                    return l.Estado;
                }
            }
            empretimoLivroWindow.TextBox_Autor.Text = "";
            empretimoLivroWindow.TextBox_Descricao.Text = "";
            empretimoLivroWindow.Textbox_Editora.Text = "";
            empretimoLivroWindow.TextBox_ISBN.Text = "";
            empretimoLivroWindow.TextBox_Titulo.Text = "";
            empretimoLivroWindow.TextBox_IdBiblioteca.Text = "";
            empretimoLivroWindow.TextBox_Edicao.Text = "";
            empretimoLivroWindow.TextBox_DataEntrega.Text = "";
            empretimoLivroWindow.RButton_Expresso.IsChecked = false;

            empretimoLivroWindow.RButton_Normal.IsChecked = false;

            return "O livro não existe na biblioteca";
        }

        //Chama o metodo para alterar o estado do livro da class Sqlite_helper
        public void alterarestadoLivro(long IdLivro, string Estado)
        {
            dbListaLivros.alterarEstadoLivro(IdLivro, Estado);
        }

        //Chama o metodo para gravar um emprestimo
        public void inserirEmprestimo(long IdUtilizador, long IdBiblioteca, long IdLivro, string DataInicio, string DataFim, string DataEntrega)
        {
            dbListaLivros.insertEmprestimo(IdUtilizador, IdBiblioteca, IdLivro, DataInicio, DataFim, DataEntrega);
        }

        //Enviar dados do novo livro para a funcao de gravar na BD e atualiza a vista dos livros
        public void gravarLivro(string ISBN, string Titulo, string Autor, string Editora, string Edicao, string Descricao,
            string TipoEmprestimo)
        {

            dbListaLivros.inserirLivro(getIdBibliotecaEscolhida(), ISBN, Titulo, Autor, Editora, Edicao, Descricao, TipoEmprestimo);
            atualizarListaViewLivros();
        }

        // Chama o metodo da BD para atualizar utilizador
        public void alteraDadosLivros(long idL, string isbn, string titulo, string autor, string editora, string edicao, string descricao, string tipoE)
        {
            dbListaLivros.alterarLivro(idL, isbn, titulo, autor, editora, edicao, descricao, tipoE);
            atualizarListaViewLivros();
        }

        // Chama o metodo da BD para atualizar emprestimo.
        public void devolucaoLivro(long IdEmprestimo, string DataEntrega)
        {
            dbListaLivros.devolucaoLivro(IdEmprestimo, DataEntrega);
        }

        // adiciona livros a Lista
        public void adicionaLivro(long idL, long idB, string isbn, string titulo, string autor, string editora, string edicao, string descricao, string tipoE, string estado)
        {
            livros.Add(new Livro { IdLivro = idL, IdBiblioteca = idB, ISBN = isbn, Titulo = titulo, Autor = autor, Editora = editora, Edicao = edicao, Descricao = descricao, TipoEmprestimo = tipoE, Estado = estado });
        }

        // adiciona librarias a comboBox
        public void adElementoComboBox(string a)
        {
            this.Combobox_Escolha_Biblioteca.Items.Add(a);
        }

        //Lista os livros conforme a biblioteca escolhiha
        private void Combobox_Escolha_Biblioteca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            atualizarListaViewLivros();
        }

        // Chama o medodo da BD para pesquisar livro

        public Boolean pesquisaLibroDB(long idLivro, string Estado)
        {

            return dbListaLivros.pesquisaIdLivro(idLivro, Estado);
        }

        //atualizar lista de livros
        public void atualizarListaViewLivros()
        {
            livros.Clear();
            viewPesquisa = false;
            livrosPesquisados.Clear();
            //Sempre que selecionamos uma biblioteca, os dados da mesma sao carregados para a listView
            //e a caixa de pesquisa é limpa
            ListViewLivros.ItemsSource = livros;
            TextBox_PesquisaLivros.Text = "";
            // extrai o id da biblioteca da combobox.
            dbListaLivros.carregarlistaLivros(getIdBibliotecaEscolhida());
            //Faz a contagem do total de livros em cada biblioteca
            Label_NumeroLivros.Content = livros.Count.ToString();
        }

        //Pesquisa dados na lista de livros
        public void PesquisaDadosLivros(string dadosPesquisar)
        {

            //se dadosPesquisar tiver conteudo faz a pesquisa se não lista novamente todos os livros
            if (dadosPesquisar != "")
            {
                viewPesquisa = true;
                livrosPesquisados.Clear();
                foreach (Livro l in livros)
                {
                    if (l.IdLivro.ToString().ToLower().Contains(dadosPesquisar.ToLower()) || l.Titulo.ToString().ToLower().Contains(dadosPesquisar.ToLower())
                        || l.Autor.ToLower().Contains(dadosPesquisar.ToLower()) || l.Editora.ToLower().Contains(dadosPesquisar.ToLower()))
                    {
                        livrosPesquisados.Add(l);
                    }

                }
                //o utilizador ve a lista dos livros encontrados
                ListViewLivros.ItemsSource = livrosPesquisados;
                //Se nao existirem elementos na lista devolve uma mensagem de aviso
                if (livrosPesquisados.Count == 0)
                {

                    MessageBox.Show("NÃO FORAM ENCONTRADOS LIVROS");
                    viewPesquisa = false;
                    ListViewLivros.ItemsSource = livros;
                    TextBox_PesquisaLivros.Text = "";
                }
            }
            else
            {
                // o utilizador ve a lista total de livros da biblioteca
                viewPesquisa = false;
                ListViewLivros.ItemsSource = livros;
            }
        }

        // Ao fechar a MainWindow verifica se há janelas abertas e fecha as janelas
        // BtUtilizadoresAtivo e BtLivroAtivo estão a true quando as janelas estão abertas
        private void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (windowsUtilizadoresAberta == true)
            {
                listagemUtilizadorWindow.Close();
            }
            if (windowsInserirLivroAberta == true)
            {
                livrosInserirWindow.Close();
            }
            if (windowsEditarLivroAberta == true)
            {
                alterarLivroView.Close();
            }

            if (windowDetalhesLivroAberta == true)
            {
                detalhesLivrowindow.Close();
            }

            if (windowEmpretimoAberta == true)
            {
                empretimoLivroWindow.Close();
            }

            if (windowDevolverLivro == true)
            {
                devolverLivroWindow.Close();
            }
            if (windowListagemEmprestimosAberta == true)
            {
                listagemEmprestimos.Close();
            }

        }

        /// <summary>
        /// Retorna o ID da biblioteca selecionada na comboBox (biblioteca de trabalho)
        /// </summary>
        /// <returns>ID Biblioteca</returns>
        public long getIdBibliotecaEscolhida()
        {
            Object bibliotecaSelecionada = Combobox_Escolha_Biblioteca.SelectedItem;
            // extrai o id da biblioteca da combobox.
            string[] temp = bibliotecaSelecionada.ToString().Split(' ');
            return long.Parse(temp[0]);
        }

        private void Button_EditarLivro_Click(object sender, RoutedEventArgs e)
        {
            if (Combobox_Escolha_Biblioteca.SelectedItem.ToString() != "0 - Todos os livros")
            {
                if (windowsEditarLivroAberta == false)
                {

                    windowsEditarLivroAberta = true;
                    alterarLivroView = new AlterarLivroView(this);
                    alterarLivroView.Show();
                }
            }
            else
            {
                MessageBox.Show("POR FAVOR, SELECIONE UMA DAS QUATRO BIBLIOTECAS");
            }

        }

        private void Button_Pesquisas_Click(object sender, RoutedEventArgs e)
        {
            PesquisaDadosLivros(TextBox_PesquisaLivros.Text);
        }

        private void ListViewLivros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if (windowDetalhesLivroAberta == false)
                {
                    // visualiza o livro da lista total de livros ou apenas de uma pesquida (detalhes)
                    if (viewPesquisa == false)
                    {
                        detalhesLivrowindow = new DetalhesLivroWindow();
                        detalhesLivrowindow.Show();
                        detalhesLivrowindow.TextBox_IdLivro.Text = livros[ListViewLivros.SelectedIndex].IdLivro.ToString();
                        detalhesLivrowindow.TextBox_ISBN.Text = livros[ListViewLivros.SelectedIndex].ISBN.ToString();
                        detalhesLivrowindow.TextBox_Titulo.Text = livros[ListViewLivros.SelectedIndex].Titulo.ToString();
                        detalhesLivrowindow.TextBox_Autor.Text = livros[ListViewLivros.SelectedIndex].Autor.ToString();
                        detalhesLivrowindow.Textbox_Editora.Text = livros[ListViewLivros.SelectedIndex].Editora.ToString();
                        detalhesLivrowindow.TextBox_Edicao.Text = livros[ListViewLivros.SelectedIndex].Edicao.ToString();
                        detalhesLivrowindow.TextBox_Descricao.Text = livros[ListViewLivros.SelectedIndex].Descricao.ToString();
                        detalhesLivrowindow.TextBox_IdBiblioteca.Text = livros[ListViewLivros.SelectedIndex].IdBiblioteca.ToString();
                        detalhesLivrowindow.TextBox_Estado.Text = livros[ListViewLivros.SelectedIndex].Estado.ToString();
                        if (livros[ListViewLivros.SelectedIndex].TipoEmprestimo.ToString() == "Normal")
                        {
                            detalhesLivrowindow.RButton_Normal.IsChecked = true;
                        }
                        else
                        {
                            detalhesLivrowindow.RButton_Expresso.IsChecked = true;
                        }
                    }
                    else
                    {
                        detalhesLivrowindow = new DetalhesLivroWindow();
                        detalhesLivrowindow.Show();
                        detalhesLivrowindow.TextBox_IdLivro.Text = livrosPesquisados[ListViewLivros.SelectedIndex].IdLivro.ToString();
                        detalhesLivrowindow.TextBox_ISBN.Text = livrosPesquisados[ListViewLivros.SelectedIndex].ISBN.ToString();
                        detalhesLivrowindow.TextBox_Titulo.Text = livrosPesquisados[ListViewLivros.SelectedIndex].Titulo.ToString();
                        detalhesLivrowindow.TextBox_Autor.Text = livrosPesquisados[ListViewLivros.SelectedIndex].Autor.ToString();
                        detalhesLivrowindow.Textbox_Editora.Text = livrosPesquisados[ListViewLivros.SelectedIndex].Editora.ToString();
                        detalhesLivrowindow.TextBox_Edicao.Text = livrosPesquisados[ListViewLivros.SelectedIndex].Edicao.ToString();
                        detalhesLivrowindow.TextBox_Descricao.Text = livrosPesquisados[ListViewLivros.SelectedIndex].Descricao.ToString();
                        detalhesLivrowindow.TextBox_IdBiblioteca.Text = livrosPesquisados[ListViewLivros.SelectedIndex].IdBiblioteca.ToString();
                        detalhesLivrowindow.TextBox_Estado.Text = livrosPesquisados[ListViewLivros.SelectedIndex].Estado.ToString();
                        if (livrosPesquisados[ListViewLivros.SelectedIndex].TipoEmprestimo.ToString() == "Normal")
                        {
                            detalhesLivrowindow.RButton_Normal.IsChecked = true;
                        }
                        else
                        {
                            detalhesLivrowindow.RButton_Expresso.IsChecked = true;
                        }
                    }


                    windowDetalhesLivroAberta = true;
                }
            }
            catch (Exception)
            {
                detalhesLivrowindow.Close();
                MessageBox.Show("Não Selecionou nenhum item!", "Excepção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // button emprestimo
        private void Button_Emprestimo_Click(object sender, RoutedEventArgs e)
        {
            if (Combobox_Escolha_Biblioteca.SelectedItem.ToString() != "0 - Todos os livros")
            {
                //Só abre a janela se ela estiver fechada
                if (windowEmpretimoAberta == false)
                {
                    windowEmpretimoAberta = true;
                    empretimoLivroWindow = new EmprestimoLivroWindow(this);
                    empretimoLivroWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("POR FAVOR, SELECIONE UMA DAS QUATRO BIBLIOTECAS");
            }
        }

        private void Button_Devolucao_Click(object sender, RoutedEventArgs e)
        {

            if (Combobox_Escolha_Biblioteca.SelectedItem.ToString() != "0 - Todos os livros")
            {
                if (windowDevolverLivro == false)
                {
                    devolverLivroWindow = new DevolvelLivroWindow(this);
                    windowDevolverLivro = true;
                    devolverLivroWindow.Show();
                }

            }
            else
            {
                MessageBox.Show("POR FAVOR, SELECIONE UMA DAS QUATRO BIBLIOTECAS");
            }

        }

        private void Button_ListarEmprestimo_Click(object sender, RoutedEventArgs e)
        {


            if (Combobox_Escolha_Biblioteca.SelectedItem.ToString() != "0 - Todos os livros")
            {
                if (windowListagemEmprestimosAberta == false)
                {
                    listagemEmprestimos = new ListagemEmprestimos(getIdBibliotecaEscolhida());
                    windowListagemEmprestimosAberta = true;
                    listagemEmprestimos.Show();
                }

            }
            else
            {
                MessageBox.Show("POR FAVOR, SELECIONE UMA DAS QUATRO BIBLIOTECAS");
            }

        }

    }
}
