namespace MedisysCitasApi.Models
{
    public class HistorialConsulta
    {
        public int IdConsulta { get; set; }
        public int CitaId { get; set; }
        public string? Diagnostico { get; set; }
        public string? Receta { get; set; }
        public string? Notas { get; set; }
    }
}
