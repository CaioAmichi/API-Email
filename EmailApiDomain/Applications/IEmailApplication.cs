using EmailApi.Domain.Entities;
using EmailApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApi.Domain.Applications
{
    public interface IEmailApplication
    {
        Task<Result> EnviarEmail(Email email);
    }
}
