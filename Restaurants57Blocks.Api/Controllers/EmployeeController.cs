using Microsoft.AspNetCore.Mvc;
using Restaurants57Blocks.Application;
using Restaurants57Blocks.Domain.Request;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurants57Blocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }


        // GET: api/<EmployeeeController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get()
        {
            HttpContext.Request.Headers.TryGetValue("Token", out var traceValue);
            var Result = _employeeServices.GetAll(traceValue);
            return Ok(Result);
        }
        // GET api/<EmployeeeController>/5
        [HttpGet("{identificacion}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult Get(int identificacion)
        {
            HttpContext.Request.Headers.TryGetValue("Token", out var traceValue);
            var Result = _employeeServices.GetById(identificacion, traceValue);
            return StatusCode(Result.StatusCode, Result);
        }

        // POST api/<EmployeeeController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] EmployeeRequest employee)
        {
            var Result = await _employeeServices.AddAsync(employee);
            return StatusCode(Result.StatusCode, Result);
        }
    }
}
