using GlobalGames.Dados.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGames.Dados
{
    public class InscricoesRepository : GenericRepository<Inscricoes>, IInscricoesRepository
    {
        public InscricoesRepository(DataContext context) : base(context)
        {

        }
    }
}
