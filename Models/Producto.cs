using System.ComponentModel.DataAnnotations.Schema;

namespace GestionClientesMyM.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioInicial { get; set; }

        public ICollection<ProductoVersion> ProductosVersiones { get; set; } = new List<ProductoVersion>();
        public ICollection<ClienteProducto> ClientesProductos { get; set; } = new List<ClienteProducto>();
    }
}
