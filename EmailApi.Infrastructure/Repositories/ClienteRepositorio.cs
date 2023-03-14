using EmailApi.Domain.Entities;
using EmailApi.Domain.IRepository;
using EmailApi.Infrastructure.Contexto;

namespace EmailApi.Infrastructure.Repositories
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(DBContexto context) : base(context) { }

    }
}
