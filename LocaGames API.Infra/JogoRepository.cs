using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LocaGames_API.Domain.Dtos.Request;
using LocaGames_API.Domain.Models;
using LocaGames_API.Domain.Repository;
using LocaGames_API.Infra.DatabaseConfiguration;
using static LocaGames_API.Infra.JogoRepository;

namespace LocaGames_API.Infra
{
    
    
        public class JogoRepository : IJogosRepository
        {
            private readonly IDbConnectionFactory _connectionFactory;

            public JogoRepository(IDbConnectionFactory connectionFactory)
            {
                _connectionFactory = connectionFactory;
            }

        public async Task AlugarJogo(Jogos jogo)
        {
            var sql = @"
            UPDATE Jogos
            SET
                 Disponivel = @Disponivel,
                Responsavel = @Responsavel,
                DataEntrega = @DataEntrega
                
            WHERE
                Id = @Id
        ";

            var connection = _connectionFactory.CreateConnection();
            await connection.QueryFirstOrDefaultAsync(sql, jogo);
        
        }

        public async Task AtualizarPorId(Jogos jogo)
            {
                var sql = @"
            UPDATE Jogos
            SET
                Titulo = @Titulo,
                Descricao = @Descricao,
                CategoriaId = @Categoria
               
            WHERE
                Id = @Id
        ";

                var connection = _connectionFactory.CreateConnection();
                await connection.QueryFirstOrDefaultAsync(sql, jogo);
            }

            public async Task<long> Cadastrar(Jogos jogo)
            {
                var sql = @"
            INSERT INTO Jogos
                (Titulo, Descricao, Disponivel, CategoriaId, Responsavel, DataEntrega, IsEmAtraso)
            OUTPUT INSERTED.Id
            VALUES
                (@Titulo, @Descricao, @Disponivel, @Categoria, @Responsavel, @DataEntrega, @IsEmAtraso)
        ";

                var connection = _connectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<long>(sql, jogo);
            }

            public async Task DeletarPorId(long id)
            {
                var sql = @"
            DELETE FROM Jogos
            WHERE Id = @Id
        ";

                var connection = _connectionFactory.CreateConnection();
                await connection.QueryFirstOrDefaultAsync(sql, new { Id = id });
            }

        public async Task DevolverJogo(Jogos jogo)
        {
            var sql = @"
            UPDATE Jogos
            SET
                Disponivel = @Disponivel,
                Responsavel = @Responsavel,
                DataEntrega = @DataEntrega
            WHERE
                Id = @Id
        ";

            var connection = _connectionFactory.CreateConnection();
            await connection.QueryFirstOrDefaultAsync(sql, jogo);
        }

        public async Task<Jogos> ObterDetalhadoPorId(long id)
            {
                var sql = @"
            SELECT 
                j.Id,
                j.Titulo,
                j.Descricao,
                j.Disponivel,
                c.id AS Categoria,
                j.Responsavel,
                j.DataEntrega,
                j.IsEmAtraso
            FROM Jogos j
            INNER JOIN CategoriaJogos c ON c.Id = j.CategoriaId
            WHERE j.Id = @Id
        ";

                 var connection = _connectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<Jogos>(sql, new { Id = id });
            }

            public async Task<IEnumerable<Jogos>> ObterTodos()
            {
                var sql = @"
            SELECT 
                j.Id,
                j.Titulo,
                j.Descricao,
                j.Disponivel,
                j.CategoriaId,
                c.Nome AS CategoriaNome,
                j.Responsavel,
                j.DataEntrega,
                j.IsEmAtraso
            FROM Jogos j
            INNER JOIN CategoriaJogos c ON c.Id = j.CategoriaId
        ";

                 var connection = _connectionFactory.CreateConnection();
                return await connection.QueryAsync<Jogos>(sql);
            }
        }

    }

