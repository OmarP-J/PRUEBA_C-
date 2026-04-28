using CalculadoraPrestamosApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CalculadoraPrestamosApp.Data
{
    public interface IPrestamoRepository
    {
        decimal? ObtenerTasaPorEdad(int edad);
        bool ValidarPlazo(int meses);
        void InsertarLog(int edad, decimal monto, int meses, decimal valorCuota, string ip);
    }

    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly string _connectionString;

        public PrestamoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("ConnectionString 'DefaultConnection' not found.");
        }

        public decimal? ObtenerTasaPorEdad(int edad)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("usp_ObtenerTasaPorEdad", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Edad", edad);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? (decimal?)null : Convert.ToDecimal(result);
                }
            }
        }

        public bool ValidarPlazo(int meses)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("usp_ValidarPlazo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Meses", meses);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void InsertarLog(int edad, decimal monto, int meses, decimal valorCuota, string ip)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("usp_InsertarLogConsulta", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Edad", edad);
                    cmd.Parameters.AddWithValue("@Monto", monto);
                    cmd.Parameters.AddWithValue("@Meses", meses);
                    cmd.Parameters.AddWithValue("@ValorCuota", valorCuota);
                    cmd.Parameters.AddWithValue("@IP_Consulta", ip);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}