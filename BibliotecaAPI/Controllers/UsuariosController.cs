using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using BibliotecaAPI.DataAccess;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioData _data = new UsuarioData();

        [HttpGet]
        public IActionResult Get() => Ok(_data.GetUsuarios());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usuario = _data.GetUsuario(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            _data.InsertarUsuario(usuario);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            usuario.IdUsuario = id;
            _data.ActualizarUsuario(usuario);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _data.EliminarUsuario(id);
            return Ok();
        }
    }
}
