using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.DTOs;
using MinhaApi.Models;

namespace MinhaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase{
       private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context){
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllAsync(){
        var categorias = await _context.Categorias
            .AsNoTracking()
            .ToListAsync();
        var res = categorias.Select(c => new CategoriaDTO{
            Id = c.Id,
            Nome = c.Nome
        });

        return Ok(res);
    }

    [HttpGet("{id:int}", Name = "GetById")]
    public async Task<ActionResult<CategoriaDTO>> GetByIdAsync(int id){
        var Categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if(Categoria is null) return NotFound();

        var res = new CategoriaDTO{
            Id = Categoria.Id,
            Nome = Categoria.Nome
        };
            
            return Ok(res);
        }


    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> CreateAsync(CategoriaCreateDTO dto){

        if(!ModelState.IsValid) return BadRequest(ModelState);
    
    var categoria = new Categoria{
        Nome = dto.Nome,
    };

    _context.Categorias.Add(categoria);
    await _context.SaveChangesAsync();

    var res = new CategoriaDTO{
        Id = categoria.Id,
        Nome = categoria.Nome
    };

    return CreatedAtRoute("GetById", new{id = categoria.Id}, res);
}
}