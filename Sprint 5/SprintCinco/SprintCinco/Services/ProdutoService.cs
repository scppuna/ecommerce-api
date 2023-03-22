using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using IEcommerceAPI.Data.Dtos.ProdutoDtos;
using IEcommerceAPI.Models;
using IEcommerceAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

//Criar regras de negócio

namespace IEcommerceAPI.Service
{
    public class ProdutoService
    {
        private IMapper _mapper; //mover para repository
        private ProdutoRepository _repository;

        public ProdutoService(IMapper mapper, ProdutoRepository repository)
        {

            _mapper = mapper;
            _repository = repository;
        }

        public ReadProdutoDto AdicionaProduto(CreateProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _repository.CriarProduto(produto);
            return _mapper.Map<ReadProdutoDto>(produto);

        }

        public ReadProdutoDto BuscarProdutoPorId(int id)
        {
            var produto = _repository.BuscarProdutoPorId(id);
            if (produto != null)
            {
                ReadProdutoDto produtoDto = _mapper.Map<ReadProdutoDto>(produto);
                return produtoDto;
            }
            return null;
        }

        public Result EditarProduto(int id, [FromBody] UpdateProdutoDto produto)
        {

            var pesquisa = _repository.BuscarProdutoPorId(id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }

            _repository.EditarProduto(pesquisa);
            return Result.Ok();
        }

        public Result EditarStatus(int id)
        {
            var produto = _repository.BuscarProdutoPorId(id);
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
            _repository.EditarProduto(produto);
            return Result.Ok();
        }

        internal Result ExcluirProduto(int id)
        {
            var produto = _repository.BuscarProdutoPorId(id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            return Result.Ok();
        }

        public IReadOnlyList<Produto> PesquisarFiltro([FromQuery] string nome, [FromQuery] string centro, [FromQuery] bool? status, [FromQuery] double? peso, [FromQuery] double? altura, [FromQuery] double? largura,
    [FromQuery] double? comprimento, [FromQuery] double? valor, [FromQuery] int? estoque, [FromQuery] string ordem, [FromQuery] int itensPagina, [FromQuery] int paginaAtual)
        {
            return _repository.GetPesquisarComFiltro(nome, centro, status, peso, altura, largura,
                comprimento, valor, estoque, ordem, itensPagina, paginaAtual);
        }
    }
}
