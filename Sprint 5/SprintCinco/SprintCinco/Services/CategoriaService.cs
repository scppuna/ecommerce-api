using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using IEcommerceAPI.Repository;
using IEcommerceAPI.Data.Dtos.CategoriaDtos;
using IEcommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using IEcommerceAPI.Middleware;

namespace IEcommerceAPI.Services
{
    public class CategoriaService
    {

        private CategoriaRepository _repository;

        public CategoriaService(CategoriaRepository repository)
        {
            _repository = repository;
        }

        public ReadCategoriaDto CadastrarCategoria(CreateCategoriaDto categoriaDto)
        {
            if (categoriaDto.Status == true)
            {
                Result cadastro = _repository.CadastrarCategorias(categoriaDto);
            }
            return null;
        }


        public List<ReadCategoriaDto> BuscarCategoriaPorId(int? id)
        {
            if (id != null)
            {
                var buscarCategoria = _repository.BuscarCategoriaPorId(id);
                return buscarCategoria;
            }
            throw new NullException("Categoria não localizada!"); ;

        }

        public List<ReadCategoriaDto> BuscarCategoria(string nome)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length < 3 || nome.Length > 128)
            {
                throw new NullException("Categoria não localizada!");
            }
            var buscarCategoria = _repository.BuscarTodasCategorias();
            return buscarCategoria;
        }

        public Result EditarCategoria(int id, UpdateCategoriaDto categoria)
        {
            var editarCategoria = _repository.EditarCategoria(id, categoria);
            if (editarCategoria != null)
            {
                categoria.DataEdicao = DateTime.Now;
            }
            return Result.Ok();
        }

        public Result EditarStatus(int? id)
        {
            if (id != null)
            {
                var pesquisa = _repository.BuscarCategoriaPorId(id);
                return Result.Ok();
            }
            throw new NullException("Categoria não localizada!");
        }

        internal Result ExcluirCategoria(int id)
        {
            var categoria = _repository.BuscarCategoriaPorId(id);
            if (categoria == null)
            {
                return Result.Fail("Categoria não encontrado");
            }
            return Result.Ok();
        }

    }
}
