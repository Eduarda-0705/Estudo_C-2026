using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs;

public class CategoriaCreateDTO{
    [Required(ErrorMessage = "O nome não pode ser vazio")]
    [MaxLength(80, ErrorMessage = "O nome não pode ter mais de 80 caracteres")]
    public string Nome { get; set; } = string.Empty;
}