using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SprintCinco.Data.Dtos.ProdutoDtos;
using SprintCinco.Models;
using SprintCinco.Dao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

//Criar regras de negócio

namespace SprintCinco.Service
{
    public class ProdutoService
    {
        private IMapper _mapper;
        private ProdutoDao _dao;

        public ProdutoService(IMapper mapper, ProdutoDao dao)
        {

            _mapper = mapper;
            _dao = dao;
        }

        internal ReadProdutoDto AdicionaProduto(CreateProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _dao.CriarProduto(produto);
            return _mapper.Map<ReadProdutoDto>(produto);

        }

        public ReadProdutoDto BuscarProdutoPorId(int id)
        {
            var produto = _dao.BuscarProdutoPorId(id);
            if (produto != null)
            {
                ReadProdutoDto produtoDto = _mapper.Map<ReadProdutoDto>(produto);
                return produtoDto;
            }
            return null;
        }

        public Result EditarProduto(int id, [FromBody] UpdateProdutoDto produto)
        {

            var pesquisa = _dao.BuscarProdutoPorId(id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }

            _dao.EditarProduto(pesquisa);
            return Result.Ok();
        }

        public Result EditarStatus(int id)
        {
            var produto = _dao.BuscarProdutoPorId(id);
            produto.DataEdicao = DateTime.Now;
            if (produto.Status != null)
            {
                if (produto.Status == true)
                {
                    produto.Status = false;
                }
                else
                {
                    produto.Status = true;
                }
            }
            _dao.EditarProduto(produto);
            return Result.Ok();
        }

        internal Result ExcluirProduto(int id)
        {
            var produto = _dao.BuscarProdutoPorId(id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            return Result.Ok();
        }

        public IReadOnlyList<Produto> PesquisarFiltro([FromQuery] string nome, [FromQuery] string centro, [FromQuery] bool? status, [FromQuery] double? peso, [FromQuery] double? altura, [FromQuery] double? largura,
    [FromQuery] double? comprimento, [FromQuery] double? valor, [FromQuery] int? estoque, [FromQuery] string ordem, [FromQuery] int itensPagina, [FromQuery] int paginaAtual)
        {
            return _dao.GetPesquisarComFiltro(nome, centro, status, peso, altura, largura,
                comprimento, valor, estoque, ordem, itensPagina, paginaAtual);
        }
    }
}
