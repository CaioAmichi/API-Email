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
	public class EmailController : ApiControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IEmailApplication _EmailAplication;


		public EmailController(IMapper mapper, IEmailApplication EmailAplication)
		{
			_mapper = mapper;
			_EmailAplication = EmailAplication;

		}


		[HttpPost("EnviarEmail")]
		[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErroResult), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ErroResult), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ObterEmailPorIdUsuario([FromBody] EnviarEmailRequest request)
		{
			var result = await _EmailAplication.EnviarEmail(_mapper.Map<Email>(request));
			return result.IsValid ? Ok() : BadRequest(result.Notifications);
		}
		 
	}
}

