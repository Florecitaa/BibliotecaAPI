using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using System.Data.SqlClient;
using System.Numerics;
using BibliotecaAPI.Services;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;


namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //la ruta para acceder a esto será "api/Libros"
    public class LibrosController : Controller
    {
        private readonly Services.LibroService _libroService; // Guarda el servicio para trabajar con los libros

        public LibrosController(Services.LibroService libroService)  // est es el controlador que conecta con el servicio de libros
        {
            _libroService = libroService;
        }
        [HttpGet]

        //metodo para imprimir los libros 
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            var libros = await _libroService.ObtenerLibrosAsync();
            return Ok(libros); // Aqui se imprimen los libros 
        }

        //metodo para buscar un libro por id
        [HttpGet("{id}")] //busca el libro de esta manera "api/Libros/1" ya que lo busca por id
        public async Task<ActionResult<Models.Libro>> GetLibroPorId(int id)
        {
            var libro = await _libroService.ObtenerLibroPorIdAsync(id);
            if (libro == null)
                return NotFound("Libro no disponible"); // Si no existe  avisa
            return Ok(libro);// Aqui se imprimen los libros que encontro segun el id

        }


        [HttpPost]
        public async Task<ActionResult> CrearLibro([FromBody] Libro libro) //metodo para crear un libro
        {
            await _libroService.CrearLibroAsync(libro);
            return Ok("Libro agregado"); //aqui se avisa que se agrego correctamente
        }

        [HttpPut("{id}")] //busca de esta manera "api/Libros/1" el libro primero para tomar la informacion del libro por la id y luego poder editarla

        public async Task<ActionResult> ActualizarLibro(int id, [FromBody] Libro libro) {
            var resultado = await _libroService.ActualizarLibroAsync(id, libro);
            if (!resultado)
                return NotFound("Libro no disponible"); //declara error
            return Ok(resultado); //imprime el resultado

        }

        [HttpDelete("{id}")] // Metodo para borrar, llama por medio del id
        public async Task<ActionResult> EliminarLibro(int id)
        {
            var resultado = await _libroService.EliminarLibroAsync(id);
            if (!resultado)
                return NotFound("Libro no disponible"); //el libro no existe o no esta disponible
            return Ok(resultado); //se avisa que se borro

        }
        
        
        
        
        
        
        
        
            
            





    }


}




    