using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants57Blocks.Api.Middleware;
using Restaurants57Blocks.Application;
using Restaurants57Blocks.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurants57Blocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }
        // GET: api/<RestaurantController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get()
        {
            HttpContext.Request.Headers.TryGetValue("Token", out var traceValue);
            var Result = _restaurantServices.GetAll(traceValue.ToString());
            return StatusCode(Result.StatusCode, Result);
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{idRestaurant}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult Get(string idRestaurant)
        {
            HttpContext.Request.Headers.TryGetValue("Token", out var traceValue);
            var Result = _restaurantServices.GetById(idRestaurant, traceValue);
            return StatusCode(Result.StatusCode, Result);
        }

        // POST api/<RestaurantController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] RestaurantRequest value)
        {
            var Result = await _restaurantServices.AddAsync(value);
            return StatusCode(Result.StatusCode, Result);
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] RestaurantRequest value)
        {
        }
    }
}
