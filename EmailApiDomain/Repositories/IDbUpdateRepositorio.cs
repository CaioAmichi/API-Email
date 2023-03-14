using EmailApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailApi.Domain.IRepository
{
    public interface IDbUpdateRepositorio : IRepositorio<Entidade>
    {
        public void AtualizarBanco();

    }

}

