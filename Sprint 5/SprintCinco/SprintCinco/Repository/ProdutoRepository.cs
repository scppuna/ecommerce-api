using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IEcommerceAPI.Data;
using IEcommerceAPI.Models;

namespace IEcommerceAPI.Repository

{
    public class ProdutoRepository
    {
        AppDbContext _context;
        IDbConnection _dbConnection;

        public ProdutoRepository(AppDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public Produto BuscarProdutoPorId(int id)
        {
            return _context.Produtos.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Produto> BuscarTodosProdutos(int id) => _context.Produtos;

        public void CriarProduto(Produto obj)
        {
            _context.Produtos.Add(obj);
            _context.SaveChanges();
        }

        public void EditarProduto(Produto obj)
        {
            _context.Produtos.Update(obj);
            _context.SaveChanges();
        }

        public void ExcluirProduto(Produto obj)
        {
            _context.Produtos.Remove(obj);
            _context.SaveChanges();
        }

        public async Task<IReadOnlyList<Produto>> GetProdutosAsync(int id)
        {
            var sql = "SELECT * FROM Costumers WHERE Id = @id";

            var result = await _dbConnection.QueryAsync<Produto>(sql, new { Id = id });

            return result.ToList();
        }

        public IReadOnlyList<Produto> GetPesquisarComFiltro(string nome, string centro, bool? status, double? peso, double? altura, double? largura,
             double? comprimento, double? valor, int? estoque, string ordem, int itensPagina, int paginaAtual)
        {
            var sql = "SELECT * FROM Produtos WHERE ";

            if (nome != null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and ";
            }
            if (centro != null)
            {
                sql += "CentroDistribuicao LIKE \"%" + centro + "%\" and ";
            }
            if (status != null)
            {
                sql += "Status = @status and ";
            }
            if (peso != null)
            {
                sql += "Peso = @peso and ";
            }
            if (altura != null)
            {
                sql += "Altura = @altura and ";
            }
            if (largura != null)
            {
                sql += "Largura = @largura and ";
            }
            if (comprimento != null)
            {
                sql += "Comprimento = @comprimento and ";
            }
            if (valor != null)
            {
                sql += "Valor = @valor and ";
            }
            if (estoque != null)
            {
                sql += "Estoque = @estoque and ";
            }

            if (nome == null && centro == null && estoque == null && valor == null && comprimento == null && largura == null &&
                altura == null && peso == null && status == null)
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

            var result = _dbConnection.Query<Produto>(sql, new
            {
                Nome = nome,
                CentroDistribuicao = centro,
                Status = status,
                Peso = peso,
                Altura = altura,
                Largura = largura,
                Comprimento = comprimento,
                Valor = valor,
                Estoque = estoque
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
