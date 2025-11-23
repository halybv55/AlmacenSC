namespace AlmacenSC.Core.DTOs
{
    public class ProductoSalidaDto
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int CantidadSolicitada { get; set; }

        public DateTime FechaSalida { get; set; }
    }

}
