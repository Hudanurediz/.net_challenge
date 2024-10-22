using Enoca_Challenge.Application.Features.Orders.Commands.CreateOrder;
using Enoca_Challenge.Application.Features.Orders.Commands.DeleteOrder;
using Enoca_Challenge.Application.Features.Orders.Commands.UpdateOrder;
using Enoca_Challenge.Application.Features.Orders.Queries.GetAllOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enoca_Challenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrdersQueryRequest());
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderQueryRequest request)
        {

            var response = await _mediator.Send(request);
            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateOrderQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpDelete]
        public IActionResult Delete(DeleteOrderQueryRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }
    }
}
