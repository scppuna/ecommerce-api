using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SprintCinco.Data;
using SprintCinco.Data.Dtos.SubcategoriaDtos;
using SprintCinco.Models;
using SprintCinco.Dao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SprintCinco.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SubcategoriaController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private SubcategoriaDao _dao;


        public SubcategoriaController(AppDbContext context, IMapper mapper, SubcategoriaDao dao)
        {
            _context = context;
            _mapper = mapper;
            _dao = dao;
        }

        [HttpPost]

        public IActionResult AdicionaSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDto)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == subcategoriaDto.CategoriaID);
            if (categoria == null)
            {
                throw new ArgumentNullException(paramName: nameof(categoria), message: "Categoria nula. Favor digitar");
            }

            if (categoria.Status == true)
            {
                Subcategoria subcategoria = _mapper.Map<Subcategoria>(subcategoriaDto);
                _context.Subcategorias.Add(subcategoria);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Pesquisar), new { Nome = subcategoria.Nome, subcategoria });
            }

            return BadRequest("Categoria inexistente.");
        }

        [HttpGet("pesquisa")]
        public IActionResult Pesquisar([FromQuery] string nome, [FromQuery] string ordem, [FromQuery] bool? status,
            [FromQuery] int paginaAtual, [FromQuery] int itens)
        {
            IQueryable<Subcategoria> list = null;

            if (nome != null && nome.Length < 3)
            {
                return BadRequest("Limite de Caracteres atingido. Favor digitar entre 3 à 128");
            }

            if (nome != null && nome.Length >= 3)
            {
                list = _context.Subcategorias.Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            }
            else
            {
                list = _context.Subcategorias;
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

            if (paginaAtual > 0 && itens > 0)
            {
                list = list.Skip((paginaAtual - 1) * itens).Take(itens);
            }
            return Ok(list);

        }

        [HttpGet("pesquisar/{id}")]
        public IActionResult BuscarSubcategoriaPorId(int id)
        {
            var subcategoria = _dao.BuscarSubcategoriaPorId(id);
            if (subcategoria != null)
            {
                ReadSubcategoriaDto subcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(subcategoria);
                return Ok(subcategoriaDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult EditarSubcategoria(int id, [FromBody] UpdateSubcategoriaDto subcategoriaDto)
        {
            var subcategoria = _dao.BuscarSubcategoriaPorId(id);
            if (subcategoria != null)
            {
                _mapper.Map(subcategoriaDto, subcategoria);
                subcategoria.DataEdicao = DateTime.Now.ToString("dd-MM-yyyy - HH:mm:ss");
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("editarStatus/{id}")]
        public IActionResult AtualizaStatus(int id)
        {
            var attStatus = _dao.BuscarSubcategoriaPorId(id);
            if (attStatus != null)
            {
                attStatus.DataEdicao = DateTime.Now.ToString("dd-MM-yyyy - HH:mm:ss");
                if (attStatus.Status == true)
                {
                    attStatus.Status = false;
                }
                else
                {
                    attStatus.Status = true;
                }
                _context.SaveChanges();
                return NoContent();

            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaSubcategoria(int id)
        {
            var subcategoria = _dao.BuscarSubcategoriaPorId(id);
            if (subcategoria == null)
            {
                return NotFound();
            }
            _context.Remove(subcategoria);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
