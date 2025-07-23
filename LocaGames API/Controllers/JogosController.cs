using Azure.Core;
using LocaGames_API.Domain.Dtos;
using LocaGames_API.Domain.Dtos.Request;
using LocaGames_API.Domain.Service;
using Microsoft.AspNetCore.Mvc;
namespace LocaGames_API.Controllers
{

    [ApiController]
    [Route("jogo")]
    public class JogosController : Controller
    {
        private readonly IJogosService _JogosService;

        public JogosController(IJogosService jogosService)
        {
            _JogosService = jogosService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var jogosResponse = await _JogosService.ObterTodos();

            return Ok(jogosResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterDetalhadoPorId([FromRoute] long id)
        {
            try
            {
                var jogoDetalhadoResponse = await _JogosService.ObterDetalhadoPorId(id);

                return Ok(jogoDetalhadoResponse);
            }
            catch (Exception ex)
            {
                var erroResponse = new ErroResponse
                {
                    Mensagem = ex.Message
                };

                return NotFound(erroResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarRequest cadastrarRequest)
        {
            try
            {
                var cadastrarResponse = await _JogosService.Cadastrar(cadastrarRequest);

                return Ok(cadastrarResponse);
            }
            catch (Exception ex)
            {
                var erroResponse = new ErroResponse
                {
                    Mensagem = ex.Message
                };

                return BadRequest(erroResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPorId([FromRoute] long id)
        {
            try
            {
                await _JogosService.DeletarPorId(id);

                return Ok();
            }
            catch (Exception ex)
            {
                var erroResponse = new ErroResponse
                {
                    Mensagem = ex.Message
                };

                return BadRequest(erroResponse);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarPorID([FromRoute] long id, [FromBody] AtualizarRequest adicionarRequest)
        {
            try
            {
                await _JogosService.AtualizarPorId(id, adicionarRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                var erroResponse = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(erroResponse);
            }
        }
        [HttpPut("{id}/alugar")]
        public async Task<IActionResult> Alugar( long id, [FromBody] AlugarJogoRequest request)
        {

            try
            {
                await _JogosService.AlugarJogo(id, request);
                return Ok(new { message = "Jogo alugado com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
           





        }

        
        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> Devolver(long id)
        {
            try
            {
                await _JogosService.DevolverJogo(id);
                return Ok(new { message = "Jogo devolvido com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }




        }

    }
}
