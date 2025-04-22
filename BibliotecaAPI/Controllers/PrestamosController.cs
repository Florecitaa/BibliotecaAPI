using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using BibliotecaAPI.DataAccess;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly PrestamoData _data;

        public PrestamosController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _data = new PrestamoData(connectionString);
        }
        [HttpGet]
        public IActionResult Get() => Ok(_data.GetPrestamos());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var p = _data.GetPrestamo(id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Prestamo p)
        {
            _data.InsertarPrestamo(p);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Prestamo p)
        {
            p.IdPrestamo = id;
            _data.ActualizarPrestamo(p);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _data.EliminarPrestamo(id);
            return Ok();
        }
    }
}
