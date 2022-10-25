using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SprintCinco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SprintCinco.Data;
using AutoMapper;
using SprintCinco.Data.Dtos.CategoriaDtos;
using SprintCinco.Data.Dtos.SubcategoriaDtos;
using SprintCinco.Dao;

namespace SprintCinco.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private CategoriaDao _dao;
        public CategoriaController(AppDbContext context, IMapper mapper, CategoriaDao dao)
        {
            _context = context;
            _mapper = mapper;
            _dao = dao;
        }

        [HttpPost]
        public IActionResult CriarCategoria([FromBody] CreateSubcategoriaDto categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PesquisarCategoria), new { Nome = categoria.Nome }, categoria);
        }

        [HttpGet("pesquisar/{id}")]
        public IActionResult BuscarCategoriaPorId(int id)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);
            if (categoria != null)
            {
                ReadCategoriaDto categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
                return Ok(categoriaDto);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult PesquisarCategoria([FromQuery] string nome, [FromQuery] string ordem, [FromQuery] bool? status)
        {
            IQueryable<Categoria> list = null;

            if (nome != null && nome.Length < 3)
            {
                return BadRequest("Limite de Caracteres atingido. Favor digitar entre 3 à 128");
            }

            if (nome != null && nome.Length >= 3)
            {
                list = _context.Categorias.Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            }
            else
            {
                list = _context.Categorias;
            }

            if (status != null)
            {
                list = list.Where(c => c.Status == status);
            }


            if (ordem != null)
            {
                if (ordem == "desc")
                {
                    list = list.OrderByDescending(c => c.Nome);
                }
                else if (ordem == "asc")
                {
                    list = list.OrderBy(c => c.Nome);
                }
            }
            return Ok(list);

        }

        [HttpPut("{id}")]
        public IActionResult EditarCategoriaPorId(int id, [FromBody] UpdateCategoriaDto categoriaDto)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);
            if (categoria != null)
            {
                _mapper.Map(categoriaDto, categoria);
                categoria.DataEdicao = DateTime.Now.ToString("dd-MM-yyyy - HH:mm:ss");
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("editarStatus/{id}")]
        public IActionResult EditarStatusPorId(int id)
        {
            var categoriaStatus = _dao.BuscarCategoriaPorId(id);
            if (categoriaStatus != null)
            {
                categoriaStatus.DataEdicao = DateTime.Now.ToString("dd-MM-yyyy - HH:mm:ss");
                if (categoriaStatus.Status == true)
                {
                    categoriaStatus.Status = false;
                }
                else
                {
                    categoriaStatus.Status = true;
                }
                _context.SaveChanges();
                return NoContent();

            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCategoria(int id)
        {
            var categoria = _dao.BuscarCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _context.Remove(categoria);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
