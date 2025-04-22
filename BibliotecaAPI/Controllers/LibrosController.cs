using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using BibliotecaAPI.DataAccess;
using System.Data.SqlClient;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly Conexion _conexion = new Conexion();

        [HttpGet]
        public IActionResult Get()
        {
            List<Libro> libros = new List<Libro>();
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Libros", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    libros.Add(new Libro
                    {
                        IdLibro = Convert.ToInt32(reader["IdLibro"]),
                        Titulo = reader["Titulo"].ToString(),
                        Autor = reader["Autor"].ToString(),
                        Editorial = reader["Editorial"].ToString(),
                        ISBN = reader["ISBN"].ToString(),
                        Anio = Convert.ToInt32(reader["Anio"]),
                        Categoria = reader["Categoria"].ToString(),
                        Stock = Convert.ToInt32(reader["Stock"])
                    });
                }
            }
            return Ok(libros);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Libro libro)
        {
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Libros (Titulo, Autor, Editorial, ISBN, Anio, Categoria, Stock) VALUES (@Titulo, @Autor, @Editorial, @ISBN, @Anio, @Categoria, @Stock)", conn);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                cmd.ExecuteNonQuery();
            }
            return Ok("Libro agregado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Libro libro)
        {
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, Editorial = @Editorial, ISBN = @ISBN, Anio = @Anio, Categoria = @Categoria, Stock = @Stock WHERE IdLibro = @IdLibro", conn);
                cmd.Parameters.AddWithValue("@IdLibro", id);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                cmd.Parameters.AddWithValue("@Stock", libro.Stock);

                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas == 0)
                {
                    return NotFound($"No se encontró el libro con ID {id}");
                }
            }
            return Ok("Libro actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Libros WHERE IdLibro = @IdLibro", conn);
                cmd.Parameters.AddWithValue("@IdLibro", id);

                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas == 0)
                {
                    return NotFound($"No se encontró el libro con ID {id}");
                }
            }
            return Ok("Libro eliminado correctamente");
        }


    }
}
