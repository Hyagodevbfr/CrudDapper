using System.ComponentModel.DataAnnotations;

namespace CrudDapper.Dtos;

public class ListarUsuarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public double Salario { get; set; }
    public bool Situação { get; set; }
}
