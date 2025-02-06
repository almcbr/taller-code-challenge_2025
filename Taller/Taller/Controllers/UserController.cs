using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Taller.Models;

namespace Taller.Controllers
{

    public class UserController : Controller
    {
        private readonly TallerDbContext _context;

        public UserController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                //EF already takes care of SQL Injection preventing.
                //A better approach would be to put this data access code in a separate layer,
                //but for lack of time I couldn't do that.
                var user = await _context.Users
                    .Where(u => u.Name == username)
                    .Select(u => new { Name = u.Name })
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    return View("UserView", user); 
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while processing your request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
