using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Livro> Livro { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Livro>()
                .Property(p => p.Autor)
                .HasMaxLength(80);

            builder.Entity<Livro>()
                .Property(p => p.Nome)
                .HasMaxLength(80);

            base.OnModelCreating(builder);
        }
    }
}
