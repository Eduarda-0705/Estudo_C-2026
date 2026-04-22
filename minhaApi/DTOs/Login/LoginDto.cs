using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs;

public class LoginDto{
    [Required(ErrorMessage = "Login é obrigatório")]
    public string Login { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Senha é obrigatório")]
    public string Senha { get; set; } = string.Empty;
}