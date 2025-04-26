using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly Services.UsuarioService _usuarioService; // Declara el servicio de usuarios

        public UsuarioController(UsuarioService usuarioService)
        { 
            _usuarioService = usuarioService; //se usa el servicio usuarioservice
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ObtenerUsuarios()
        { // usa el método 'ObtenerUsuariosAsync' del servicio para obtener una lista de todos los usuarios 
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        
        }

        [HttpGet("{id}")] //este método usa "api/Usuario/id" para obtener los usuarios por medio de el id 
        public async Task<ActionResult<Usuario>> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        
        [HttpPost("validar")]
        public async Task<ActionResult<Usuario>> ValidarUsuario([FromBody] LoginViewModel login)
        //Llama al método 'ValidarUsuarioAsync' del servicio y  verifica las credenciales del usuario (correo y clave) 
        // El atributo '[FromBody]' lo que significa es que los datos se esperan en el cuerpo.
        {
            var usuario = await _usuarioService.ValidarUsuarioAsync(login.Correo, login.Clave);
            if (usuario == null)
            {
                return Unauthorized(); // // Si las credenciales no son válidas, retorna una respuesta 'No autorizado'
            }
            return Ok(usuario);
        }
        

        [HttpPost]
        public async Task<ActionResult> CrearUsuario([FromBody] Usuario usuario)
        { //Llama al método 'CrearUsuarioAsync' del servicio para agregar un nuevo usuario a la base de datos
            await _usuarioService.CrearUsuarioAsync(usuario);
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuario)
        { //// Llama al método 'ActualizarUsuarioAsync' del servicio para actualizar la información de un usuario
          //// //específico (por su ID) con los datos proporcionados en el cuerpo de la petición 
            var actualizado = await _usuarioService.ActualizarUsuarioAsync(id, usuario);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")] //este es un metodo que utiliza el id para eliminar un usuario
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _usuarioService.EliminarUsuarioAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}
