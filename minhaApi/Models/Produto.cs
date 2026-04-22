namespace MinhaApi.Models;

public class Produto{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public ICollection<Categoria> Categorias {get; set;} = new List<Categoria>();
}