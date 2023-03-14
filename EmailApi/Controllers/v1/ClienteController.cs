using AutoMapper;
using EmailApi.Api.Controllers;
using EmailApi.Api.Dtos;
using EmailApi.API.Dtos.Email;
using EmailApi.Domain.Applications;
using EmailApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailApi.API.Controllers.v1
{
    [AllowAnonymous]
    [Route("v1/[controller]")]
    public class ClienteController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClienteApplication _clienteApplication;


        public ClienteController(IMapper mapper, IClienteApplication clienteApplication)
        {
            _mapper = mapper;
            _clienteApplication = clienteApplication;

        }


        [HttpPost("AdicionarCliente")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdicionarCliente([FromBody] AdicionarClienteRequest request)
        {
            var result = await _clienteApplication.CriarCliente(_mapper.Map<Cliente>(request));
            return result.IsValid ? Ok() : BadRequest(result.Notifications);
        }

        [HttpPost("ListarClientes")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodosClientes()
        {
            var result = await _clienteApplication.ListarClientes();
            var response = _mapper.Map<List<ObterClienteNomeResponse>>(result.Valor);
            return result.IsValid ? Ok(response) : BadRequest(result.Notifications);
        }


        [HttpPost("ObterClienteNome")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterCliente([FromBody] ObterClienteNomeRequest request)
        {
            var result = await _clienteApplication.ObterClienteNome(request.NomeEmpresa);
            var response = _mapper.Map<ObterClienteNomeResponse>(result.Valor);
            return result.IsValid ? Ok(response) : BadRequest(result.Notifications);
        }

        [HttpPost("DeletarCliente")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarCliente([FromBody] DeletarClienteRequest request)
        {
            var result = await _clienteApplication.DeletarCliente(request.IdCliente);
            return result.IsValid ? Ok() : BadRequest(result.Notifications);
        }


    }


}

