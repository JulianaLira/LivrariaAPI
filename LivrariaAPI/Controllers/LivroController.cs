using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaAPI.DAO;
using LivrariaAPI.Data;
using LivrariaAPI.Models;
using LivrariaAPI.Models.ViewModels;
using System.Globalization;
using System.IO;
using LivrariaAPI.infra;

namespace LivrariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILivroDAO _livroDAO;

        public LivroController(ApplicationDbContext context, ILivroDAO livroDAO)
        {
            _context = context;
            _livroDAO = livroDAO;
        }

        // GET: api/Livro
        [HttpGet]
        public async Task<ActionResult> GetLivros()
        {
            try
            {
                var livroLista = await _livroDAO.Listar();

                var livrosViewModel = new List<LivroViewModel>();

                foreach (var livro in livroLista)
                {                    
                    var data = livro.Data_Publicacao != null ? livro.Data_Publicacao.Value.ToString("dd/MM/yyyy") : null;
                    var livroViewModel = new LivroViewModel
                    {
                        Id = livro.Id,
                        ISBN = livro.ISBN,
                        Autor = livro.Autor,
                        Nome = livro.Nome,
                        Preco = livro.Preco,
                        Data_Publicacao = data,
                        Url_Imagem = livro.Url_Imagem
                    };

                    livrosViewModel.Add(livroViewModel);

                }

                return Ok(livrosViewModel);
            }
            catch
            {
                return BadRequest("Erro ao recuperar a lista!");
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivroDetail(int? id)
        {          
            if (id.HasValue)
            {
                var livro = await _livroDAO.GetByLivroId(id);

                var data = livro.Data_Publicacao != null ? livro.Data_Publicacao.Value.ToString("dd/MM/yyyy") : null;

                var model = new LivroViewModel
                {
                    Id = livro.Id,
                    ISBN = livro.ISBN,
                    Autor = livro.Autor,
                    Nome = livro.Nome,
                    Preco = livro.Preco,
                    Data_Publicacao = data,
                    Url_Imagem = livro.Url_Imagem
                };

                return Ok(model); 
            }
            return BadRequest("Erro ao recuperar o livro!");
        }

        // POST: api/Livro
        [HttpPost]
        public async Task<ActionResult<Livro>> AddLivro([FromForm]LivroViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ISBN == null || model.ISBN == 0) 
                {
                    return BadRequest("ISBN Obrigatorio!");
                }

                var Isbn = await _livroDAO.GetISBN(model.ISBN);
                if (Isbn != null) 
                {
                    return BadRequest("ISBN já cadastrada no sistema, informe outra!");
                }

                string path = null;
                if (model.IFormImage.Length > 0)
                {
                    SharedClass sharedClass = new SharedClass();
                    path = await sharedClass.PostFile(model.IFormImage, ("imagens/"), "Capa_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ssss"));
                }

                var livro = new Livro
                {
                    ISBN = model.ISBN,
                    Autor = model.Autor,
                    Nome = model.Nome,
                    Preco = model.Preco,
                    Data_Publicacao = model.Data_Publicacao != null && model.Data_Publicacao != "string" ? Convert.ToDateTime(model.Data_Publicacao) : null,
                    Url_Imagem = path
                };

                await _livroDAO.CreateAsync(livro);
                return Ok("Cadastrado com Sucesso!");

            }

            return BadRequest("Erro ao cadastrar o livro!");
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int? id)
        {
            if (id.HasValue)
            {
                var livro = await _livroDAO.GetByLivroId(id);
                if (livro != null) 
                {
                    await _livroDAO.Delete(livro);
                    return Ok("Deletado com Sucesso!");
                }
               
            }
            return BadRequest("Livro não Encontrado!");
        }
    }
}
