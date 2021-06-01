using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React_Arquivos_Backend.VO.ArquivoVO
{
    public class ArquivoVO_Lista
    {
        public int ArquivoId { get; set; }

        public string CaminhoImg { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int Tamanho { get; set; }

        public string Formato { get; set; }
    }
}
