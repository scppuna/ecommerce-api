
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dto.Usuario;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroUsuarioController : ControllerBase
    {
        private CadastroUsuarioService _cadastroService;

        public CadastroUsuarioController(CadastroUsuarioService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = await _cadastroService.CadastrarUsuario(createDto);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> PesquisarUsuario([FromQuery]string username, [FromQuery] string cpf, [FromQuery] string email, [FromQuery] bool? status)
        {
            var resposta = await _cadastroService.PesquisarUsuarioComFiltro(username, cpf, email, status);
            return Ok(resposta);
        }

        [HttpPut]
        public async Task <IActionResult> EditarUsuario(int id, [FromBody]UpdateUsuarioDto updateUsuario)
        {
            Result result = await _cadastroService.EditarUsuario(id, updateUsuario);
            if (result.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
}
