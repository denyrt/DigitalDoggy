using DigitalDoggy.BusinessLogic.ApiCommands;
using DigitalDoggy.BusinessLogic.ApiQueries;
using DigitalDoggy.BusinessLogic.Responses;
using DigitalDoggy.Domain.Constants;
using DigitalDoggy.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDoggy.Controllers
{
    [Route("api/dogs")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetDogsResponse), 200)]
        public async Task<IActionResult> GetDogsAsync([FromQuery] GetDogsQuery query)
        {
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateDogResponse), 200)]
        [ProducesResponseType(typeof(CreateDogResponse), 409)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateDogAsync([FromBody] CreateDogCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);
            return response.ToActionResult();
        }
    }
}