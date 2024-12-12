using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.unictive.Data;
using test.unictive.Models;

namespace test.unictive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest("Invalid data.");

            var user = new User
            {
                Name = userDto.Name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (userDto.Hobbies != null && userDto.Hobbies.Any())
            {
                var userHobbies = userDto.Hobbies.Select(hobbyId => new UserHobby
                {
                    UserId = user.Id,
                    HobbyId = hobbyId
                }).ToList();

                _context.User_Hobbies.AddRange(userHobbies);
                await _context.SaveChangesAsync();
            }

            return Ok(user);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var user = await _context.Users.Include(u => u.UserHobbies)
                                            .ThenInclude(uh => uh.Hobby)
                                            .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            user.Name = userDto.Name;

            var existingHobbies = _context.User_Hobbies.Where(uh => uh.UserId == id);
            _context.User_Hobbies.RemoveRange(existingHobbies);

            if (userDto.Hobbies != null && userDto.Hobbies.Any())
            {
                var userHobbies = userDto.Hobbies.Select(hobbyId => new UserHobby
                {
                    UserId = user.Id,
                    HobbyId = hobbyId
                }).ToList();

                _context.User_Hobbies.AddRange(userHobbies);
            }

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.Include(u => u.UserHobbies).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            _context.User_Hobbies.RemoveRange(user.UserHobbies);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users
                                     .Include(u => u.UserHobbies)
                                     .ThenInclude(uh => uh.Hobby)
                                     .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDtoModel
            {
                Id = user.Id,
                Name = user.Name,
                UserHobbies = user.UserHobbies.Select(uh => new UserHobbyDto
                {
                    HobbyId = uh.HobbyId,
                    HobbyName = uh.Hobby.Name
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.UserHobbies)
                                            .ThenInclude(uh => uh.Hobby)
                                            .ToListAsync();

            return Ok(users);
        }
    }
}
