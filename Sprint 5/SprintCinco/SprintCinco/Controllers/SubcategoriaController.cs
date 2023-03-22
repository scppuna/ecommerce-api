using FluentResults;
using Microsoft.AspNetCore.Mvc;
using IEcommerceAPI.Repository;
using IEcommerceAPI.Data.Dtos.CategoriaDtos;
using IEcommerceAPI.Models;
using IEcommerceAPI.Services;
using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
using EcommerceAPI.Services;

namespace IEcommerceAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class SubcategoriaController : ControllerBase
    {
        private SubcategoriaService _subcategoriaService;
        public SubcategoriaController(SubcategoriaService subcategoriaService)
        {
            _subcategoriaService = subcategoriaService;
        }

        [HttpPost]
        public IActionResult CadastrarSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDto)
        {
            var readSubcategoriaDto = _subcategoriaService.CadastrarSubcategoria(subcategoriaDto);
            return CreatedAtAction(nameof(PesquisarSubcategoriaPorNome), new { nome = subcategoriaDto.Nome }, subcategoriaDto);
        }

        [HttpGet]
        public IActionResult PesquisarSubcategoriaComFiltro (FiltroSubcategoriaDto filtroSubcategoriaDto)
        {
            var pesquisa = _subcategoriaService.PesquisarSubcategoriaPorFiltros(filtroSubcategoriaDto);
            if (pesquisa == null || pesquisa.Count == 0)
            {
                return NotFound("Não foi possível encontrar a subcategoria desejada!");
            }
            return Ok(pesquisa);
        }

        [HttpGet("{nome}")]
        public IActionResult PesquisarSubcategoriaPorNome(string nome)
        {
            var subcategoria = _subcategoriaService.RecuperarSubcategorias();
            return Ok(subcategoria);
        }

        [HttpGet("{id}")]
        public IActionResult PesquisaSubcategoriaPorId(int id)
        {
            var subcategoriaPesquisado = _subcategoriaService.PesquisarSubcategoriaPorId(id);
            return Ok(subcategoriaPesquisado);
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarSubcategoria(int id)
        {
            Result resultado = _subcategoriaService.DeletarSubcategoria(id);
            if (resultado.IsFailed) return NotFound();
            return Accepted();
        }

        [HttpPut("EditarStatus/{id}")]
        public IActionResult EditarStatusDeSubcategoria(int id)
        {
            Result result = _subcategoriaService.EditarStatus(id);
            if (result.IsFailed) return NotFound();
            return Accepted();
        }

        [HttpPut("EditarSubcategoria/{id}")]
        public IActionResult EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto updateSubcategoriaDto)
        {
            Result result = _subcategoriaService.EditarSubcategoria(id, updateSubcategoriaDto);
            if (result.IsFailed) return NotFound();
            return Accepted();
        }
    }
}









//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using IEcommerceAPI.Data;
//using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
//using IEcommerceAPI.Models;
//using IEcommerceAPI.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IEcommerceAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]

//    public class SubcategoriaController : ControllerBase
//    {
//        private AppDbContext _context;
//        private IMapper _mapper;
//        private SubcategoriaRepository _repository;


//        public SubcategoriaController(AppDbContext context, IMapper mapper, SubcategoriaRepository repository)
//        {
//            _context = context;
//            _mapper = mapper;
//            _repository = repository;
//        }

//        [HttpPost]

//        public IActionResult AdicionaSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDto)
//        {
//            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == subcategoriaDto.CategoriaID);
//            if (categoria == null)
//            {
//                throw new ArgumentNullException(paramName: nameof(categoria), message: "Categoria nula. Favor digitar");
//            }

//            if (categoria.Status == true)
//            {
//                Subcategoria subcategoria = _mapper.Map<Subcategoria>(subcategoriaDto);
//                _context.Subcategorias.Add(subcategoria);
//                _context.SaveChanges();
//                return CreatedAtAction(nameof(Pesquisar), new { Nome = subcategoria.Nome, subcategoria });
//            }

//            return BadRequest("Categoria inexistente.");
//        }

//        [HttpGet("pesquisa")]
//        public IActionResult Pesquisar([FromQuery] string nome, [FromQuery] string ordem, [FromQuery] bool? status,
//            [FromQuery] int paginaAtual, [FromQuery] int itens)
//        {
//            IQueryable<Subcategoria> list = null;

//            if (nome != null && nome.Length < 3)
//            {
//                return BadRequest("Limite de Caracteres atingido. Favor digitar entre 3 à 128");
//            }

//            if (nome != null && nome.Length >= 3)
//            {
//                list = _context.Subcategorias.Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

//            }
//            else
//            {
//                list = _context.Subcategorias;
//            }

//            if (status != null)
//            {
//                list = list.Where(c => c.Status == status);
//            }


//            if (ordem != null)
//            {
//                if (ordem == "desc")
//                {
//                    list = list.OrderByDescending(c => c.Nome);
//                }
//                else if (ordem == "asc")
//                {
//                    list = list.OrderBy(c => c.Nome);
//                }
//            }

//            if (paginaAtual > 0 && itens > 0)
//            {
//                list = list.Skip((paginaAtual - 1) * itens).Take(itens);
//            }
//            return Ok(list);

//        }

//        [HttpGet("pesquisar/{id}")]
//        public IActionResult BuscarSubcategoriaPorId(int id)
//        {
//            var subcategoria = _repository.BuscarSubcategoriaPorId(id);
//            if (subcategoria != null)
//            {
//                ReadSubcategoriaDto subcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(subcategoria);
//                return Ok(subcategoriaDto);
//            }
//            return NotFound();
//        }

//        [HttpPut("{id}")]
//        public IActionResult EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto subcategoriaDto)
//        {
//            var subcategoria = _repository.BuscarSubcategoriaPorId(id);
//            if (subcategoria != null)
//            {
//                _mapper.Map(subcategoriaDto, subcategoria);
//                subcategoria.DataEdicao = DateTime.Now;
//                _context.SaveChanges();
//                return NoContent();
//            }
//            return NotFound();
//        }

//        [HttpPut("editarStatus/{id}")]
//        public IActionResult AtualizaStatus(int id)
//        {
//            var attStatus = _repository.BuscarSubcategoriaPorId(id);
//            if (attStatus != null)
//            {
//                attStatus.DataEdicao = DateTime.Now;
//                if (attStatus.Status == true)
//                {
//                    attStatus.Status = false;
//                }
//                else
//                {
//                    attStatus.Status = true;
//                }
//                _context.SaveChanges();
//                return NoContent();

//            }
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeletaSubcategoria(int id)
//        {
//            var subcategoria = _repository.BuscarSubcategoriaPorId(id);
//            if (subcategoria == null)
//            {
//                return NotFound();
//            }
//            _context.Remove(subcategoria);
//            _context.SaveChanges();
//            return NoContent();
//        }

//    }
//}
