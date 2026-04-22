using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs;

public class ProdutoCreateDTO{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(120, ErrorMessage = "O nome pode ter no máximo 120 caracteres")]
    public string Nome { get; set; } = string.Empty;
    [Range(typeof(decimal), "0", "999999999999999999,99", ErrorMessage ="O preço precisa ser Positivo")]
    public decimal Preco { get; set; }
    public List<int> CategoriasIds { get; set; } = new();
}