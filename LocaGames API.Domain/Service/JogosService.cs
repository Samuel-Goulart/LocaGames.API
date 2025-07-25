﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaGames_API.Domain.Dtos.Request;
using LocaGames_API.Domain.Dtos.Response;
using LocaGames_API.Domain.Models;
using LocaGames_API.Domain.Repository;

namespace LocaGames_API.Domain.Service
{
    public class JogosService : IJogosService
    {
        private readonly IJogosRepository _JogosRepository;

        public JogosService(IJogosRepository jogosRepository)
        {
            _JogosRepository = jogosRepository;
        }
        public async Task AtualizarPorId(long id, AtualizarRequest atualizarRequest)
        {
            bool CategoriaValida = Enum.TryParse(atualizarRequest.Categoria, ignoreCase: true, out CategoriaJogos categoriaJogos);

            if (!CategoriaValida)
            {
                throw new Exception($"O tipo de combustivel {atualizarRequest.Categoria} é inválido");
            }


            var jogo = await _JogosRepository.ObterDetalhadoPorId(id);

            if (jogo == null)
            {
                throw new Exception($"Nao foi possivel encontra o a carro com {id}");
            }

            jogo.Titulo = atualizarRequest.Titulo;
            jogo.Descricao = atualizarRequest.Descricao;
            jogo.Categoria = categoriaJogos;

            await _JogosRepository.AtualizarPorId(jogo);
        }

        public async Task<CadastrarResponseJogo> Cadastrar(CadastrarRequest cadastrarRequest)
        {
            bool CategoriaValida = Enum.TryParse(cadastrarRequest.Categoria, ignoreCase: true, out CategoriaJogos categoriaJogos);

            if (!CategoriaValida)
            {
                throw new Exception($"O tipo de combustivel {cadastrarRequest.Categoria} é inválido");
            }

            var jogo = new Jogos
            {
                Titulo = cadastrarRequest.Titulo,
                Descricao = cadastrarRequest.Descricao,
                Disponivel = cadastrarRequest.Disponivel,
                Responsavel = cadastrarRequest.Responsavel,
                DataEntrega = cadastrarRequest.DataEntrega,
                Categoria = categoriaJogos
               
            };

            long idResponse = await _JogosRepository.Cadastrar(jogo);

            var response = new CadastrarResponseJogo
            {
                Id = idResponse,
            };

            return response;
        }

        public async Task DeletarPorId(long id)
        {
            var jogo = await _JogosRepository.ObterDetalhadoPorId(id);

            if (jogo == null)
            {
                throw new Exception($"Não foi encontrado um carro com o id {id}");
            }

            await _JogosRepository.DeletarPorId(id);
        }


        public async Task<IEnumerable<ObterTodosResponse>> ObterTodos()
        {

            var carros = await _JogosRepository.ObterTodos();

            var carrosResponse = carros
                .Select(x => new ObterTodosResponse
                {
                    Titulo = x.Titulo,
                    Id = x.Id
                });

            return carrosResponse;
        }
        public async Task<ObterDetalhadoPorIdResponse> ObterDetalhadoPorId(long id)
        {
            var jogo = await _JogosRepository.ObterDetalhadoPorId(id);

            if (jogo == null)
                throw new KeyNotFoundException($"Jogo com ID {id} não encontrado.");

            return new ObterDetalhadoPorIdResponse
            {
                Id = jogo.Id,
                Titulo = jogo.Titulo,
                Descricao = jogo.Descricao,
                Disponivel = jogo.Disponivel,
                Categoria = jogo.Categoria.ToString(),
                Responsavel = jogo.Responsavel,
                DataEntrega = jogo.DataEntrega,
                IsEmAtraso = DateTime.Now > jogo.DataEntrega
            };
        }
        public async Task AlugarJogo(long id, AlugarJogoRequest request)
        {
            var jogo = await _JogosRepository.ObterDetalhadoPorId(id);

            if (jogo == null)
                throw new Exception("Jogo não encontrado.");

            if (!jogo.Disponivel)
                throw new InvalidOperationException("Jogo já está alugado.");

            jogo.Disponivel = false;
            jogo.Responsavel = request.Responsavel;
            jogo.DataEntrega = CalcularDataEntrega(jogo.Categoria);
            

            await _JogosRepository.AlugarJogo(jogo);
        }

        public async Task DevolverJogo(long id)
        {
           

            var jogo = await _JogosRepository.ObterDetalhadoPorId(id);

            if (jogo == null)
                throw new Exception("Jogo não encontrado.");

            if (jogo.Disponivel)
                throw new InvalidOperationException("Jogo já está disponível para aluguel.");

            jogo.Disponivel = true;
            jogo.Responsavel = null;
            jogo.DataEntrega = null;
            jogo.IsEmAtraso = false;

            await _JogosRepository.DevolverJogo(jogo);
        }


        private DateTime CalcularDataEntrega(CategoriaJogos categoria)
        {
            var dias = categoria switch
            {
                CategoriaJogos.Bronze => 9,
                CategoriaJogos.Prata => 6,
                CategoriaJogos.Ouro => 3,
                _ => throw new InvalidOperationException("Categoria inválida.")
            };

            return DateTime.Now.AddDays(dias);
        }
    }
}

