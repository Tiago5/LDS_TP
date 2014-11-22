using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteca
{
    class Sqlite_Helper
    {
        private MainWindow mainWindow;
        private ListagemUtilizador lista;
        private ListagemEmprestimos listagemEmprestimos;
        //Intancia com a Mainwindow
        public Sqlite_Helper(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public Sqlite_Helper(ListagemUtilizador lista)
        {
            this.lista = lista;
        }

        public Sqlite_Helper(ListagemEmprestimos l)
        {
            this.listagemEmprestimos = l;
        }

        public Sqlite_Helper()
        {

        }

        //Inserir livro
        public void inserirLivro(long IdBiblioteca, string ISBN, string Titulo, string Autor, string Editora, string Edicao, string Descricao,
            string TipoEmprestimo)
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"INSERT INTO Livros (IdBiblioteca,ISBN,Titulo, Autor,Editora, Edicao, Descricao, TipoEmprestimo) 
VALUES( " + IdBiblioteca + ",'" + ISBN + "','" + Titulo + "','" + Autor + "','" + Editora + "', '" + Edicao + "','" + Descricao + "','" + TipoEmprestimo + "')";

                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }

        }



        // pesquisa um idlivro e retorna true ou false
        public Boolean pesquisaIdLivro(long IdLivro, string Estado)
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            Boolean idReturn = false;
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"SELECT IdLivro FROM Livros WHERE IdLivro=" + IdLivro + " AND Estado='" + Estado + "' ";
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                        
                            idReturn = true;
                        }
                    }
                    // Iniciar a limpeza da conexão e "pipe"
                    cmd.Dispose();
                }
                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
            return idReturn;
        }
        // altera o estado do livro na base de dados
        public void alterarEstadoLivro(long IdLivro, String Estado)
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"UPDATE Livros SET Estado= '" + Estado + "' WHERE IdLivro=" + IdLivro + "";
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }
                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }


        }
        //Alterar o livro na base de dados
        public void alterarLivro(long IdLivro, string ISBN, string Titulo, string Autor, string Editora, string Edicao, string Descricao,
            string TipoEmprestimo)
        {

            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"UPDATE Livros SET ISBN= '" + ISBN + "',Titulo='" + Titulo + "',Autor='" + Autor + "',Editora='" + Editora + "',Edicao='" + Edicao + "',Descricao='" + Descricao + "',TipoEmprestimo='" + TipoEmprestimo + "' WHERE IdLivro=" + IdLivro + "";
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }
                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }


        }

        //Carregar os livros da listView
        public void carregarlistaLivros(long bibliotecaSelecionada)
        {

            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    //Se a biblioteca selecionada fôr 0 mostra todos os livros das quatro bibliotecas
                    //caso contrário mostra apenas os da biblioteca selecionada
                    if (bibliotecaSelecionada != 0)
                    {
                        cmd.CommandText = @"SELECT IdLivro, IdBiblioteca, ISBN, Titulo, Autor, Editora, Edicao,  Descricao, TipoEmprestimo, Estado FROM Livros WHERE IdBiblioteca=" + mainWindow.getIdBibliotecaEscolhida() + "";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT IdLivro, IdBiblioteca, ISBN, Titulo, Autor, Editora, Edicao, Descricao, TipoEmprestimo, Estado FROM Livros";
                    }
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long IdLivro = reader.GetInt64(0);
                            long IdBiblioteca = reader.GetInt64(1);
                            string ISBN = reader.GetString(2);
                            string Titulo = reader.GetString(3);
                            string Autor = reader.GetString(4);
                            string Editora = reader.GetString(5);
                            string Edicao = reader.GetString(6);
                            string Descricao = reader.GetString(7);
                            string TipoEmprestimo = reader.GetString(8);
                            string Estado = reader.GetString(9);
                            mainWindow.adicionaLivro(IdLivro, IdBiblioteca, ISBN, Titulo, Autor, Editora, Edicao, Descricao, TipoEmprestimo, Estado);
                        }
                    }
                    // Iniciar a limpeza da conexão e "pipe"
                    cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }
        /// <summary>
        /// Carrega todas a bibliotecas na ComboBox mais a "0 - Todos os livros" no indice zero
        /// para depois ser listada a lista de livros conforme a bibioteca escolhida
        /// </summary>

        public void carregarComboBox()
        {
            // Localizar Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);
            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())
            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando a ser executado
                    cmd.CommandText = @"SELECT IdBiblioteca, NomeBiblioteca FROM Bibliotecas";
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                    {
                        string juntar = "";
                        //Adiciona no indice zero da ComboBox "0 - Todos os livros"
                        this.mainWindow.adElementoComboBox("0 - Todos os livros");
                        while (reader.Read())
                        {
                            long ID = reader.GetInt64(0);
                            string Data = reader.GetString(1);
                            juntar = ID + " - " + Data;
                            this.mainWindow.adElementoComboBox(juntar);
                        }
                    }
                    // Iniciar a limpeza da conexão e "pipe"
                    cmd.Dispose();
                }
                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }


        // Carrega os utilizadores da BD
        public void carregarUtilizadores()
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"SELECT IdUtilizador, NomeUtilizador, TelefoneUtilizador, EMail, Password,MoradaUtilizador, CodigoPostal,Localidade, Estado FROM  Utilizadores";
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long IdUtilizador = reader.GetInt64(0);
                            string NomeUtilizador = reader.GetString(1);
                            string TelefoneUtilizador = reader.GetString(2);
                            string EMail = reader.GetString(3);
                            string PassWord = reader.GetString(4);
                            string MoradaUtilizador = reader.GetString(5);
                            string CodigoPostal = reader.GetString(6);
                            string Localidade = reader.GetString(7);
                            string Estado = reader.GetString(8);
                            //chama o metodo para adiocionar os utilizadores a lista 
                            lista.adicionaUtilizadores(IdUtilizador, NomeUtilizador, TelefoneUtilizador, EMail, PassWord,
                                MoradaUtilizador, CodigoPostal, Localidade, Estado);
                        }
                    }
                    // Iniciar a limpeza da conexão e "pipe"
                    cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }
        // inserir utilizador na BD
        public void insertUtilizador(string NomeUtilizador, string TelefoneUtilizador, string EMail, string PassWord, string MoradaUtilizador, string CodigoPostal, string Localidade)
        {

            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"INSERT INTO Utilizadores (NomeUtilizador, TelefoneUtilizador,  EMail, PassWord,  MoradaUtilizador,  CodigoPostal,  Localidade ) 
VALUES( '" + NomeUtilizador + "','" + TelefoneUtilizador + "','" + EMail + "','" + PassWord + "','" + MoradaUtilizador + "', '" + CodigoPostal + "','" + Localidade + "')";

                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())


                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }

        // Alterar utilizador
        public void AlterarUtilizador(long IdUtilizador, string NomeUtilizador, string TelefoneUtilizador, string EMail, string PassWord, string MoradaUtilizador, string CodigoPostal, string Localidade, string Estado)
        {

            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"UPDATE Utilizadores SET  NomeUtilizador='" + NomeUtilizador + "', TelefoneUtilizador='" + TelefoneUtilizador + "', EMail='" + EMail + "', PassWord='" + PassWord + "', MoradaUtilizador='" + MoradaUtilizador + "', CodigoPostal='" + CodigoPostal + "', Estado='" + Estado + "'    WHERE IdUtilizador=" + IdUtilizador + "";
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }
                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }

        // altera o estado do utilizador Ativo/Inativo na BD
        public void alteraEstadoUtilizador(long IdUtilizador, string Estado)
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);
            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())
            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"UPDATE Utilizadores SET Estado='" + Estado + "' WHERE IdUtilizador=" + IdUtilizador + "";

                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }

        //Pesquisa utilizador por Id de utilizador
        public string psquisaUtilizadorPorId(long IdUtilizador)
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);
            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())
            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"SELECT IdUtilizador, NomeUtilizador, Estado FROM Utilizadores WHERE IdUtilizador=" + IdUtilizador + "";

                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            long id = reader.GetInt64(0);
                            string a = id.ToString();
                            string NomeUtilizador = reader.GetString(1);
                            string estado = reader.GetString(2);
                            if (estado=="Inativo"){
                                return "Inativo";
                            }else {
                                return "ID: " + a +  "   NOME: " + reader.GetString(1);
                            }
                            

                        }
                    }
                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
            return "O utilizador não existe";
        }

        // inserir emprestimo na BD
        public void insertEmprestimo(long IdUtilizador, long IdBiblioteca, long IdLivro, string DataInicio, string DataFim, string DataEntrega)
        {

            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"INSERT INTO Emprestimos (IdUtilizador,  IdBiblioteca, IdLivro,  DataInicio,  DataFim, DataEntrega) 
VALUES( " + IdUtilizador + "," + IdBiblioteca + "," + IdLivro + ",'" + DataInicio + "','" + DataFim + "', '" + DataEntrega + "')";

                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())


                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }


        public void devolucaoLivro(long IdLivro, string DataEntrega)
        {

            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);
            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);
            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())
            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {
                    //Comando  a ser executado
                    cmd.CommandText = @"UPDATE Emprestimos SET DataEntrega='" + DataEntrega + "' WHERE IdLivro=" + IdLivro + " AND DataEntrega='"+"'";

                    // Executar comando e proceder à leitura dos dados

                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
               


                        // Iniciar a limpeza da conexão e "pipe"
                        cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }

        //Carregar a lista de Emprestimos conforme o criterio escolhido (variavel tipo : false=Todos  true=sem data entrega)
        public void carregarEmprestimos(long IdBiblioteca , Boolean tipo)
        {
            // Localizar  Ficheiro
            var dbFile = @"..\..\Biblioteca.db";
            // Gerir informação  de ficheiro localizado
            if (File.Exists(dbFile)) Console.Out.WriteLine("File found!" + dbFile);
            else Console.Out.WriteLine("File NOT found!" + dbFile);


            // Parametros  para aceder à base de dados
            var connString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", dbFile);

            // Utilizar factory para gerir os sub-sistemas de queries
            using (var factory = new System.Data.SQLite.SQLiteFactory())

            // Criar ligação fisica em forma de uma conexão passível de ser reutilizada.
            using (System.Data.Common.DbConnection dbConn = factory.CreateConnection())
            {
                dbConn.ConnectionString = connString;
                // Abrir o "pipe" para a base de dados
                dbConn.Open();
                using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                {

                    if (tipo == false)
                    {
                        //Comando  a ser executado
                        cmd.CommandText = @"SELECT IdEmprestimo, IdUtilizador, IdBiblioteca, IdLivro, DataInicio, DataFim, DataEntrega FROM  Emprestimos";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT IdEmprestimo, IdUtilizador, IdBiblioteca, IdLivro, DataInicio, DataFim, DataEntrega FROM  Emprestimos  WHERE IdBiblioteca=" + IdBiblioteca + " AND DataEntrega='"+"'";
                    }                   
                    // Executar comando e proceder à leitura dos dados
                    using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long IdEmprestimo = reader.GetInt64(0);
                            long IdUtilizador = reader.GetInt64(1);
                            long IdB = reader.GetInt64(2);
                            long IdLivro = reader.GetInt64(3);
                            string DataInicio = reader.GetString(4);
                            string DataFim = reader.GetString(5);
                            string DataEntrega = reader.GetString(6);
                            //chama o metodo para adicionar os emprestimos a lista 

                            listagemEmprestimos.adicionarEmprestimosLista(IdEmprestimo, IdUtilizador, IdB, IdLivro, DataInicio, DataFim, DataEntrega);                          
                        }
                    }
                    // Iniciar a limpeza da conexão e "pipe"
                    cmd.Dispose();
                }

                if (dbConn.State != System.Data.ConnectionState.Closed)
                    dbConn.Close();
                dbConn.Dispose();
                factory.Dispose();
            }
        }
    }
}
