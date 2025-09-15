namespace GrupoNRJ.Servicio.GestionCafe
{
    public class dtoInforme
    {

        public int idLote { get; set; }
        public string tipoGrano { get; set; }
        public string tipoTueste { get; set; }
        public int estadoActual { get; set; }
        public DateTime fechaUltimoCambio{ get; set; }
        public string situacion { get; set; }

    }
}
