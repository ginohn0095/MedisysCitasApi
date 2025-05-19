namespace MedisysCitasApi.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string ?Nombre { get; set; }
        public string ?Correo { get; set; }
        public string ?Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
    }
}
