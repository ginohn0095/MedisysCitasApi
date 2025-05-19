namespace MedisysCitasApi.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public bool Activo { get; set; }
    }
}
