using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using System.Data.SqlClient;
using System.Numerics;
using BibliotecaAPI.Services;
using System.Reflection.Metadata.Ecma335;


namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : Controller
    {
        private readonly Services.LibroService _libroService;

        public LibrosController(Services.LibroService libroService)
        {
            _libroService = libroService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            var libros = await _libroService.ObtenerLibrosAsync();
            return Ok(libros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Libro>> GetLibroPorId(int id)
        {
            var libro = await _libroService.ObtenerLibroPorIdAsync(id);
            if (libro == null)
                return NotFound("Libro no disponible");
            return Ok(libro);

        }


        [HttpPost]
        public async Task<ActionResult> CrearLibro([FromBody] Libro libro)
        {
            await _libroService.CrearLibroAsync(libro);
            return Ok("Libro agregado");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarLibro(int id, [FromBody] Libro libro) {
            var resultado = await _libroService.ActualizarLibroAsync(id, libro);
            if (!resultado)
                return NotFound("Libro no disponible");
            return Ok(resultado);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarLibro(int id)
        {
            var resultado = await _libroService.EliminarLibroAsync(id);
            if (!resultado)
                return NotFound("Libro no disponible");
            return Ok(resultado);

        }
        
        
        
        
        
        
        
        
            
            





    }


}




    