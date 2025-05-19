using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MedisysCitasApi.Models;
using System.Data;

namespace MedisysCitasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CitasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/citas
        [HttpPost]
        public async Task<IActionResult> CrearCita([FromBody] Cita cita)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("InsertarCita", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PacienteId", cita.PacienteId);
            command.Parameters.AddWithValue("@DoctorId", cita.DoctorId);
            command.Parameters.AddWithValue("@ConsultorioId", cita.ConsultorioId);
            command.Parameters.AddWithValue("@FechaCita", cita.FechaCita);
            command.Parameters.AddWithValue("@Estado", cita.Estado);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return Ok(new { message = "Cita creada correctamente" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/citas
        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasCitas()
        {
            var citas = new List<Cita>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("SELECT * FROM Citas", connection);

            try
            {
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    citas.Add(new Cita
                    {
                        IdCita = reader.GetInt32(reader.GetOrdinal("IdCita")),
                        PacienteId = reader.GetInt32(reader.GetOrdinal("PacienteId")),
                        DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                        ConsultorioId = reader.GetInt32(reader.GetOrdinal("ConsultorioId")),
                        FechaCita = reader.GetDateTime(reader.GetOrdinal("FechaCita")),
                        Estado = reader.GetString(reader.GetOrdinal("Estado"))
                    });
                }

                return Ok(citas);
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/citas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCita(int id, [FromBody] Cita cita)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("ActualizarCita", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdCita", id);
            command.Parameters.AddWithValue("@NuevaFecha", cita.FechaCita);
            command.Parameters.AddWithValue("@NuevoEstado", cita.Estado);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return Ok(new { message = "Cita actualizada correctamente" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/citas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCita(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("EliminarCita", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdCita", id);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return Ok(new { message = "Cita eliminada correctamente" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
