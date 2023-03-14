using EmailApi.Domain;
using EmailApi.Domain.Entities;
using EmailApi.Domain.IRepository;
using EmailApi.Domain.Models;
using EmailApi.Infrastructure.Contexto;
using EmailApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Infrastructure.Repositories
{
    public class DbUpdateRepositorio : Repositorio<Entidade>, IDbUpdateRepositorio
    {
        public DbUpdateRepositorio(DBContexto context) : base(context) { }

        public void AtualizarBanco()
        {
            Db.Database.Migrate();
        }
    }
}
