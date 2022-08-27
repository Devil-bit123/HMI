using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WA_Interfaces.Context;
using WA_Interfaces.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WA_Interfaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sensorsController : ControllerBase
    {
        private readonly conexionSQL conntext;
        // GET: api/<sensorsController>
        public sensorsController(conexionSQL context)
        {
            this.conntext = context;

        }





        [HttpGet]
        public IEnumerable<sensores> Get()
        {
            return conntext.sensores.ToList();
        }

        // GET api/<sensorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<sensorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<sensorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<sensorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
