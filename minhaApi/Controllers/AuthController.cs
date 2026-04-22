using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.DTOs;
using MinhaApi.Services;
using static BCrypt.Net.BCrypt;

namespace MinhaApi.Controllers;
[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext context, TokenService token){
        _context = context;
        _tokenService = token;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto){
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login == dto.Login);

        if (!Verify(dto.Senha, usuario.Senha)){
            return Unauthorized(new {message = "Usuário ou senha inválido"});
        }

        var token = _tokenService.GenerateToken(usuario.Login);

        return Ok(new {
            token,
            nome = usuario.Nome,
            usuario = usuario.Login
        });

    
    }
}