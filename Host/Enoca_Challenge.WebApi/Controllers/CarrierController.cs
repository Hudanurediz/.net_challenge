using Enoca_Challenge.Application.Features.Carriers.Commands.CreateCarrier;
using Enoca_Challenge.Application.Features.Carriers.Commands.DeleteCarrier;
using Enoca_Challenge.Application.Features.Carriers.Commands.UpdateCarrier;
using Enoca_Challenge.Application.Features.Carriers.Queries.GetAllCarriers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enoca_Challenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        readonly IMediator _mediator;

        public CarrierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarriersQueryRequest());
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarrierQueryRequest request)
        {

            var response = await _mediator.Send(request);    
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCarrierQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpDelete]
        public IActionResult Delete(DeleteCarrierQueryRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response.Result);
        }
    }
}
