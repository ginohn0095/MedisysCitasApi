namespace MedisysCitasApi.Models
{
    public class RegistroAccion
    {
        public int IdRegistro { get; set; }
        public string? Accion { get; set; } 
        public DateTime FechaAccion { get; set; }
        public string? Tabla { get; set; }
        public int? UsuarioId { get; set; } 
        public string? Descripcion { get; set; }
    }
}
