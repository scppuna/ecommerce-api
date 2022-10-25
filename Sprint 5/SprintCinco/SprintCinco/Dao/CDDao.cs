using Dapper;
using SprintCinco.Data;
using SprintCinco.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SprintCinco.Dao
{
    public class CDDao
    {
        AppDbContext _context;
        IDbConnection _dbConnection;

        public CDDao(AppDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public CentroDistribuicao BuscarCDPorId(int id)
        {
           return _context.CentrosDistribuicoes.FirstOrDefault(t => t.Id == id);
        }

        public Produto BuscaProdutoPorCentroID(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.CentroId == id);
            return produto;
        }

        public List<CentroDistribuicao> BuscarCDPorNome(string nome)
        {
            var pesquisaPorNome = _context.CentrosDistribuicoes.Where(centro => centro.Nome.ToLower().Equals(nome.ToLower())).ToList();
            return pesquisaPorNome;
        }

        public List<CentroDistribuicao> PesquisaCentroPorCEP(string cep)
        {
            return _context.CentrosDistribuicoes.Where(c => c.CEP.Equals(cep)).ToList();

        }

        public IEnumerable<CentroDistribuicao> BuscarCentrosDistribuicoes(int id) => _context.CentrosDistribuicoes;

        public void CriarCD(CentroDistribuicao obj)
        {
            _context.CentrosDistribuicoes.Add(obj);
            _context.SaveChanges();
        }

        public void EditarCD(CentroDistribuicao obj)
        {
            _context.CentrosDistribuicoes.Update(obj);
            _context.SaveChanges();
        }

        public void ExcluirCD(CentroDistribuicao obj)
        {
            _context.CentrosDistribuicoes.Remove(obj);
            _context.SaveChanges();
        }

        public IReadOnlyList<CentroDistribuicao> PesquisarCentroDistribuicaoComFiltro(string nome, string logradouro, string bairro,
            string localidade, string uf, string cep, bool? status, DateTime? dataCriacao, DateTime? dataEdicao, int? numero, string ordem, int itensPagina, int paginaAtual)
        {
            var sql = "SELECT * FROM CentrosDistribuicoes WHERE ";

            if (nome != null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and ";
            }
            if (logradouro != null)
            {
                sql += "Logradouro LIKE \"%" + logradouro + "%\" and ";
            }
            if (bairro != null)
            {
                sql += "Bairro = @bairro and ";
            }
            if (localidade != null)
            {
                sql += "Localidade = @localidade and ";
            }
            if (uf != null)
            {
                sql += "UF = @uf and ";
            }
            if (cep != null)
            {
                sql += "Cep = @cep and ";
            }
            if (status != null)
            {
                sql += "Status = @status and ";
            }
            if (dataCriacao != null)
            {
                sql += "DataCriacao = @dataCriacao and ";
            }
            if (dataEdicao != null)
            {
                sql += "DataEdicao = @dataEdicao and ";
            }
            if (numero != null)
            {
                sql += "Numero = @numero and ";
            }

            if (nome == null && logradouro == null && bairro == null && localidade == null && uf == null && cep == null &&
                status == null && dataCriacao == null && dataEdicao == null && numero == null)
            {
                var PosicaoDoWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(PosicaoDoWhere);
            }
            else
            {
                var posicaoDoAnd = sql.LastIndexOf("and");
                sql = sql.Remove(posicaoDoAnd);
            }
            if (ordem != null)
            {
                if (ordem != "desc")
                {
                    sql += " ORDER BY Nome";
                }
                else
                {
                    sql += " ORDER BY Nome DESC";
                }
            }

            var result = _dbConnection.Query<CentroDistribuicao>(sql, new
            {
                Nome = nome,
                Logradouro = logradouro,
                Bairro = bairro,
                Localidade = localidade,
                UF = uf,
                Cep = cep,
                Status = status,
                DataCriacao = dataCriacao,
                DataEdicao = dataEdicao,
                Numero = numero

            });

            if (paginaAtual > 0 && itensPagina > 0 && itensPagina <= 10)
            {
                var resultado = result.Skip((paginaAtual - 1) * itensPagina).Take(itensPagina).ToList();
                return resultado;

            }

            return result.ToList();


        }

    }
}
