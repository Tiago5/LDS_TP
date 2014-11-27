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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteca.Inicio
{
    /// <summary>
    /// #Interaction logic for WindowEscolhaBiblioteca.xaml
    /// </summary>
    public partial class WindowEscolhaBiblioteca : Window
    {
        private Sqlite_Helper dbListaLivros;
        private MainWindow mainWindows;
        public WindowEscolhaBiblioteca()
        {
            InitializeComponent();
            //Para centrar a janela
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Show();
            dbListaLivros = new Sqlite_Helper(this);
            dbListaLivros.carregarComboBox();
            this.ComboBoxBiblioteca.SelectedIndex = 0;
        }
        /// <summary>
        /// #Adiciona bibliotecas à comboBox
        /// </summary>
        /// <param name="a"></param>
        public void adElementoComboBox(string a)
        {
            this.ComboBoxBiblioteca.Items.Add(a);
        }
        /// <summary>
        /// #Inicialisa a windows principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBiblioteca_Click(object sender, RoutedEventArgs e)
        {
            mainWindows = new MainWindow(ComboBoxBiblioteca.SelectedItem);
            mainWindows.Show();
            this.Close();
        }

        /// <summary>
        /// #Retorna o ID da biblioteca selecionada na combobox
        /// </summary>
        /// <returns>ID Biblioteca</returns>
        public long getIdBibliotecaEscolhida(Object bibliotecaSelecionada)
        {
            // extrai o id da biblioteca da combobox.
            string[] temp = bibliotecaSelecionada.ToString().Split('-');
            return long.Parse(temp[0]);
        }
        /// <summary>
        /// #Altera a imagem conforme a biblioteca escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxBiblioteca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (getIdBibliotecaEscolhida(ComboBoxBiblioteca.SelectedItem) == 1)
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Imagens/1 (28).jpg")));
            } 
            if (getIdBibliotecaEscolhida(ComboBoxBiblioteca.SelectedItem) == 2)
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Imagens/biblioteca.jpg")));
            }
            if (getIdBibliotecaEscolhida(ComboBoxBiblioteca.SelectedItem) == 3)
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Imagens/Hausel-Biblioteca-de-Stuttgart-Germany-.png")));
            }
            if (getIdBibliotecaEscolhida(ComboBoxBiblioteca.SelectedItem) == 4)
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Imagens/medium_94520121.jpg")));
            } 
        }

    }
}
