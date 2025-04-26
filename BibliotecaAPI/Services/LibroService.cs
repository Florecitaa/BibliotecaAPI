
using BibliotecaAPI.Models;
using System.Data;
using System.Data.SqlClient;
using BibliotecaAPI.Services;

namespace BibliotecaAPI.Services
{ // Esta clase, 'LibroService', es la que se encarga de toda la lógica relacionada con los libros
    public class LibroService
    {
        private readonly string _connectionString; // Guarda la información necesaria para conectarnos a nuestra base de datos.
        public LibroService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");


        }


        public async Task<List<Models.Libro>> ObtenerLibrosAsync() //Este método va a la base de datos y trae todos los libros que encuentre.
        {
            var libros = new List<Models.Libro>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerLibros", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) {
                            libros.Add(new Models.Libro {
                                Id = (int)reader["Id"],
                                Titulo = reader["Titulo"].ToString(),
                                Autor = reader["Autor"].ToString(),
                                Editorial = reader["Editorial"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                Anio = (int)reader["Anio"],
                                Categoria = reader["Categoria"].ToString(),
                                Existencias = (int)reader["Existencias"]
                            });
                        }




                    }
                }
            }

            return libros;
        }

        //  // Este método busca un libro específico por su ID en la base de datos.
        // El '?' en 'Models.Libro' es por que podría devolver un libro o nada (null).
        public async Task<Models.Libro?> ObtenerLibroPorIdAsync(int id)
        {
            Models.Libro libro = null;
            using (SqlConnection con = new SqlConnection(_connectionString)) {

                using (SqlCommand cmd = new SqlCommand("ObtenerLibros", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync()) {

                        while (await reader.ReadAsync()) {

                            if ((int)reader["Id"] == id) {

                                libro = new Models.Libro
                                {
                                    Id = (int)reader["Id"],
                                    Titulo = reader["Titulo"].ToString(),
                                    Autor = reader["Autor"].ToString(),
                                    Editorial = reader["Editorial"].ToString(),
                                    ISBN = reader["ISBN"].ToString(),
                                    Anio = (int)reader["Anio"],
                                    Categoria = reader["Categoria"].ToString(),
                                    Existencias = (int)reader["Existencias"]

                                };
                                break;



                            }
                        }



                    }

                }

            }
            return libro;

        }

        // Este método toma un objeto 'Libro' y lo guarda en la base de datos
        public async Task CrearLibroAsync(Models.Libro libro)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("InsertarLibro", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@ISBN",libro.ISBN);
                    cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                    cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                    cmd.Parameters.AddWithValue("@Existencias", libro.Existencias);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

            }
        }
        //actualiza el libro ya que reescribe los parametros 
        public async Task<bool> ActualizarLibroAsync(int id, Models.Libro libro)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("ActualizarLibro", con))
                {
                  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", libro.Id);
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                    cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                    cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                    cmd.Parameters.AddWithValue("@Existencias", libro.Existencias);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }

            } 
        }

        public async Task<bool> EliminarLibroAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("EliminarLibro", con))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;

                }
            }
        }








    }
}

