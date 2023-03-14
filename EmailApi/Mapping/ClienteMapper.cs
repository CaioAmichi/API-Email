using AutoMapper;
using EmailApi.Domain.Entities;
using EmailApi.API.Dtos.Email;

namespace EmailApi.Api.Mapping
{
    public class ClienteMapper : Profile
    {
        /// <summary>
        /// Mapeamento De transações
        /// </summary>
        public ClienteMapper()
        {


            CreateMap<Cliente, AdicionarClienteRequest>().ReverseMap();
            CreateMap<Cliente, ObterClienteNomeRequest>().ReverseMap();
            CreateMap<Cliente, ObterClienteNomeResponse>().ReverseMap();


        }
    }
}
