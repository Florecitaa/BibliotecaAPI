using System.Data.SqlClient;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.DataAccess
{
    public class AdministradorData
    {
        private readonly string _connectionString;

        public AdministradorData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Administrador> GetAdministradores()
        {
            var lista = new List<Administrador>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Administradores", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Administrador
                    {
                        IdAdmin = (int)reader["IdAdmin"],
                        Usuario = reader["Usuario"].ToString(),
                        Contrasena = reader["Contrasena"].ToString()
                    });
                }
            }
            return lista;
        }

        public Administrador GetAdministrador(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Administradores WHERE IdAdmin = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Administrador
                    {
                        IdAdmin = (int)reader["IdAdmin"],
                        Usuario = reader["Usuario"].ToString(),
                        Contrasena = reader["Contrasena"].ToString()
                    };
                }
            }
            return null;
        }

        public void InsertarAdministrador(Administrador admin)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Administradores (Usuario, Contrasena) VALUES (@Usuario, @Contrasena)", conn);
                cmd.Parameters.AddWithValue("@Usuario", admin.Usuario);
                cmd.Parameters.AddWithValue("@Contrasena", admin.Contrasena);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarAdministrador(Administrador admin)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Administradores SET Usuario = @Usuario, Contrasena = @Contrasena WHERE IdAdmin = @IdAdmin", conn);
                cmd.Parameters.AddWithValue("@IdAdmin", admin.IdAdmin);
                cmd.Parameters.AddWithValue("@Usuario", admin.Usuario);
                cmd.Parameters.AddWithValue("@Contrasena", admin.Contrasena);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarAdministrador(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Administradores WHERE IdAdmin = @IdAdmin", conn);
                cmd.Parameters.AddWithValue("@IdAdmin", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
