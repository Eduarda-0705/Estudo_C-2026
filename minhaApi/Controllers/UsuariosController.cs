using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Data;
using MinhaApi.DTOs;
using MinhaApi.Models;

// dotnet add package BCrypt.Net-Next
using static BCrypt.Net.BCrypt;

namespace MinhaApi.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase{

private readonly AppDbContext _context;
public UsuariosController(AppDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UsuarioCreateDto dto){
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (dto.Senha != dto.ConfirmarSenha){
            return BadRequest(new {message = "As senhas não conferem"});
        }

        string senhaHash = HashPassword(dto.Senha);

        var usuario = new Usuario        {
            Nome = dto.Nome,
            Login = dto.Login,
            Senha = senhaHash  
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetUserById", new {id = usuario.Id}, new UsuarioDto{
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login,
        });
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetByIdAsync(int id){
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null){
            return NotFound();
        }

        return Ok(new UsuarioDto{
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        });
    }
}