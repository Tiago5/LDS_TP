using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Biblioteca
{
    public class Validacoes
    {

        public Validacoes() { }

        //recebe como argumento uma variavel do tipo textbox
        public Boolean validarEmail(string email)
        {
            //email é convertido para texto com o toString uma vez que a funcao Match() recebe uma string e uma expressao regular
            if (!(Regex.Match(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Success))
            {
                //caso o email seja invalido devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Email inválido ou vazio", "Email", MessageBoxButton.OK, MessageBoxImage.Error);
                //e devolvido o focus ao campo onde teremos de escrever o email
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarTelefone(string telefone)
        {
            //telefone é convertido para texto com o toString uma vez que a funcao Match() recebe uma string e uma expressao regular
            if (!(Regex.Match(telefone, @"^[1-9]{9}$").Success))
            {
                //caso o telefone seja invalido devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Numero de Telefone inválido ou vazio", "Telefone", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarCodigoPostal(string codigoPostal)
        {
            //codigo postal é convertido para texto com o toString uma vez que a funcao Match() recebe uma string e uma expressao regular
            if (!(Regex.Match(codigoPostal, @"^\d{4}(-\d{3})?$").Success))
            {
                //caso o codigo postal seja invalido devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Formato: NNNN-NNN", "Codigo Postal inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarCinquentaChars(string str)
        {
            //é verificado se o tamanho da string é menor ou igual que 50
            if (!(str.Length <= 50))
            {
                //caso a str seja invalida devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Campos não podem estar vazios nem exceder os 50 caracteres", "Mensagem", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarCemChars(string str)
        {
            //é verificado se o tamanho da string é menor ou igual que 100
            if (!(str.Length <= 100))
            {
                //caso a str seja invalida devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Campos não podem estar vazios nem exceder os 100 caracteres", "Descrição", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarISBN(string isbn)
        {
            //isbn é convertido para texto com o toString uma vez que a funcao Match() recebe uma string e uma expressao regular
            if (!(Regex.Match(isbn, @"^[1-9]{13}$").Success))
            {
                //caso o isbn seja invalido devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("ISBN inválido", "ISBN", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarId(string id)
        {
            //isbn é convertido para texto com o toString uma vez que a funcao Match() recebe uma string e uma expressao regular
            if (!(Regex.Match(id, @"^[1-9]{1,9}$").Success))
            {
                //caso o id seja invalido devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Id inválido", "Id", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        //recebe como argumento uma variavel do tipo textbox 
        public Boolean validarPassword(string password)
        {
            //password é convertido para texto com o toString uma vez que a funcao Match() recebe uma string e uma expressao regular
            if (!(Regex.Match(password, @"^[0-9A-Za-z!@\.;:'?-]{1,10}$").Success))
            {
                //caso a password seja invalida devolve esta janela e respectiva mensagem de erro
                MessageBox.Show("Não pode exceder os 10 caracteres", "Password", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}