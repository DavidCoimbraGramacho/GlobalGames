using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GlobalGames.Models;
using GlobalGames.Dados.Entidades;
using System.IO;
using GlobalGames.Dados;

namespace GlobalGames.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Servicos()
        {
            return View();
        }

        public IActionResult Inscricoes()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscricoes([Bind("Id,Nome,Apelido,Morada,CC,Localidade,DataNascimento,ImageFile")] InscricoesViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                
                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                   Directory.GetCurrentDirectory(),
                   "wwwroot\\images\\inscricoes",
                   file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/inscricoes/{file}";
                }

                var inscricoes = this.ToInscricoes(view, path);

                _context.Add(inscricoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Inscricoes ToInscricoes(InscricoesViewModel view, string path)
        {
            return new Inscricoes
            {
                Id = view.Id,
                Nome = view.Nome,
                Apelido = view.Apelido,
                Morada = view.Morada,
                CC = view.CC,
                Localidade = view.Localidade,
                DataNascimento = view.DataNascimento,
                ImageUrl = path,
                User = view.User,
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
