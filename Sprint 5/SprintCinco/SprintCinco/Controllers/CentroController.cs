using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SprintCinco.Dao;
using SprintCinco.Data.Dtos.CentroDistribuicaoDtos;
using SprintCinco.Models;
using SprintCinco.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SprintCinco.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class CentroDeDistribuicaoController : Controller
    {
        private CentroService _service;

        public CentroDeDistribuicaoController(CentroService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CreateCDDto centroDto)
        {
            var readCDDto = await _service.AdicionaCentro(centroDto);
            return CreatedAtAction(nameof(PesquisaCentroPorId), new { centroDto.Id }, centroDto);
        }

        [HttpGet("{id}")]
        public IActionResult PesquisaCentroPorId(int id)
        {
            var centroPesquisado = _service.BuscarCDPorId(id);
            return Ok(centroPesquisado);
        }

        //[HttpGet("{nome}")]
        //public IActionResult PesquisarCentroPorNome(string nome)
        //{
        //    var pesquisaNome = _service.BuscarCDPorNome(nome);
        //    return Ok(pesquisaNome);
        //}

        [HttpGet("PesquisarComFiltro")]
        public IReadOnlyList<CentroDistribuicao> PesquisarCentroDistribuicaoPorFiltro([FromQuery] string nome, [FromQuery] string logradouro, [FromQuery] string bairro,
       [FromQuery] string localidade, [FromQuery] string uf, [FromQuery] string cep, [FromQuery] bool? status, [FromQuery] DateTime? dataCriacao, [FromQuery] DateTime? dataEdicao, [FromQuery] int? numero, [FromQuery] string ordem, [FromQuery] int itensPagina, [FromQuery] int paginaAtual)
        {
            return _service.PesquisarCentroDistribuicaoComFiltro(nome, logradouro, bairro, localidade, uf, cep,
                status, dataCriacao, dataEdicao, numero, ordem, itensPagina, paginaAtual);
        }

        [HttpPut("{id}")]
        public IActionResult EditarCentroDeDistribuicao(int id, [FromBody] UpdateCDDto cDDto)
        {
            Result resultado = _service.EditarCD(id, cDDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpPut("EditarStatus/{id}")]
        public IActionResult EditarStatusCentroDistribuicao(int id, [FromBody] UpdateCDDto cDDto)
        {
            Result result = _service.EditarStatus(id, cDDto);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCentroDeDistribuicao(int id)
        {
            Result result = _service.ExcluirCentroDeDistribuicao(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }


    }

}
