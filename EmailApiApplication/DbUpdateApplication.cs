
using EmailApi.Domain.Applications;
using EmailApi.Domain.Consts;
using EmailApi.Domain.Entities;
using EmailApi.Domain.IRepository;
using EmailApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailApi.Application
{
    public class DbUpdateApplication : IDbUpdateApplication
    {
        private readonly IDbUpdateRepositorio _dbUpdateRepository;

        public DbUpdateApplication(IDbUpdateRepositorio dbUpdateRepositorio)
        {
            _dbUpdateRepository = dbUpdateRepositorio;
        }

        public async Task<Result> AtualizarDataBase()
        {

            Result response = Result.Ok();

            try
            {
                _dbUpdateRepository.AtualizarBanco();
            }
            catch (Exception ex)
            {
                response = Result.Error(Errors.GlobalErroDeRepositorio.Key, ex.Message);
            }


            return response;
        }
    }
}
