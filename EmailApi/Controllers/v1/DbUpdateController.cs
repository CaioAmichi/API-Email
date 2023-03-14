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
	public class DbUpdateController : ApiControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IDbUpdateApplication _dbUpdateApplication;


		public DbUpdateController(IMapper mapper, IDbUpdateApplication dbUpdateApplication)
		{
			_mapper = mapper;
			_dbUpdateApplication = dbUpdateApplication;

		}


		[HttpPost("AtualizarBanco")]
		[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErroResult), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ErroResult), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AtualizarBanco()
		{
			var result = await _dbUpdateApplication.AtualizarDataBase();
			return result.IsValid ? Ok() : BadRequest(result.Notifications);
		}
		 
	}
}

