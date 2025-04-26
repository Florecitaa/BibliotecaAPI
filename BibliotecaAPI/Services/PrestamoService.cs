using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using System.Data;
using System.Data.SqlClient;
using BibliotecaAPI.Services;


namespace BibliotecaAPI.Services
{
    public class PrestamoService : Controller
    {
        private readonly string _connectionString; // Guarda la información necesaria para conectarnos a nuestra base de datos.
        public PrestamoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");


        }
        public async Task<List<Prestamo>> ObtenerPrestamosAsync() //Aqui imprime todos los prestamos que existen en la base de datos
        {
            var prestamos = new List<Prestamo>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerPrestamos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var prestamo = new Prestamo
                            {
                                Id = (int)reader["Id"],
                                IdUsuario = (int)reader["IdUsuario"],
                                IdLibro = (int)reader["IdLibro"],
                                FechaDevolucionEsperada = (DateTime)reader["FechaDevolucionEsperada"],
                                FechaDevolucionReal = reader["FechaDevolucionReal"] as DateTime?,
                                Estado = reader["Estado"].ToString()
                            };
                            prestamos.Add(prestamo);
                        }
                    }
                }
            }

            return prestamos; //imprime los prestamos 
        }

        public async Task RegistrarPrestamoAsync(Prestamo prestamo) //Aqui se puede registrar un nuevo prestamo 
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RegistrarPrestamo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure; //registra segun estos parametros y utiliza un proceso almacenado de la base de datos
                    cmd.Parameters.AddWithValue("@IdUsuario", prestamo.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdLibro", prestamo.IdLibro);
                    cmd.Parameters.AddWithValue("@FechaDevolucionEsperada", prestamo.FechaDevolucionEsperada);
                    cmd.Parameters.AddWithValue("@Estado", prestamo.Estado);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<bool> ActualizarPrestamoAsync(int id, Prestamo prestamo) //Aqui se logran actualizar los datos del prestamo
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ActualizarPrestamo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;//utiliza un proceso almacenado de la base de datos
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FechaDevolucionReal", prestamo.FechaDevolucionReal);
                    cmd.Parameters.AddWithValue("@Estado", prestamo.Estado);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

        public async Task<bool> EliminarPrestamoAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarPrestamo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0; 
                }
            }
        }




    }
}
