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

                confessions = confessionRepository.Get(orderByDate, status);

                return StatusCode(200, confessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/confession/abcxyz
        [HttpGet("{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        [ProducesResponseType(204)]
        public IActionResult Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("ID is empty!");
                }
                Confession confession = confessionRepository.Get(id);
                if (confession == null)
                {
                    return StatusCode(204);
                }
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/confession
        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        public IActionResult Post([FromBody] Confession confession)
        {
            try
            {
                confessionRepository.Create(confession);
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/confession/approve/5
        [HttpPut("approve/{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        public IActionResult Approve(string id)
        {
            try
            {
                string admin = "PhongNT"; // TODO Code
                Confession confession = confessionRepository.Approve(id, admin);
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/confession/reject/6
        [HttpPut("reject/{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        public IActionResult Reject(string id, [FromBody] string comment)
        {
            try
            {
                string admin = "PhongNT"; // TODO Code
                Confession confession = confessionRepository.Reject(id, admin, comment);
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
