using EcommerceAPI.Data.Repository;
using FluentResults;
using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Services
{
    public class SubcategoriaService
    {
        private readonly SubcategoriaRepository _subcategoriaRepository;

        public SubcategoriaService(SubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        public Result CadastrarSubcategoria(CreateSubcategoriaDto dto)
        {
            var cadastroSubcategoria = _subcategoriaRepository.CadastrarSubcategoria(dto);
            if (cadastroSubcategoria.IsFailed)
            {
                return Result.Fail(cadastroSubcategoria.Errors.FirstOrDefault());
            }
            return Result.Ok();
        }

        public List<ReadSubcategoriaDto> RecuperarSubcategorias()
        {
            var pesquisa = _subcategoriaRepository.RecuperarSubcategorias();
            return pesquisa;
        }

        public List<ReadSubcategoriaDto> PesquisarSubcategoriaPorFiltros(FiltroSubcategoriaDto filtroDto)
        {
            if (filtroDto.Ordem == null && filtroDto.Status == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.PesquisarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;
            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Status != null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.PesquisarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;
            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Status != null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.PesquisarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;

            }
            if (filtroDto.Status != null && filtroDto.Nome != null && filtroDto.Ordem == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.PesquisarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;

            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.PesquisarSubcategoriaPorFiltros(filtroDto);

                return pesquisaFiltros;

            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.PesquisarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;
            }
            return null;
        }

        public Result DeletarSubcategoria(int id)
        {
            var deletaSubcategoria = _subcategoriaRepository.DeletarSubcategoria(id);
            if (deletaSubcategoria.IsFailed)
            {
                return Result.Fail("Subcategoria Não Encontrada");
            }
            return Result.Ok();

        }

        public Result EditarStatus(int id)
        {
            var editaStatus = _subcategoriaRepository.EditarStatus(id);
            if (editaStatus.IsFailed)
            {
                return Result.Fail("Subcategoria Não Encontrada");
            }
            return Result.Ok();
        }

        public Result EditarSubcategoria(int id, UpdateSubcategoriaDto subcategoriaDto)
        {
            var editaSubcategoria = _subcategoriaRepository.EditarSubcategoria(id, subcategoriaDto);
            if (editaSubcategoria.IsFailed)
            {
                return Result.Fail(editaSubcategoria.Errors.FirstOrDefault());
            }
            return Result.Ok();
        }

        public List<ReadSubcategoriaDto> PesquisarSubcategoriaPorStatus(bool? status)
        {
            var pesquisaStatus = _subcategoriaRepository.RecuperarSubcategoriaPorStatus(status);
            return pesquisaStatus;
        }

        public List<ReadSubcategoriaDto> PesquisarSubcategoriaPorId(int id)
        {
            var pesquisaId = _subcategoriaRepository.RecuperarSubcategoriaPorId(id);
            if (pesquisaId != null)
            {
                return pesquisaId;
            }
            return null;
        }
    }
}


//using AutoMapper;
//using FluentResults;
//using Microsoft.AspNetCore.Mvc;
//using IEcommerceAPI.Repository;
//using IEcommerceAPI.Data.Dtos.CategoriaDtos;
//using IEcommerceAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using IEcommerceAPI.Middleware;
//using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;

//namespace IEcommerceAPI.Services
//{
//    public class SubcategoriaService
//    {

//        private SubcategoriaRepository _repository;

//        public SubcategoriaService(SubcategoriaRepository repository)
//        {
//            _repository = repository;
//        }

//        public ReadSubcategoriaDto CadastrarSubcategoria(CreateSubcategoriaDto subcategoriaDto)
//        {
//            if (subcategoriaDto.Status == true)
//            {
//                Result cadastro = _repository.CadastrarSubcategorias(subcategoriaDto);
//            }
//            return null;
//        }


//        public List<ReadSubcategoriaDto> BuscarSubcategoriaPorId(int? id)
//        {
//            if (id != null)
//            {
//                var buscarSubcategoria = _repository.BuscarSubcategoriaPorId(id);
//                return buscarSubcategoria;
//            }
//            throw new NullException("Subcategoria não localizada!"); ;

//        }

//        public List<ReadSubcategoriaDto> BuscarSubcategoria(string nome)
//        {
//            if (string.IsNullOrEmpty(nome) || nome.Length < 3 || nome.Length > 128)
//            {
//                throw new NullException("Subcategoria não localizada!");
//            }
//            var buscarCategoria = _repository.BuscarTodasSubcategorias();
//            return buscarCategoria;
//        }

//        public Result EditarSubcategoria(int id, UpdateSubcategoriaDto subcategoria)
//        {
//            var editarSubcategoria = _repository.ExcluirSubcategoria(id);
//            if (editarSubcategoria != null)
//            {
//                subcategoria.DataEdicao = DateTime.Now;
//            }
//            return Result.Ok();
//        }

//        public Result EditarStatus(int? id)
//        {
//            if (id != null)
//            {
//                var pesquisa = _repository.BuscarSubcategoriaPorId(id);
//                return Result.Ok();
//            }
//            throw new NullException("Categoria não localizada!");
//        }

//        internal Result ExcluirSubcategoria(int id)
//        {
//            var categoria = _repository.BuscarSubcategoriaPorId(id);
//            if (categoria == null)
//            {
//                return Result.Fail("Categoria não encontrado");
//            }
//            return Result.Ok();
//        }

//    }
//}



//using FluentResults;
//using IEcommerceAPI.Repository;
//using IEcommerceAPI.Data.Dtos.SubcategoriaDtos;
//using System;

//namespace IEcommerceAPI.Services
//{
//    public class SubcategoriaService
//    {
//        internal SubcategoriaRepository _subcategoriaDao;

//        public SubcategoriaService(SubcategoriaRepository subcategoriaDao)
//        {
//            _subcategoriaDao = subcategoriaDao;
//        }

//        public ReadSubcategoriaDto CadastroSubcategoria(CreateSubcategoriaDto subcategoriaDto)
//        {
//            if (subcategoriaDto == null)
//            {
//                throw new ArgumentNullException(paramName: nameof(subcategoriaDto), message: "Categoria nula. Favor digitar");
//            }

//            var cadastro = _subcategoriaDao.CriarSubcategoria(subcategoriaDto);
//            return null;
//        }

//    }
//}
