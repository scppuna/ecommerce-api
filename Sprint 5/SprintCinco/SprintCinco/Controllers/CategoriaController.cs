using FluentResults;
using Microsoft.AspNetCore.Mvc;
using IEcommerceAPI.Repository;
using IEcommerceAPI.Data.Dtos.CategoriaDtos;
using IEcommerceAPI.Models;
using IEcommerceAPI.Services;

namespace IEcommerceAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class CategoriaController : ControllerBase
    {
        private CategoriaService _service;
        public CategoriaController(CategoriaService categoriaService)
        {
            _service = categoriaService;
        }

        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {
            ReadCategoriaDto lerDto = _service.CadastrarCategoria(categoriaDto);
            return CreatedAtAction(nameof(PesquisaCategoriaPorNome), new { nome = categoriaDto.Nome }, categoriaDto);
        }


        [HttpGet("{id}")]
        public IActionResult PesquisaCategoriaPorId(int id)
        {
            var categoriaPesquisado = _service.BuscarCategoriaPorId(id);
            return Ok(categoriaPesquisado);
        }

        [HttpGet("PesquisarPorNome")]
        public IActionResult PesquisaCategoriaPorNome(string nome)
        {
            var categoriaPesquisada = _service.BuscarCategoria(nome);
            return Ok(categoriaPesquisada);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            Result resultado = _service.ExcluirCategoria(id);
            if (resultado.IsFailed) return NotFound();
            return Accepted();
        }

        [HttpPut("EditarStatus/{id}")]
        public IActionResult EditarStatusDeCategoria(int id)
        {
            Result result = _service.EditarStatus(id);
            if (result.IsFailed) return NotFound();
            return Accepted();
        }

        [HttpPut("EditarCategoria/{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
        {
            Result result = _service.EditarCategoria(id, categoriaDto);
            if (result.IsFailed) return NotFound();
            return Accepted();
        }
    }

}