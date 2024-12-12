using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using test.unictive.Models;
using test.unictive.Data;
using Microsoft.EntityFrameworkCore;

public class UserModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public UserModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<User> Users { get; set; }
    public IList<Hobby> AllHobbies { get; set; }

    public async Task OnGetAsync()
    {
        Users = await _context.Users
            .Include(u => u.UserHobbies)
            .ThenInclude(uh => uh.Hobby)
            .ToListAsync();

        AllHobbies = await _context.Hobbies.ToListAsync();
    }


}
