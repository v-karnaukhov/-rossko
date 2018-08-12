using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermutationsService.Data.ServicesData;
using PermutationsService.Services.Abstract;

namespace PermutationsService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PermutationsController : ControllerBase
    {
        private readonly IPermutationsService _permutationsService;

        public PermutationsController(IPermutationsService permutationsService)
        {
            _permutationsService = permutationsService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[] {"value1", "value2"});
        }

        // POST: api/<controller>/add
        [HttpPost]
        [Route("bulk-insert")]
        public OkObjectResult AddPermutations([FromBody] string[] elements)
        {
            var result = new List<PermutationEntry>();
            foreach (var element in elements)
            {
                result.Add(_permutationsService.GetPermutations(element));
            }

            return Ok(result);
        }

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
