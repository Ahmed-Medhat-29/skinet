using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;

    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("testauth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return "secret stuff";
    }

    [HttpGet("NotFound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(42);

        if (thing == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok(thing);
    }

    [HttpGet("ServerError")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);

        var thingToReturn = thing.ToString();

        return Ok(thingToReturn);
    }

    [HttpGet("BadRequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("BadRequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return Ok();
    }
}