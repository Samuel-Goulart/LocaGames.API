using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaGames_API.Domain.Models;

namespace LocaGames_API.Domain.Dtos.Request
{
    public class AtualizarRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
    }
}
