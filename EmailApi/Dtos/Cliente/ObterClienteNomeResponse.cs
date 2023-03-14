using EmailApi.Domain.Models;
using System;
using System.Collections.Generic;

namespace EmailApi.Domain.Entities
{
    public class ObterClienteNomeResponse
    {
        public string NomeEmpresa { get; set; }
        public Guid Id { get; set; }

    }
}