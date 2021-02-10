using LivrariaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.DAO
{
    public interface ILivroDAO
    {
        Task CreateAsync(Livro livro);
        Task Delete(Livro livro);
        Task<Livro> GetByLivroId(int? Id);
        Task<List<Livro>> ListAllAsync();
        Task<ICollection<Livro>> Listar();
        Task UpdateAsync(Livro livro);

        Task<Livro> GetISBN(int? Isbn);
    }
}