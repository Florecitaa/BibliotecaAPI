using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly PrestamoService _prestamoService;  // Declara el servicio de préstamos
        // aqui esta el constructor de la clase 'PrestamoController'.
        public PrestamoController(PrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet] //este es un metodo para ver todos los prestamos en una lista
        public async Task<ActionResult<List<Prestamo>>> ObtenerPrestamos()
        { // Aqui esta el servicio de préstamos para hacer lista de todos los préstamos de forma asíncrona
            var prestamos = await _prestamoService.ObtenerPrestamosAsync();
            return Ok(prestamos);
            //  Al ser exitoso devuelve la lista de préstamos
        }

        //metodo para registrar prestamos
        [HttpPost]
        public async Task<ActionResult> RegistrarPrestamo([FromBody] Prestamo prestamo)
        { // usa el servicio de préstamos y busca registrar un  préstamo
          // usa '[FromBody]' por que utiliza los datos del préstamo vienen en el cuerpo de la petición.
            await _prestamoService.RegistrarPrestamoAsync(prestamo);
            return CreatedAtAction(nameof(ObtenerPrestamos), new { id = prestamo.Id }, prestamo);
            // Devuelve que se creó un nuevo prestamo 
            // 'CreatedAtAction' genera una URL para obtener el préstamo recién creado 
        }

        [HttpPut("{id}")] //metodo para actualizar un prestamo usando el id
        public async Task<ActionResult> ActualizarPrestamo(int id, [FromBody] Prestamo prestamo)
        { //aqui se llama al servicio de préstamos para actualizar un préstamo con el 'id'
            var actualizado = await _prestamoService.ActualizarPrestamoAsync(id, prestamo);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")] //metodo para borrar un prestamo usando el id
        public async Task<ActionResult> EliminarPrestamo(int id)
        {
            var eliminado = await _prestamoService.EliminarPrestamoAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
