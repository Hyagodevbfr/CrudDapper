using AutoMapper;
using CrudDapper.Dtos;
using CrudDapper.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CrudDapper.Services;

public class UsuarioService: IUsuario
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UsuarioService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ListarUsuarioDto>> BuscarUsuarioId(int id)
    {
        ResponseModel<ListarUsuarioDto> response = new( );
        await using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from Usuarios where id = @Id", new {Id = id});
            if(usuarioBanco is null)
            {
                response.Mensagem = "Nenhum usuário localizado";
                response.Status = false;
                return response;
            }

            var usuarioMapeado = _mapper.Map<ListarUsuarioDto>(usuarioBanco);
            response.Dados = usuarioMapeado;
            response.Mensagem = "Usuário localizado com sucesso.";

            return response;
        }
    }

    public async Task<ResponseModel<List<ListarUsuarioDto>>> BuscarUsuarios()
    {
        ResponseModel<List<ListarUsuarioDto>> response = new();

        await using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuariosBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");

            if(!usuariosBanco.Any())
            {
                response.Mensagem = "Nenhum usuário localizado";
                response.Status = false;
                return response;
            }

            var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuariosBanco);
            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários localizados com sucesso.";

            return response;
            
        }
    }

    public async Task<ResponseModel<List<ListarUsuarioDto>>> CriarUsuario(CriarUsuarioDto criarUsuario)
    {
        ResponseModel<List<ListarUsuarioDto>> response = new( );
        await using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuariosBanco = await connection.ExecuteAsync("insert into Usuarios (NomeCompleto, Email, Cargo, Salario, CPF, Situacao, Senha) values (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Situação, @Senha)", criarUsuario);

            if(usuariosBanco == 0)
            {
                response.Mensagem = "Ocorreu um erro ao criar o usuário";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);

            var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuarios);
            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuarios listados com sucesso.";
        }

        return response;


    }

    public async Task<ResponseModel<List<ListarUsuarioDto>>> EditarUsuario(EditarUsuarioDto editarUsuario)
    {
        ResponseModel<List<ListarUsuarioDto>> response = new( );
        await using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuariosBanco = await connection.ExecuteAsync("UPDATE Usuarios set NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, Salario = @Salario, CPF = @CPF, Situacao = @Situação WHERE Id = @Id", editarUsuario);

            if(usuariosBanco == 0)
            {
                response.Mensagem = "Erro ao tentar atualizar.";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);
            var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuarios);
            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários listados com sucesso.";
            return response;

        }
    }
    public async Task<ResponseModel<List<ListarUsuarioDto>>> RemoverUsuario(int id)
    {
        ResponseModel<List<ListarUsuarioDto>> response = new( );
        await using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuariosBanco = await connection.ExecuteAsync("DELETE from Usuarios where id = @Id",new { Id = id });
            if(usuariosBanco == 0)
            {
                response.Mensagem = "Usuário não existe ou já foi excluído.";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);
            var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuarios);
            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários listados com sucesso.";
        }
        return response;
    }


    private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
    {
        return await connection.QueryAsync<Usuario>("select * from Usuarios");
    }

}
