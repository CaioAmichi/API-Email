using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Flunt.Notifications;
using System.Security.Claims;
using EmailApi.Api.Dtos;

namespace EmailApi.Api.Controllers
{
    /// <summary>
    /// Classe base para as controllers da aplicação
    /// </summary>
    
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Erro 400
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        protected IActionResult BadRequest(IReadOnlyCollection<Notification> notifications)
        {
            var erros = notifications.Select(n => new Erro(n.Key, n.Message)).ToList();
            return new BadRequestObjectResult(new ErroResult(erros));
        }


    }
}
