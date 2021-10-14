using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAOLibrary.Repository;
using DTOLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HOVStory.Controllers
{
    [Route("api/confession")]
    [ApiController]
    public class ConfessionController : ControllerBase
    {
        private IConfessionRepository confessionRepository;

        public ConfessionController()
        {
            confessionRepository = new ConfessionRepository();
        }

        // GET: api/confession
        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<Confession>), 200)]
        public IActionResult Get([FromQuery] bool orderByDate = false, [FromQuery] string status = "")
        {
            try
            {
                List<Confession> confessions;

                if (string.IsNullOrEmpty(status))
                {
                    confessions = confessionRepository.Get(orderByDate);
                } else
                {
                    confessions = confessionRepository.Get(status, orderByDate);
                }

                return StatusCode(200, confessions);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/confession/abcxyz
        [HttpGet("{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        public IActionResult Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("ID is empty!");
                }
                Confession confession = confessionRepository.Get(id);
                return StatusCode(200, confession);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/confession
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/confession/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/confession/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
