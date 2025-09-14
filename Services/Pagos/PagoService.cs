using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.Pagos
{
    public class PagoService
    {
        private readonly AppDbContext appDbContext;

        public PagoService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Pago>> ObtenerTodo()
        {
            var pagos = await appDbContext.Pagos
                .ToListAsync();
            return pagos;
        }

        public async Task<Pago> ObtenerPorId(int pagoId)
        {
            var pago = await appDbContext.Pagos
                .Where(x => x.Id == pagoId)
                .FirstAsync();
            return pago;
        }

        public async Task<List<Pago>> ObtenerPorClienteProductoId(int clienteProductoId)
        {
            var pagos = await appDbContext.Pagos
                .Where(x => x.ClienteProductoId == clienteProductoId)
                .ToListAsync();
            return pagos;
        }

        public async Task<Pago> Crear(Pago pago)
        {
            try
            {
                var nuevoPago = await appDbContext.Pagos.AddAsync(pago);
                await appDbContext.SaveChangesAsync();
                return nuevoPago.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Pago> Actualizar(Pago pago)
        {
            try
            {
                var nuevoPago = appDbContext.Pagos.Update(pago);
                await appDbContext.SaveChangesAsync();
                return nuevoPago.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Pago> Eliminar(int pagoId)
        {
            try
            {
                var pago = await appDbContext.Pagos.FindAsync(pagoId);
                if (pago != null)
                {
                    appDbContext.Pagos.Remove(pago);
                    await appDbContext.SaveChangesAsync();
                    return pago;
                }

                return new Pago();
            }
            catch
            {
                throw;
            }
        }
    }
}
