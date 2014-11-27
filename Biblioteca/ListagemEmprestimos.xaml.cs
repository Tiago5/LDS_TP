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
    /// Interaction logic for ListagemEmprestimos.xaml
    /// </summary>
    public partial class ListagemEmprestimos : Window
    {
        ObservableCollection<Emprestimo> emprestimos = new ObservableCollection<Emprestimo>();
        private Sqlite_Helper dbEmprestimos;
        private long idB;
        private MainWindow mainWindow;

        public ListagemEmprestimos(long idB, MainWindow main)
        {
            InitializeComponent();
            this.idB = idB;
            dbEmprestimos = new Sqlite_Helper(this);
            RButton_PorDevolver.IsChecked = true;
            this.mainWindow = main;
        }

        // Adiciona emprestimos à lista de emprestimos para visualizar na view
        public void adicionarEmprestimosLista(long IdE, long IdU, long IdB, long IdL, string DataI, string DataF, string DataE)
        {
            emprestimos.Add(new Emprestimo { IdEmprestimo = IdE, IdUtilizador = IdU, IdBiblioteca = IdB, IdLivro = IdL, DataInicio = DataI, DataFim = DataF, DataEntrega = DataE });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Button_ListarEmprestimo.IsEnabled = true;
        }
        //Listagem dos emprestimos por devolver
        private void RButton_PorDevolver_Checked(object sender, RoutedEventArgs e)
        {
            emprestimos.Clear();
            dbEmprestimos.carregarEmprestimos(idB, true);
            ListView_Emprestimos.ItemsSource = emprestimos;
        }
        //Listagem de todos os emprestimos
        private void RButton_Todos_Checked(object sender, RoutedEventArgs e)
        {
            emprestimos.Clear();
            dbEmprestimos.carregarEmprestimos(idB, false);
            ListView_Emprestimos.ItemsSource = emprestimos;
        }
    }
}
