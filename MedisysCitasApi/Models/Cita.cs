namespace MedisysCitasApi.Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public int ConsultorioId { get; set; }
        public DateTime FechaCita { get; set; }
        public string? Estado { get; set; }
    }
}
