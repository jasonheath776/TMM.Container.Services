using Customers.API.Application.Commands;
using Customers.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customers.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(
             IMediator mediator,
             ILogger<CustomerController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Customer>> CreateCustomerAsync([FromBody] CreateCustomerCommand customer)
        {
            try
            {
                Customer commandResult = await _mediator.Send(customer);

                if (commandResult == null)
                {
                    return BadRequest();
                }

                return Created(Request.Path, commandResult);
            }
            catch(CustomerDomainException cde)
            {
                
                    return BadRequest(cde.Message + "\n" + cde.InnerException?.Message);
                
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Customer>> UpdateCustomerAsync([FromBody] UpdateCustomerCommand customer)
        {
            Customer commandResult = await _mediator.Send(customer);

            if (commandResult == null)
            {
                return BadRequest();
            }

            return Ok(commandResult);
        }
    }
}
