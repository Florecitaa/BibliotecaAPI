using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using BibliotecaAPI.DataAccess;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradoresController : ControllerBase
    {
        private readonly AdministradorData _data;

        public AdministradoresController(IConfiguration config)
        {
            _data = new AdministradorData(config);
        }

        [HttpGet]
        public IActionResult Get() => Ok(_data.GetAdministradores());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var admin = _data.GetAdministrador(id);
            return admin == null ? NotFound() : Ok(admin);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Administrador admin)
        {
            _data.InsertarAdministrador(admin);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Administrador admin)
        {
            admin.IdAdmin = id;
            _data.ActualizarAdministrador(admin);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _data.EliminarAdministrador(id);
            return Ok();
        }
    }
}
