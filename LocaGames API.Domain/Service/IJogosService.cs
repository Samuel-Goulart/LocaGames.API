using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaGames_API.Domain.Dtos.Request;
using LocaGames_API.Domain.Dtos.Response;
using LocaGames_API.Domain.Models;

namespace LocaGames_API.Domain.Service
{
    public interface IJogosService
    {
        Task<ObterDetalhadoPorIdResponse> ObterDetalhadoPorId(long id);
        Task<CadastrarResponseJogo> Cadastrar(CadastrarRequest cadastrarRequest);
        Task DeletarPorId(long id);
        Task AtualizarPorId(long id, AtualizarRequest atualizarRequest);
        Task AlugarJogo(long id, AlugarJogoRequest request);
        Task DevolverJogo(long id);
        
        Task<IEnumerable<ObterTodosResponse>> ObterTodos();
        
    }
}
