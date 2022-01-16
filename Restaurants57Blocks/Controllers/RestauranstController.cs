using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurants57Blocks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranstController : ControllerBase
    {
        // GET: api/<RestauranstController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RestauranstController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RestauranstController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RestauranstController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestauranstController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
