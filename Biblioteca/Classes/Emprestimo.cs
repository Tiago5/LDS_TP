using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Emprestimo
    {
        public long IdEmprestimo { get; set; }
        public long IdUtilizador { get; set; }
        public long IdBiblioteca { get; set; }
        public long IdLivro { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string DataEntrega { get; set; }
    }
}
