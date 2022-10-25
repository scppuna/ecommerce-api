using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SprintCinco.Data.Dtos.ProdutoDtos;
using SprintCinco.Models;
using SprintCinco.Dao;
using SprintCinco.Service;
using System.Collections.Generic;

namespace SprintCinco.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoService _produtoService;
        private ProdutoDao _produtoDao;

        public ProdutoController(ProdutoService produtoService, ProdutoDao produtoDao)
        {
            _produtoService = produtoService;
            _produtoDao = produtoDao;
        }

        [HttpPost]
        public IActionResult CriarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            ReadProdutoDto readDto = _produtoService.AdicionaProduto(produtoDto);
            return CreatedAtAction(nameof(BuscarProdutoPorId), new { Id = readDto.Id }, readDto);

        }

        [HttpGet("pesquisar/{id}")]
        public IActionResult BuscarProdutoPorId(int id)
        {
            ReadProdutoDto readDto = _produtoService.BuscarProdutoPorId(id);
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult EditarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
            Result resultado = _produtoService.EditarProduto(id, produtoDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirProduto(int id)
        {
            Result resultado = _produtoService.ExcluirProduto(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpGet("PesquisarComFiltro")]
        public IReadOnlyList<Produto> PesquisarFiltro([FromQuery]string nome, [FromQuery] string centro, [FromQuery] bool? status, [FromQuery] double? peso, [FromQuery] double? altura, [FromQuery] double? largura,
            [FromQuery] double? comprimento, [FromQuery] double? valor, [FromQuery] int? estoque, [FromQuery] string ordem, [FromQuery] int itensPagina, [FromQuery] int paginaAtual)
        {
            return _produtoService.PesquisarFiltro(nome, centro, status, peso, altura, largura,
                comprimento, valor, estoque, ordem, itensPagina, paginaAtual);
        }

    }
}
