namespace MinhaApi.DTOs;

public class ProdutoDTO{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public List<CategoriaDTO> Categorias {get; set;} = new();
}