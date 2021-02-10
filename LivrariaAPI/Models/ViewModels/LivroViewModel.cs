using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Models.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required]
        public int? ISBN { get; set; }
        public string Autor { get; set; }
        public string Nome { get; set; }
        public decimal? Preco { get; set; }
        public string Data_Publicacao { get; set; }
        public string Url_Imagem { get; set; }
    }
}
