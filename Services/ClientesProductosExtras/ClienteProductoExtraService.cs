using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.ClientesProductosExtras
{
    public class ClienteProductoExtraService
    {
        private readonly AppDbContext appDbContext;

        public ClienteProductoExtraService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<ClienteProductoExtra>> ObtenerPorClienteProductoId(int clienteProductoId)
        {
            var outputs = await appDbContext.ClientesProductosExtras.Where(x => x.ClienteProductoId == clienteProductoId).ToListAsync();
            return outputs;
        }

        public async Task<ClienteProductoExtra> Crear(ClienteProductoExtra clienteProductoExtra)
        {
            try
            {
                var nuevoClienteProductoExtra = await appDbContext.ClientesProductosExtras.AddAsync(clienteProductoExtra);
                await appDbContext.SaveChangesAsync();
                return nuevoClienteProductoExtra.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteProductoExtra> Eliminar(int clienteProductoExtraId)
        {
            try
            {
                var clienteProductoExtra = await appDbContext.ClientesProductosExtras.FindAsync(clienteProductoExtraId);
                if (clienteProductoExtra != null)
                {
                    appDbContext.ClientesProductosExtras.Remove(clienteProductoExtra);
                    await appDbContext.SaveChangesAsync();
                    return clienteProductoExtra;
                }

                return new ClienteProductoExtra();
            }
            catch
            {
                throw;
            }
        }
    }
}
