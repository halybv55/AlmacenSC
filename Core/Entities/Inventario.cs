namespace AlmacenSC.Core.Entities
{
    public class Inventario
    {
        public int Id { get; set; }

        public int ProductoEntradaId { get; set; }
        public ProductoEntrada ProductoEntrada { get; set; }

        public int StockActual { get; set; }
    }

}
