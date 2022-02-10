using DAOLibrary.Repository;
using DTOLibrary;
using HOVStoryUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

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
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(List<Confession>), 200)]
        public IActionResult Get([FromQuery] bool orderByDate = false, [FromQuery] string status = "")
        {
            try
            {
                List<Confession> confessions;
                if (status != "A")
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        Utils.Logger.Log("Authentication", $"Error at ConfessionController: User is not authenticated and try to get confessions with status {status}", User);
                        return StatusCode(401);
                    }
                }
                confessions = confessionRepository.Get(orderByDate, status);
                foreach (var confession in confessions)
                {
                    confession.CreatedAt = Utils.Datetime.ConvertToVn(confession.CreatedAt);
                    confession.ModifiedOn = Utils.Datetime.ConvertToVn(confession.ModifiedOn);
                }

                return StatusCode(200, confessions);
            }
            catch (Exception ex)
            {
                Utils.Logger.Log("Error", ex.Message, User);
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
                confession.CreatedAt = Utils.Datetime.ConvertToVn(confession.CreatedAt);
                confession.ModifiedOn = Utils.Datetime.ConvertToVn(confession.ModifiedOn);
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                Utils.Logger.Log("Error", ex.Message, User);
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/confession with Body: array of Confession id
        [HttpPost("my-confess")]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(List<Confession>), 200)]
        public IActionResult Get([FromBody] string[] confessionIds)
        {
            List<Confession> confessions = new List<Confession>();
            try
            {
                if (confessionIds.Length > 0)
                {
                    confessions = confessionRepository.Get(orderByDate: true, confessionIds: confessionIds);
                    foreach (var confession in confessions)
                    {
                        confession.CreatedAt = Utils.Datetime.ConvertToVn(confession.CreatedAt);
                        confession.ModifiedOn = Utils.Datetime.ConvertToVn(confession.ModifiedOn);
                    }
                }
                return StatusCode(200, confessions);
            }
            catch (Exception ex)
            {
                Utils.Logger.Log("Error", ex.Message, User);
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
                Utils.Logger.Log("Error", ex.Message, User);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/confession/approve/?id=asdasd
        [HttpPost("approve")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        [Authorize]
        public IActionResult Approve([FromQuery] string id)
        {
            try
            {
                string admin = getOperatorName();
                Confession confession = confessionRepository.Approve(id, admin);
                confession.CreatedAt = Utils.Datetime.ConvertToVn(DateTime.SpecifyKind(confession.CreatedAt, DateTimeKind.Utc));
                confession.ModifiedOn = Utils.Datetime.ConvertToVn(DateTime.SpecifyKind(confession.ModifiedOn, DateTimeKind.Utc));
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                Utils.Logger.Log("Error", ex.Message, User);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/confession/reject/?id=asdad
        [HttpPost("reject")]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Confession), 200)]
        [Authorize]
        public IActionResult Reject([FromQuery] string id, [FromQuery] string reason)
        {
            try
            {
                string admin = getOperatorName();
                Confession confession = confessionRepository.Reject(id, admin, reason);
                confession.CreatedAt = Utils.Datetime.ConvertToVn(DateTime.SpecifyKind(confession.CreatedAt, DateTimeKind.Utc));
                confession.ModifiedOn = Utils.Datetime.ConvertToVn(DateTime.SpecifyKind(confession.ModifiedOn, DateTimeKind.Utc));
                return StatusCode(200, confession);
            }
            catch (Exception ex)
            {
                Utils.Logger.Log("Error", ex.Message, User);
                return StatusCode(500, ex.Message);
            }
        }

        private string getOperatorName()
        {
            string operatorName = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                var claims = User.Claims;
                operatorName = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value;
            }

            return operatorName;
        }
    }

    public class AdminConfess {
        public string Id { get; set; } 
        public string Reason { get; set; }    
    }
}
