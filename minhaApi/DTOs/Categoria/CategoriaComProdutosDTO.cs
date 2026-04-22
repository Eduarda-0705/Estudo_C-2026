namespace MinhaApi.DTOs;

public class CategoriaComProdutosDTO{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public List<ProdutoDTO> Produtos { get; set; } = new();
}