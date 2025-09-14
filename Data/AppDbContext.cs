using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ClienteProducto> ClientesProductos { get; set; }
    public DbSet<ClienteProductoExtra> ClientesProductosExtras { get; set; }
    public DbSet<Extra> Extras { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ProductoVersion> ProductosVersiones { get; set; }
}
