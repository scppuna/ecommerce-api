using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SprintCinco.Data;
using SprintCinco.Models;

namespace SprintCinco.Dao
{
    public class CategoriaDao
    {
        AppDbContext _context;
        IMapper _mapper;

        public CategoriaDao(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public Categoria BuscarCategoriaPorId(int id)
        {
            return _context.Categorias.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Categoria> BuscarTodasCategorias() => _context.Categorias;

        public void CriarCategoria(Categoria obj)
        {
            _context.Categorias.Add(obj);
            _context.SaveChanges();
        }

        public void EditarCategoria(Categoria obj)
        {
            _context.Categorias.Update(obj);
            _context.SaveChanges();
        }

        public void ExcluirCategoria(Categoria obj)
        {
            _context.Categorias.Remove(obj);
            _context.SaveChanges();
        }
    }
}
