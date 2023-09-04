using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingPriceUserManagementWebApp.Data;
using KingPriceUserManagementWebApp.Models;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        if (_dbContext.Users == null)
        {
            return NotFound();
        }
        return await _dbContext.Users.Include(u => u.UserGroups).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        if (_dbContext.Users == null)
        {
            return NotFound();
        }
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<User>>> AddUserAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserAsync), new { id = user.UserId }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        _dbContext.Entry(user).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool UserExists(long id)
    {
        return (_dbContext.Users?.Any(a => a.UserId == id)).GetValueOrDefault();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        if (_dbContext.Users == null)
        {
            return NotFound();
        }

        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetUserCountAsync()
    {
        try
        {
            var count = await _dbContext.Users.CountAsync();
            return Ok(count);
        }
        catch
        {
            return NoContent();
        }
    }

    [HttpGet("group-count")]
    public async Task<IActionResult> GetUsersPerGroupCountAsync()
    {
        try
        {
            var userCountsPerGroup = await _dbContext.UserGroups
                .GroupBy(ug => ug.GroupId)
                .Select(g => new
                {
                    GroupId = g.Key,
                    UserCount = g.Count()
                })
                .ToListAsync();

            return Ok(userCountsPerGroup);
        }
        catch
        {
            return NoContent();
        }
    }
}

