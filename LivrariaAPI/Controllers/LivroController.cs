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
               
                return Ok(livroLista);
            }
            catch
            {
                return BadRequest("Não Atualizado");
            }
        }

        //public async Task<ActionResult> GetLivros()
        //{
        //    try
        //    {
        //        var livroLista = await _livroDAO.Listar();
        //        List<LivroViewModel> livrosViewModel = new List<LivroViewModel>();

        //        foreach (var livro in livroLista)
        //        {
        //            // DateTime? dataEntre = livro.Data_Publicacao != null ? DateTime.ParseExact(livro.Data_Publicacao.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

        //            var data = livro != null ? livro.Data_Publicacao.Value.ToString("dd/MM/YYYY") : null;
        //            var livroViewModel = new LivroViewModel
        //            {
        //                Id = livro.Id,
        //                ISBN = livro.ISBN,
        //                Autor = livro.Autor,
        //                Nome = livro.Nome,
        //                Preco = livro.Preco,
        //                Data_Publicacao = data,
        //                Url_Imagem = livro.Url_Imagem
        //            };

        //            livrosViewModel.Add(livro);

        //        }

        //        return Ok(livrosViewModel);
        //    }
        //    catch
        //    {
        //        return BadRequest("Não Atualizado");
        //    }
        //}
        // GET: api/Livro
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivroDetail(int? id)
        {          
            if (id.HasValue)
            {
                var livro = await _livroDAO.GetByLivroId(id);
                //var model = new LivroViewModel
                //{
                //    Id = livro.Id,
                //    ISBN = livro.ISBN,
                //    Autor = livro.Autor,
                //    Nome = livro.Nome,
                //    Preco = livro.Preco,
                //    Data_Publicacao = livro.Data_Publicacao,
                //    Url_Imagem = livro.Url_Imagem
                //};

                return Ok(livro); 
            }
            return NotFound();
            //d1.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
        }

        // POST: api/Livro
        [HttpPost]
        public async Task<ActionResult<Livro>> AddLivro(LivroViewModel model)
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

                var livro = new Livro
                {
                    ISBN = model.ISBN,
                    Autor = model.Autor,
                    Nome = model.Nome,
                    Preco = model.Preco,
                    Data_Publicacao = model.Data_Publicacao != null && model.Data_Publicacao != "string" ? Convert.ToDateTime(model.Data_Publicacao) : null,
                    Url_Imagem = model.Url_Imagem
                };

                await _livroDAO.CreateAsync(livro);
                return Ok("Cadastrado com Sucesso!");

            }

            return BadRequest("Não Atualizado");
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
