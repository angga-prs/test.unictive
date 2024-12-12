using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.unictive.Data;

namespace test.unictive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HobbyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HobbyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllHobbies()
        {
            var hobbies = await _context.Hobbies.ToListAsync();
            return Ok(hobbies);
        }
    }
}
