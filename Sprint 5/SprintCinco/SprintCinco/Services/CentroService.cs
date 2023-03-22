﻿using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using IEcommerceAPI.Repository;
using IEcommerceAPI.Data.Dtos.CentroDistribuicaoDtos;
using IEcommerceAPI.Middleware;
using IEcommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace IEcommerceAPI.Services
{
    public class CentroService
    {
        private IMapper _mapper;
        private CDRepository _repository;


        public CentroService(IMapper mapper, CDRepository repository)
        {

            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CentroDistribuicao> PesquisaCentroCep(string cep)
        {
            HttpClient client = new HttpClient();
            var resposta = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var conteudo = await resposta.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<CentroDistribuicao>(conteudo);
            return endereco;
        }

        public async Task<CentroDistribuicao> AdicionaCentro(CreateCDDto centroDto)
        {
            var centro = _mapper.Map<CentroDistribuicao>(centroDto);
            var pesquisaEndereco = await PesquisaCentroCep(centroDto.Cep);
            if (centroDto.Nome == centro.Nome && centroDto.Logradouro == centro.Logradouro)
            {
                throw new Exception("Centro já existente, favor verificar!");
            }
            centro.Logradouro = pesquisaEndereco.Logradouro;
            centro.Bairro = pesquisaEndereco.Bairro;
            centro.Localidade = pesquisaEndereco.Localidade;
            centro.UF = pesquisaEndereco.UF;
            _repository.CriarCD(centro);
            return centro;
        }

        public ReadCDDto BuscarCDPorId(int id)
        {
            var centro = _repository.BuscarCDPorId(id);
            if (centro != null)
            {
                ReadCDDto buscarCD = _mapper.Map<ReadCDDto>(centro);
                return buscarCD;
            }
            return null;
        }
        public Result EditarCD(int id, UpdateCDDto centro)
        {
            var centroDistribuicao = _repository.BuscarCDPorId(id);
            if (centroDistribuicao == null)
            {
                throw new ArgumentNullException("Centro nulo, favor verificar.");
            }

            if (centro.Nome == centroDistribuicao.Nome)
            {
                throw new ArgumentException("Impossível cadastrar pois o centro já está cadastrado");
            }

            centroDistribuicao.Nome = centro.Nome;
            centroDistribuicao.Logradouro = centro.Logradouro;
            centroDistribuicao.Bairro = centro.Bairro;
            centroDistribuicao.Localidade = centro.Localidade;
            centroDistribuicao.UF = centro.UF;
            _repository.EditarCD(centroDistribuicao);
            return Result.Ok();
        }

        public Result EditarStatus(int id, [FromBody] UpdateCDDto centro)
        {
            var pesquisa = _repository.BuscarCDPorId(id);
            var pesquisaProduto = _repository.BuscaProdutoPorCentroID(id);
            centro.DataEdicao = DateTime.Now;
            if (pesquisaProduto == null)
            {
                throw new NullException("Centro não localizado!");
            }

            var pesquisaProdutoPorCentroDistribuicao = _repository.BuscaProdutoPorCentroID(id);
            if (pesquisaProdutoPorCentroDistribuicao.Status == true)
            {
                throw new ArgumentException("Produto ativo.\nImpossível inativar Centro de Distribuição.");
            }

            if (pesquisa.Status != null)
            {
                if (centro.Status == true)
                {
                    centro.Status = false;
                }
                centro.Status = true;

            }
            _repository.EditarCD(pesquisa);
            return Result.Ok();
        }

        internal Result ExcluirCentroDeDistribuicao(int id)
        {
            var centro = _repository.BuscarCDPorId(id);
            if (centro == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            _repository.ExcluirCD(centro);
            return Result.Ok();
        }

        public IReadOnlyList<CentroDistribuicao> PesquisarCentroDistribuicaoComFiltro(string nome, string logradouro, string bairro,
       string localidade, string uf, string cep, bool? status, DateTime? dataCriacao, DateTime? dataEdicao, int? numero, string ordem, int itensPagina, int paginaAtual)
        {
            return _repository.PesquisarCentroDistribuicaoComFiltro(nome, logradouro, bairro, localidade, uf, cep,
                status, dataCriacao, dataEdicao, numero, ordem, itensPagina, paginaAtual);
        }

    }

}
