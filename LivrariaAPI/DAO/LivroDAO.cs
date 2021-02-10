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
