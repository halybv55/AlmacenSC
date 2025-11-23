namespace AlmacenSC.Core.Entities
{
    public class AlertaReabastecimiento
    {
        public int Id { get; set; }

        public int InventarioId { get; set; }
        public Inventario Inventario { get; set; }

        public DateTime FechaAlerta { get; set; } = DateTime.UtcNow;

        public bool Atendida { get; set; } = false;

        public string Mensaje { get; set; }
    }

}
