using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.Productos
{
    public class ProductoService
    {
        private readonly AppDbContext appDbContext;

        public ProductoService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Producto>> ObtenerDatosCombo()
        {
            var productos = await appDbContext.Productos.ToListAsync();
            return productos;
        }

        public async Task<List<Producto>> ObtenerTodo()
        {
            var productos = await appDbContext.Productos
                .Include(x => x.ProductosVersiones)
                .ToListAsync();
            return productos;
        }

        public async Task<Producto> ObtenerPorId(int productoId)
        {
            var producto = await appDbContext.Productos
                .Include(x => x.ProductosVersiones)
                .Where(x => x.Id == productoId)
                .FirstAsync();
            return producto;
        }

        public async Task<Producto> Crear(Producto producto)
        {
            try
            {
                var nuevoProducto = await appDbContext.Productos.AddAsync(producto);
                await appDbContext.SaveChangesAsync();
                return nuevoProducto.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Actualizar(Producto producto)
        {
            try
            {
                var nuevoProducto = appDbContext.Productos.Update(producto);
                await appDbContext.SaveChangesAsync();
                return nuevoProducto.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Eliminar(int productoId)
        {
            try
            {
                var producto = await appDbContext.Productos.FindAsync(productoId);
                if (producto != null)
                {
                    appDbContext.Productos.Remove(producto);
                    await appDbContext.SaveChangesAsync();
                    return producto;
                }

                return new Producto();
            }
            catch
            {
                throw;
            }
        }
    }
}
