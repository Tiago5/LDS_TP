using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Livro
    {
        public long IdLivro { get; set; }
        public long IdBiblioteca { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Edicao { get; set; }
        public string Descricao { get; set; }
        public string TipoEmprestimo { get; set; }
        public string Estado { get; set; }

    }
}
