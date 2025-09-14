namespace GestionClientesMyM.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int ClienteProductoId { get; set; }
        public ClienteProducto? ClienteProducto { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal ImportePagado { get; set; }
    }
}
