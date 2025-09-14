using System.ComponentModel.DataAnnotations.Schema;

namespace GestionClientesMyM.Models
{
    public class ClienteProducto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public int ProductoVersionId { get; set; }
        public ProductoVersion? ProductoVersion { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Importe { get; set; }
        public DateTime FechaInstalacion { get; set; }
        public int MesGratis { get; set; }
        public DateTime FechaProximoPago { get; set; }

        public ICollection<ClienteProductoExtra> ClientesProductosExtras { get; set; } = new List<ClienteProductoExtra>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
