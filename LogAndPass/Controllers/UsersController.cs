using Microsoft.AspNetCore.Mvc;
using LogAndPass.Tools;
using LogAndPass.Models;
using LogAndPass.Data;

namespace LogAndPass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> userLogin([FromBody] User user)
        {
            try
            {
                String password = Password.hashPassword(user.Password);
                var dbUser = _context.Users.Where(u => u.UserName == user.UserName && u.Password == password)
                    .Select(u => new
                    {
                        u.UserId,
                        u.UserName,
                        u.Active
                    }).FirstOrDefault();

                if (dbUser == null)
                {
                    return BadRequest("Username or password is incorrrect");
                }

                return Ok(dbUser);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> userRegistration([FromBody] User user)
        {

            try
            {
                var dbUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

                if (dbUser != null)
                {
                    return BadRequest("Username already exists");

                }
                user.Password = Password.hashPassword(user.Password);
                user.Active = 1;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
             return Ok("User is successfully registered");
        }

             
    }
}


       
    

