using CrudDapper.Dtos;
using CrudDapper.Models;

namespace CrudDapper.Services;

public interface IUsuario
{
    Task<ResponseModel<List<ListarUsuarioDto>>> BuscarUsuarios();
    Task<ResponseModel<ListarUsuarioDto>> BuscarUsuarioId(int id);
    Task<ResponseModel<List<ListarUsuarioDto>>> CriarUsuario(CriarUsuarioDto criarUsuario);
    Task<ResponseModel<List<ListarUsuarioDto>>> EditarUsuario(EditarUsuarioDto editarUsuario);
    Task<ResponseModel<List<ListarUsuarioDto>>> RemoverUsuario(int id);
}
