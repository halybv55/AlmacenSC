namespace AlmacenSC.Core.DTOs
{
    public class AlertaReabastecimientoDto
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";

        public int StockActual { get; set; }
        public int StockMinimo { get; set; }

        public string Estado { get; set; } = "";
        public DateTime FechaNotificacion { get; set; }
    }

}
