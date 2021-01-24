using GlobalGames.Dados.Entidades;
using GlobalGames.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGames.Dados
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }
        
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("davidfgramacho@gmail.pt");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "David",
                    LastName = "Gramacho",
                    Email = "davidfgramacho@gmail.pt",
                    UserName = "davidfgramacho@gmail.pt",
                    PhoneNumber = "21234556"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Inscricoes.Any())
            {
                this.AddInscricoes("Bernardo Teles", user);
                this.AddInscricoes("Pilar Almeida", user);
                this.AddInscricoes("Alves Cabral", user);
                this.AddInscricoes("Pedro Lima", user);
                this.AddInscricoes("Cesar Conceição", user);
                await this.context.SaveChangesAsync();
            }
        }

        
        private void AddInscricoes(string name, User user)
        {
            this.context.Inscricoes.Add(new Inscricoes
            {
                Nome = name,
                Apelido = name,
                CC = this.random.Next(30000000),
                User = user
            });
        }
    }
}
