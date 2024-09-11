using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroBackAPI.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace CadastroBackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDadosController : ControllerBase
    {
        private readonly UserContext _context;

        public UserDadosController(UserContext context)
        {
            _context = context;
        }

        // GET: api/UserDados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDados>>> GetUserDados()
        {
            return await _context.UserDados.ToListAsync();
        }

        // GET: api/UserDados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDados>> GetUserDados(int id)
        {
            var userDados = await _context.UserDados.FindAsync(id);

            if (userDados == null)
            {
                return NotFound();
            }

            return userDados;
        }

        // GET: Serve para pegar o email e verificar se o email ja existe no banco de dados
        [HttpGet("email/{email}")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var user = await _context.UserDados.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return Ok(new { exists = true });
            }

            return Ok(new { exists = false });
        }

        // GET: Serve para pegar a senha e verificar se está igual a que ja existe no banco de dados
        [HttpGet("password/{password}")]
        public async Task<IActionResult> CheckPassword(string password)
        {
            var user = await _context.UserDados.FirstOrDefaultAsync(u => u.Password == password);

            if (user != null)
            {
                return Ok(new { exists = true });
            }

            return Ok(new { exists = false });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            //FirstOrDefaultAsync() para encontrar um usuário cujo e-mail e senha sejam correspondentes

            var user = await _context.UserDados
                        .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Email ou senha incorretos" });
            }

            // Sucesso no login
            return Ok(new { message = "Login efetuado com sucesso", userId = user.UserId });
        }

        // PUT: api/UserDados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDados(int id, UserDados userDados)
        {
            if (id != userDados.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userDados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDadosExists(id))
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

        // POST: api/UserDados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDados>> PostUserDados(UserDados userDados)
        {
            _context.UserDados.Add(userDados);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserDados), new { id = userDados.UserId }, userDados);
        }

        // DELETE: api/UserDados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDados(int id)
        {
            var userDados = await _context.UserDados.FindAsync(id);
            if (userDados == null)
            {
                return NotFound();
            }

            _context.UserDados.Remove(userDados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDadosExists(int id)
        {
            return _context.UserDados.Any(e => e.UserId == id);
        }
    }
}
