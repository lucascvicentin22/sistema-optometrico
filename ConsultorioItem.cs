using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaOptometrico
{
    public class ConsultorioItem
    {
        public int IdConsultorio { get; set; }
        public string NomeConsultorio { get; set; }
        public string EnderecoConsultorio { get; set; }
        public string CidadeConsultorio { get; set; } // Nova propriedade para a cidade
    }


}

