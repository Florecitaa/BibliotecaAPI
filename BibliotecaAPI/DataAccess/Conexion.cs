using System.Data.SqlClient;

namespace BibliotecaAPI.DataAccess
{
    public class Conexion
    {
        private readonly string cadena = "Server=MSI\\UNIVERSIDAD;Database=ExamenII;Trusted_Connection=True;";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(cadena);
        }
    }
}
