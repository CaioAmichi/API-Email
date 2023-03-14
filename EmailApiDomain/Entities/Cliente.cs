using EmailApi.Domain.Models;
using System;
using System.Collections.Generic;

namespace EmailApi.Domain.Entities
{
    public class Cliente : Entidade
    {
       
        public string NomeEmpresa { get; set; }
        public string EmailRemetente { get; set; }
        public string SenhaRemetente { get; set; }
        public string Smtp { get; set; }

    }
}