using LivrariaAPI.Data;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.DAO
{
    public class LivroDAO : ILivroDAO
    {
        private readonly ApplicationDbContext _context;

        public LivroDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Livro livro)
        {
            _context.Add(livro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Livro livro)
        {
            _context.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Livro livro)
        {
            _context.Remove(livro);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Livro>> ListAllAsync()
        {
            var list = await _context.Livro
                 .AsNoTracking()
                 .ToListAsync();
            return list;
        }

        public async Task<ICollection<Livro>> Listar()
        {
            return await _context.Livro
                .ToListAsync();
        }

        public async Task<ICollection<Livro>> FiltroLivro(string autor, string nome)
        {
            return await _context.Livro
                .Where(p => (autor == null || p.Autor == autor)
                && (nome == null || p.Nome == nome))

                .ToListAsync();
        }

        //public async Task<ICollection<Livro>> FiltroLivro(int ProfessorTableId, string titulo1, string categoria1, string status1)
        //{
        //    return await context.CursosTable.
        //        Where(p => (p.ProfessorTableId == ProfessorTableId)
        //        && (titulo1 == null || p.Titulo == titulo1)
        //        && (categoria1 == null || p.Categoria == categoria1)
        //        && (status1 == null || p.Status == status1)).ToListAsync();
        //}

        public async Task<Livro> GetByLivroId(int? Id)
        {
            return await _context.Livro
                .AsNoTracking()
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Livro> GetISBN(int? Isbn)
        {
            return await _context.Livro
                .AsNoTracking()
                .Where(p => p.ISBN == Isbn)
                .FirstOrDefaultAsync();
        }
    }
}
