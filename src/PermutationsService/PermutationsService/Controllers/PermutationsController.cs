using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermutationsService.Services.Abstract;
using PermutationsService.Web.DataAccess.Entities;

namespace PermutationsService.Web.Controllers
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

        /// <remarks>
        /// Вставка порционных данных плохо вписывается в REST, поэтому реализовано в виде " custom user function"
        /// </remarks>
        // POST: api/<controller>/bulk-insert
        [HttpPost]
        [Route("bulk-insert")]
        public async Task<ActionResult> AddPermutations([FromBody] string[] elements)
        {
            if (!elements.Any())
            {
                return BadRequest("The incoming array can not be empty");
            }

            List<PermutationEntry> result;
            try
            {
                result = (List<PermutationEntry>) await _permutationsService.GetPermutations(elements);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong during permutations calculation");
            }

            return Ok(result);
        }
    }
}
