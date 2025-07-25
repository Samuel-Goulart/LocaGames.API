﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaGames_API.Domain.Models;

namespace LocaGames_API.Domain.Dtos.Response
{
    public class ObterDetalhadoPorIdResponse
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Disponivel { get; set; }
        public string Categoria { get; set; }
        public string Responsavel { get; set; }
        public DateTime? DataEntrega { get; set; }

        public bool IsEmAtraso { get; set; }
    }
}
