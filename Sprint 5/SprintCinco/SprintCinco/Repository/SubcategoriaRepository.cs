using AutoMapper;
using FluentResults;
using IEcommerceAPI.Data;
using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
using IEcommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Data.Repository
{
    public class SubcategoriaRepository
    {
        AppDbContext _context;
        IMapper _mapper;

        public SubcategoriaRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result CadastrarSubcategoria(CreateSubcategoriaDto dto)
        {
            var categoria = _context.Categorias.FirstOrDefault(subcat => subcat.Id == dto.CategoriaID);
            if (categoria.Status == false)
            {
                return Result.Fail("Não é possível cadastrar uma subcategoria em uma categoria inativa!");
            }
            Subcategoria subcategoria = _mapper.Map<Subcategoria>(dto);
            _context.Subcategorias.Add(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<ReadSubcategoriaDto> RecuperarSubcategorias()
        {
            var pesquisa = _context.Subcategorias.ToList();
            List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(pesquisa).ToList();
            return subcategoriaDto;
        }

        public List<ReadSubcategoriaDto> PesquisarSubcategoriaPorFiltros(FiltroSubcategoriaDto filtroDto)
        {
            if (filtroDto.Ordem == null && filtroDto.Status == null)
            {
                var subcategoria = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower().Contains(filtroDto.Nome.ToLower())).ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Status != null)
            {
                var subcategoriaCrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower()) && subcategoria.Status == filtroDto.Status)
                    .OrderBy(crescente => crescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoriaCrescente);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Status != null)
            {
                var subcategoriaDecrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower()) && subcategoria.Status == filtroDto.Status)
                    .OrderByDescending(decrescente => decrescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoriaDecrescente);
                return subcategoriaDto;
            }
            if (filtroDto.Status != null && filtroDto.Nome != null && filtroDto.Ordem == null)
            {
                var subcategoria = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower()) && subcategoria.Status == filtroDto.Status)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                var subcategoriaCrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower())).OrderBy(crescente => crescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoriaCrescente);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                var subcategoriaDecrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower())).OrderByDescending(decrescente => decrescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoriaDecrescente);

                return subcategoriaDto;
            }
            return null;
        }

        public Result DeletarSubcategoria(int id)
        {
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
            if (subcategoria == null)
            {
                return Result.Fail("Não foi possível encontrar uma subcategoria com o id informado!");
            }
            _context.Remove(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditarStatus(int id)
        {
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
            List<Produto> produtos = _context.Produtos.Where(produto => produto.SubcategoriaId == id && produto.Status == true).ToList();
            if (produtos.Count > 0)
            {
                return Result.Fail("Não é possível inativar uma subcategoria com produtos ativos");
            }
            if (subcategoria.Status == false)
            {
                subcategoria.Status = true;
            }
            else
            {
                subcategoria.Status = false;
                foreach (Produto produto in produtos)
                {
                    if (subcategoria.Status == false)
                    {
                        produto.Status = false;
                    }
                }
            }
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditarSubcategoria(int id, UpdateSubcategoriaDto subcategoriaDto)
        {
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
            if (subcategoria == null)
            {
                return Result.Fail("Não foi possível identificar uma subcategoria com o id informado!");
            }
            _mapper.Map(subcategoriaDto, subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<ReadSubcategoriaDto> RecuperarSubcategoriaPorStatus(bool? status)
        {
            if (status != null)
            {
                List<Subcategoria> subcategoria = _context.Subcategorias.Where(sub => sub.Status == status).ToList();
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            return null;
        }

        public List<ReadSubcategoriaDto> RecuperarSubcategoriaPorId(int id)
        {
            List<Subcategoria> subcategoria = _context.Subcategorias.Where(sub => sub.Id == id).ToList();
            if (subcategoria != null)
            {
                List<ReadSubcategoriaDto> subcategoriaDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            return null;
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using FluentResults;
//using Microsoft.EntityFrameworkCore;
//using IEcommerceAPI.Data;
//using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
//using IEcommerceAPI.Models;

//namespace IEcommerceAPI.Repository

//{
//    public class SubcategoriaRepository
//    {
//        AppDbContext _context;
//        IMapper _mapper;

//        public SubcategoriaRepository(AppDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public Subcategoria BuscarSubcategoriaPorId(int id)
//        {
//            return _context.Subcategorias.FirstOrDefault(t => t.Id == id);
//        }

//        public IEnumerable<Subcategoria> BuscarTodasSubcategorias() => _context.Subcategorias;

//        public Result CriarSubcategoria(CreateSubcategoriaDto obj)
//        {
//            Subcategoria subcategorias = _mapper.Map<Subcategoria>(obj);
//            _context.Subcategorias.Add(subcategorias);
//            _context.SaveChanges();
//            return Result.Ok();
//        }

//        public void EditarSubcategoria(Subcategoria obj)
//        {
//            _context.Subcategorias.Update(obj);
//            _context.SaveChanges();
//        }

//        public void ExcluirSubcategoria(Subcategoria obj)
//        {
//            _context.Subcategorias.Remove(obj);
//            _context.SaveChanges();
//        }
//    }
//}
