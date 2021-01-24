using GlobalGames.Dados.Entidades;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGames.Models
{
    public class InscricoesViewModel : Inscricoes
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
