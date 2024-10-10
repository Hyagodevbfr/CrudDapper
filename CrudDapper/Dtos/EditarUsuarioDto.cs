using System.ComponentModel.DataAnnotations;

namespace CrudDapper.Dtos;

public class EditarUsuarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public double Salario { get; set; }
    public string CPF { get; set; } = string.Empty;
    public bool Situação { get; set; }
}
