using System.ComponentModel.DataAnnotations;

namespace CrudDapper.Dtos;

public class CriarUsuarioDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public double Salario { get; set; }
    public string CPF { get; set; } = string.Empty;
    public bool Situação { get; set; }
    public string Senha { get; set; } = string.Empty;
}

