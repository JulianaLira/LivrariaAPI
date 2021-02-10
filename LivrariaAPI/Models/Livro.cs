using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Models
{
    public class Livro
    {
        [Required]       
        public int Id { get; set; }

        public int? ISBN { get; set; }

        public string Autor { get; set; }
   
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Preco { get; set; }

        public DateTime? Data_Publicacao { get; set; }

        public string Url_Imagem { get; set; }
    }
}
