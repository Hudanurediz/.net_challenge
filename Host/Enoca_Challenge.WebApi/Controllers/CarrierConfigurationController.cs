using Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.CreateCarrierConfiguration;
using Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.DeleteCarrierConfiguration;
using Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.UpdateCarrierConfiguration;
using Enoca_Challenge.Application.Features.CarrierConfigurations.Queries.GetAllCarrierConfigurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enoca_Challenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationController : ControllerBase
    {

        readonly IMediator _mediator;

        public CarrierConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarrierConfigurationsQueryRequest());
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarrierConfigurationQueryRequest request)
        {

            var response = await _mediator.Send(request);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCarrierConfigurationQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteCarrierConfigurationQueryRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }



    }
}
