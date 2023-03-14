using EmailApi.Domain.Entities;
using EmailApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApi.Domain.Applications
{
    public interface IClienteApplication
    {
        Task<Result> CriarCliente(Cliente cliente);
        Task<Result<Cliente>> ObterClienteNome(string nome);
        Task<Result> DeletarCliente(Guid idCliente);
        Task<Result<List<Cliente>>> ListarClientes();
    }
}
