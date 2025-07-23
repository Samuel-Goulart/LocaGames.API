using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaGames_API.Domain.Dtos.Request;
using LocaGames_API.Domain.Dtos.Response;
using LocaGames_API.Domain.Models;

namespace LocaGames_API.Domain.Repository
{
    public interface IJogosRepository
    {
        Task<Jogos> ObterDetalhadoPorId(long id);
        
        Task DeletarPorId(long id);
        Task AtualizarPorId(Jogos jogo);

        Task<IEnumerable<Jogos>> ObterTodos();
     
        Task<long> Cadastrar(Jogos jogo);
        Task DevolverJogo( Jogos jogo);
        Task AlugarJogo(Jogos jogo);
    }
}
