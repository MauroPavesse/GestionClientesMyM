using System.ComponentModel.DataAnnotations.Schema;

namespace GestionClientesMyM.Models
{
    public class ProductoVersion
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostoInicial { get; set; }

        public Producto? Producto { get; set; }

        public ICollection<ClienteProducto> ClientesProductos { get; set; } = new List<ClienteProducto>();
    }
}
