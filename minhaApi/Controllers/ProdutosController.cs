using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.DTOs;
using MinhaApi.Models;

namespace MinhaApi.Controllers;
//Api/Produtos

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class ProdutosController : ControllerBase{

    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context){
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllAsync(){
        var res = await _context.Produtos
            .Include(p => p.Categorias)
            .AsNoTracking()
            .Select(p => new ProdutoDTO{
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Categorias = p.Categorias
                    .Select(c => new CategoriaDTO{
                        Id = c.Id, Nome = c.Nome}).ToList()})
            .ToListAsync();
            return Ok(res);
    }

    [HttpGet("{id:int}", Name = "GetProdById")]
    public async Task<ActionResult<ProdutoDTO>> GetByIdAsync(int id){
        var produto = await _context.Produtos
            .Include(p => p.Categorias)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if(produto is null) return NotFound();

        var res = new ProdutoDTO{
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Categorias = produto.Categorias
                .Select(c => new CategoriaDTO{
                    Id = c.Id, Nome = c.Nome}).ToList()};
            
            return Ok(res);
        }


        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> CreateAsync(ProdutoCreateDTO dto){
        
        var categorias = await _context.Categorias
            .Where(c => dto.CategoriasIds.Contains(c.Id))
            .ToListAsync();

        var produto = new Produto{
            Nome = dto.Nome,
            Preco = dto.Preco,
            Categorias = categorias
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetProdById", new{id = produto.Id}, new {id = produto.Id});
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, ProdutoUpdateDTO dto){
        var produto = await _context.Produtos
            .Include(p => p.Categorias)
            .FirstOrDefaultAsync(p => p.Id == id);

        if(produto is null) return NotFound();

        produto.Nome = dto.Nome;
        produto.Preco = dto.Preco;

        var categorias = await _context.Categorias
            .Where(c => dto.CategoriasIds.Contains(c.Id))
            .ToListAsync();
        produto.Categorias = categorias;

        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id){
        var produto = await _context.Produtos.FindAsync(id);

        if (produto is null) return NotFound();
         
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        
        return NoContent();
        
    }
}