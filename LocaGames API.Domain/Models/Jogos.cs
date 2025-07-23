using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaGames_API.Domain.Models
{
    public class Jogos
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Disponivel { get; set; }
        public CategoriaJogos Categoria { get; set; }
        public string Responsavel { get; set; }
        public DateTime? DataEntrega { get; set; }

        public bool IsEmAtraso { get; set; }

    }
}
