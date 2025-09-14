using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.ClientesProductos
{
    public class ClienteProductoService
    {
        private readonly AppDbContext appDbContext;

        public ClienteProductoService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<ItemClienteProductoOutput>> Buscar(int clienteId)
        {
            try
            {
                List<ItemClienteProductoOutput> outputs = new List<ItemClienteProductoOutput>();
                
                var clientesProductos = await appDbContext.ClientesProductos
                    .Include(x => x.Producto)
                    .Include(x => x.ProductoVersion)
                    .Include(x => x.ClientesProductosExtras)
                        .ThenInclude(x => x.Extra)
                    .Where(x => x.ClienteId == clienteId).ToListAsync();

                foreach(var clienteProducto in clientesProductos)
                {
                    List<ItemClienteProductoDetalleOutput> detalles = new List<ItemClienteProductoDetalleOutput>();
                    detalles.Add(new ItemClienteProductoDetalleOutput()
                    {
                        Id = clienteProducto.ProductoId,
                        Descripcion = clienteProducto.Producto!.Nombre + " - " + clienteProducto.ProductoVersion!.Nombre,
                        Costo = clienteProducto.ProductoVersion.CostoInicial != 0 ? clienteProducto.Producto.PrecioInicial.ToString("c") + " + " + clienteProducto.ProductoVersion.CostoInicial.ToString("c") : clienteProducto.Producto.PrecioInicial.ToString("c")
                    });

                    foreach(var extra in clienteProducto.ClientesProductosExtras)
                    {
                        detalles.Add(new ItemClienteProductoDetalleOutput()
                        {
                            Id = extra.Id,
                            Descripcion = extra.Extra!.Nombre,
                            Costo = extra.Extra.Valor.ToString("c")
                        });
                    }

                    outputs.Add(new ItemClienteProductoOutput()
                    {
                        Id = clienteProducto.Id,
                        Producto = clienteProducto.Producto.Nombre,
                        Version = clienteProducto.ProductoVersion.Nombre,
                        FechaInstalacion = clienteProducto.FechaInstalacion,
                        Importe = clienteProducto.Importe,
                        FechaProximaPago = clienteProducto.FechaProximoPago,
                        Detalles = detalles
                    });
                }

                return outputs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteProducto> ObtenerPorId(int clienteProductoId)
        {
            var clienteProducto = await appDbContext.ClientesProductos
                .Include(x => x.Producto)
                .Include(x => x.ProductoVersion)
                .Include(x => x.ClientesProductosExtras)
                .Where(x => x.Id == clienteProductoId)
                .FirstAsync();
            return clienteProducto;
        }

        public async Task<ClienteProducto> Crear(ClienteProducto clienteProducto)
        {
            try
            {
                var nuevoClienteProducto = await appDbContext.ClientesProductos.AddAsync(clienteProducto);
                await appDbContext.SaveChangesAsync();
                return nuevoClienteProducto.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteProducto> Actualizar(ClienteProducto clienteProducto)
        {
            try
            {
                var nuevoClienteProducto = appDbContext.ClientesProductos.Update(clienteProducto);
                await appDbContext.SaveChangesAsync();
                return nuevoClienteProducto.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteProducto> Eliminar(int clienteProductoId)
        {
            try
            {
                var clienteProducto = await appDbContext.ClientesProductos.FindAsync(clienteProductoId);
                if (clienteProducto != null)
                {
                    appDbContext.ClientesProductos.Remove(clienteProducto);
                    await appDbContext.SaveChangesAsync();
                    return clienteProducto;
                }

                return new ClienteProducto();
            }
            catch
            {
                throw;
            }
        }
    }
}
