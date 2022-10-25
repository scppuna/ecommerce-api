using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SprintCinco.Data;
using SprintCinco.Models;

namespace SprintCinco.Dao

{
    public class SubcategoriaDao
    {
        AppDbContext _context;
        IMapper _mapper;

        public SubcategoriaDao(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Subcategoria BuscarSubcategoriaPorId(int id)
        {
            return _context.Subcategorias.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Subcategoria> BuscarTodasSubcategorias() => _context.Subcategorias;

        public void CriarSubcategoria(Subcategoria obj)
        {
            _context.Subcategorias.Add(obj);
            _context.SaveChanges();
        }

        public void EditarSubcategoria(Subcategoria obj)
        {
            _context.Subcategorias.Update(obj);
            _context.SaveChanges();
        }

        public void ExcluirSubcategoria(Subcategoria obj)
        {
            _context.Subcategorias.Remove(obj);
            _context.SaveChanges();
        }
    }
}
