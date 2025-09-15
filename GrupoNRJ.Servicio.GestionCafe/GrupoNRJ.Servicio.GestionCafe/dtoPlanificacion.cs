namespace GrupoNRJ.Servicio.GestionCafe
{
    public class dtoPlanificacion
    {
        public int IdPlani { get; set; }
        public int IdLote { get; set; }
        public int IdEstado { get; set; }
        public DateTime? FechaEstimada { get; set; }
        public DateTime? FechaFinEstimada { get; set; }

    }
}