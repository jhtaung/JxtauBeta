using Server.Data;
using Server.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class BuggyController : BaseController
    {
        
        private readonly AppealContext _context;
        public BuggyController(AppealContext context)
        {
            _context = context;
        }

        /*
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }
        */
        
        [HttpGet("not-found")]
        public ActionResult<Appeal> GetNotFound()
        {
            var thing = _context.Appeals.Find(-1);
            if (thing == null) return NotFound();
            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Appeals.Find(-1);
            var thingToReturn = thing!.ToString();
            return thingToReturn!;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}