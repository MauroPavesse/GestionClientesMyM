using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.Clientes
{
    public class ClienteService
    {
        private readonly AppDbContext appDbContext;

        public ClienteService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Cliente>> ObtenerDatosCombo()
        {
            var clientes = await appDbContext.Clientes.ToListAsync();
            return clientes;
        }
    }
}
