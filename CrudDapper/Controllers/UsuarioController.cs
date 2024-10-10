using CrudDapper.Dtos;
using CrudDapper.Models;
using CrudDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapper.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController: ControllerBase
{
    private readonly IUsuario _usuarioInterface;

    public UsuarioController(IUsuario usuarioInterface)
    {
        _usuarioInterface = usuarioInterface;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarUsuarios()
    {
        var usuarios = await _usuarioInterface.BuscarUsuarios( );
        if(usuarios.Status == false)
        {
            return NotFound(usuarios);
        }

        return Ok(usuarios);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarUsuarioPorId(int id)
    {
        var usuario = await _usuarioInterface.BuscarUsuarioId(id);
        if(usuario.Status == false)
        {
            return NotFound(usuario);
        }
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> CriarUsuario(CriarUsuarioDto criarUsuario)
    {
        var usuarios = await _usuarioInterface.CriarUsuario(criarUsuario);
        if(usuarios.Status == false)
        {
            return BadRequest(usuarios);
        }
        return Ok(usuarios);

    }

    [HttpPut]
    public async Task<IActionResult> EditarUsuario(EditarUsuarioDto editarUsuario)
    {
        var usuarios = await _usuarioInterface.EditarUsuario(editarUsuario);
        if(usuarios.Status == false)
        {
            return BadRequest(usuarios);
        }
        return Ok(usuarios);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoverUsuario(int id)
    {
        var usuarios = await _usuarioInterface.RemoverUsuario(id);
        if(usuarios.Status == false)
        {
            return BadRequest(usuarios);
        }
        return Ok(usuarios);
    }

    }
