using AutoMapper;
using EmailApi.Domain.Entities;
using EmailApi.API.Dtos.Email;

namespace EmailApi.Api.Mapping
{
    public class EmailMapper : Profile
    {
        /// <summary>
        /// Mapeamento De transações
        /// </summary>
        public EmailMapper()
        {


            CreateMap<Email, EnviarEmailRequest>().ReverseMap();


        }
    }
}
