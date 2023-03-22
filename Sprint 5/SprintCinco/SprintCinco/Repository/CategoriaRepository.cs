using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentResults;
using IEcommerceAPI.Data;
using IEcommerceAPI.Data.Dtos.CategoriaDtos;
using IEcommerceAPI.Models;

namespace IEcommerceAPI.Repository
{
    public class CategoriaRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CategoriaRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Result CadastrarCategorias(CreateCategoriaDto categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return Result.Ok();
        }


        public List<ReadCategoriaDto> BuscarTodasCategorias()
        {
            var categoria = _context.Categorias.ToList();
            List<ReadCategoriaDto> categoriaDtos = _mapper.Map<List<ReadCategoriaDto>>(categoria).ToList();
            return categoriaDtos;
        }

        public List<ReadCategoriaDto> BuscarCategoriaPorId(int? id)
        {
            List<Categoria> categoria = _context.Categorias.Where(categoria => categoria.Id == id).ToList();
            if (categoria != null)
            {
                List<ReadCategoriaDto> categoriaDto = _mapper.Map<List<ReadCategoriaDto>>(categoria);
                return categoriaDto.ToList();
            }
            return null;
        }

        public Result EditarCategoria(int id, UpdateCategoriaDto updateCategoriaDto)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return Result.Fail("Não foi possível localizar a categoria.");
            }
            _mapper.Map(updateCategoriaDto, categoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditaStatus(int id, UpdateCategoriaDto updateCategoriaDto)
        {
            var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            _mapper.Map(updateCategoriaDto, categoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result ExcluirCategoria(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return Result.Fail("Não foi possível encontrar uma categoria com o id informado");
            }
            if (categoria.Status == true)
            {
                return Result.Fail("Não é possível excluir categoria ativa!");
            }
            _context.Remove(categoria);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
