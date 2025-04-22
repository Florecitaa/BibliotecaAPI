using System.Data.SqlClient;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.DataAccess
{
    public class PrestamoData
    {
        private readonly string _connectionString;

        public PrestamoData(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Prestamo> GetPrestamos()
        {
            var lista = new List<Prestamo>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Prestamos", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Prestamo
                    {
                        IdPrestamo = (int)reader["IdPrestamo"],
                        IdUsuario = (int)reader["IdUsuario"],
                        IdLibro = (int)reader["IdLibro"],
                        FechaPrestamo = (DateTime)reader["FechaPrestamo"],
                        FechaDevolucionEsperada = (DateTime)reader["FechaDevolucionEsperada"],
                        FechaDevolucionReal = reader["FechaDevolucionReal"] as DateTime?,
                        Estado = reader["Estado"].ToString()
                    });
                }
            }
            return lista;
        }

        public Prestamo GetPrestamo(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Prestamos WHERE IdPrestamo = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Prestamo
                    {
                        IdPrestamo = (int)reader["IdPrestamo"],
                        IdUsuario = (int)reader["IdUsuario"],
                        IdLibro = (int)reader["IdLibro"],
                        FechaPrestamo = (DateTime)reader["FechaPrestamo"],
                        FechaDevolucionEsperada = (DateTime)reader["FechaDevolucionEsperada"],
                        FechaDevolucionReal = reader["FechaDevolucionReal"] as DateTime?,
                        Estado = reader["Estado"].ToString()
                    };
                }
            }
            return null;
        }

        public void InsertarPrestamo(Prestamo prestamo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Prestamos (IdUsuario, IdLibro, FechaPrestamo, FechaDevolucionEsperada, FechaDevolucionReal, Estado) VALUES (@IdUsuario, @IdLibro, @FechaPrestamo, @FechaDevolucionEsperada, @FechaDevolucionReal, @Estado)", conn);
                cmd.Parameters.AddWithValue("@IdUsuario", prestamo.IdUsuario);
                cmd.Parameters.AddWithValue("@IdLibro", prestamo.IdLibro);
                cmd.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
                cmd.Parameters.AddWithValue("@FechaDevolucionEsperada", prestamo.FechaDevolucionEsperada);
                cmd.Parameters.AddWithValue("@FechaDevolucionReal", (object?)prestamo.FechaDevolucionReal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", prestamo.Estado);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarPrestamo(Prestamo prestamo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Prestamos SET IdUsuario=@IdUsuario, IdLibro=@IdLibro, FechaPrestamo=@FechaPrestamo, FechaDevolucionEsperada=@FechaDevolucionEsperada, FechaDevolucionReal=@FechaDevolucionReal, Estado=@Estado WHERE IdPrestamo=@IdPrestamo", conn);
                cmd.Parameters.AddWithValue("@IdPrestamo", prestamo.IdPrestamo);
                cmd.Parameters.AddWithValue("@IdUsuario", prestamo.IdUsuario);
                cmd.Parameters.AddWithValue("@IdLibro", prestamo.IdLibro);
                cmd.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
                cmd.Parameters.AddWithValue("@FechaDevolucionEsperada", prestamo.FechaDevolucionEsperada);
                cmd.Parameters.AddWithValue("@FechaDevolucionReal", (object?)prestamo.FechaDevolucionReal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", prestamo.Estado);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarPrestamo(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Prestamos WHERE IdPrestamo = @IdPrestamo", conn);
                cmd.Parameters.AddWithValue("@IdPrestamo", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
