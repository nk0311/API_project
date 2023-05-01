using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyFirstAPIDBContext _context;

        public UserController(MyFirstAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/C
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/C/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetUser(int id)
        {
          //if (_context.Users == null)
          //{
          //    return NotFound();
          //}
            var user = await _context.Users.FindAsync(id);
            var response = new Response();

           

            if (user != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Successful retrieval";
                response.users.Add(user);
            }
            else
            {
                response.statusCode = 400;
                response.statusDescription = "No user found";
                return NotFound(response);

            }

            return response;

            //if (user == null)
            //{
            //    return NotFound();
            //}

            //return user;


        }

        // PUT: api/C/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var response = new Response();
            
            if (id != user.UserId)
            {
                response.statusCode = 400;
                response.statusDescription = "Invalid user id";
                
                return BadRequest(response);
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    response.statusCode = 404;
                    response.statusDescription = "no user associated witht the id";
                    return NotFound(response);
                }
                else
                {
                    throw;
                }

            }

            response.statusCode = 204;
            response.statusDescription = "user has been successfully updated";
            return Ok(response);

            return NoContent();
        }

        // POST: api/C
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

          var response = new Response();
          if (_context.Users == null)
          {
              return Problem("Entity set 'MyFirstAPIDBContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.statusCode = 201;
            response.statusDescription = "User was successfully created";

            
            return CreatedAtAction("GetUser", new { id = user.UserId }, response);
        }

        // DELETE: api/C/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = new Response();
            if (_context.Users == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Unsuccessful deletion, no user found";
                return NotFound(response);
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Unsuccessful, no user associated with that id";
                return NotFound(response);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            response.statusCode = 204;
            response.statusDescription = "Successful deletion";
            return Ok(response);
            //return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
