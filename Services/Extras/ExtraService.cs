using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.Extras
{
    public class ExtraService
    {
        private readonly AppDbContext appDbContext;

        public ExtraService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Extra>> ObtenerDatosCombo()
        {
            var extras = await appDbContext.Extras.ToListAsync();
            return extras;
        }

        public async Task<List<Extra>> ObtenerTodo()
        {
            var extras = await appDbContext.Extras.ToListAsync();
            return extras;
        }

        public async Task<Extra> Crear(Extra extra)
        {
            try
            {
                var nuevoExtra = await appDbContext.Extras.AddAsync(extra);
                await appDbContext.SaveChangesAsync();
                return nuevoExtra.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Extra> Actualizar(Extra extra)
        {
            try
            {
                var nuevoExtra = appDbContext.Extras.Update(extra);
                await appDbContext.SaveChangesAsync();
                return nuevoExtra.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Extra> Eliminar(int extraId)
        {
            try
            {
                var extra = await appDbContext.Extras.FindAsync(extraId);
                if (extra != null)
                {
                    appDbContext.Extras.Remove(extra);
                    await appDbContext.SaveChangesAsync();
                    return extra;
                }

                return new Extra();
            }
            catch
            {
                throw;
            }
        }
    }
}
