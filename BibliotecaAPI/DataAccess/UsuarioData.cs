using System.Data.SqlClient;
using System.Data;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.DataAccess
{
    public class UsuarioData
    {
        private readonly string _connectionString = "Server=MSI\\UNIVERSIDAD;Database=ExamenII;Trusted_Connection=True;";

        public List<Usuario> GetUsuarios()
        {
            var lista = new List<Usuario>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuarios", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        IdUsuario = (int)reader["IdUsuario"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        TipoUsuario = reader["TipoUsuario"].ToString()
                    });
                }
            }
            return lista;
        }

        public Usuario GetUsuario(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuarios WHERE IdUsuario = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = (int)reader["IdUsuario"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        TipoUsuario = reader["TipoUsuario"].ToString()
                    };
                }
            }
            return null;
        }

        public void InsertarUsuario(Usuario usuario)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Usuarios (Nombre, Apellido, Correo, Telefono, TipoUsuario) VALUES (@Nombre, @Apellido, @Correo, @Telefono, @TipoUsuario)", conn);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Usuarios SET Nombre=@Nombre, Apellido=@Apellido, Correo=@Correo, Telefono=@Telefono, TipoUsuario=@TipoUsuario WHERE IdUsuario=@IdUsuario", conn);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario", conn);
                cmd.Parameters.AddWithValue("@IdUsuario", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
