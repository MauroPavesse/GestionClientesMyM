using System;
using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Services.ProductosVersiones
{
    public class ProductoVersionService
    {
        private readonly AppDbContext appDbContext;

        public ProductoVersionService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<ProductoVersion>> ObtenerTodo()
        {
            try
            {
                var productosVersiones = await appDbContext.ProductosVersiones.ToListAsync();

                return productosVersiones;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ProductoVersion>> ObtenerPorProductoId(int productoId)
        {
            try
            {
                var productosVersiones = await appDbContext.ProductosVersiones.Where(x => x.ProductoId == productoId).ToListAsync();

                return productosVersiones;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ProductoVersion>> ObtenerDatosCombo(int productoId = 0)
        {
            try
            {
                List<ProductoVersion> productosVersiones = new List<ProductoVersion>();
                if (productoId > 0)
                {
                    productosVersiones = await appDbContext.ProductosVersiones.Where(x => x.ProductoId == productoId).ToListAsync();
                }
                else
                {
                    productosVersiones = await appDbContext.ProductosVersiones.ToListAsync();
                }

                return productosVersiones;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoVersion> Crear(ItemProductoVersionInput input)
        {
            try
            {
                var productoVersion = input.Adapt<ProductoVersion>();
                var nuevoproductoVersion = await appDbContext.ProductosVersiones.AddAsync(productoVersion);
                await appDbContext.SaveChangesAsync();
                return nuevoproductoVersion.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoVersion> Actualizar(ItemProductoVersionInput input)
        {
            try
            {
                var productoVersion = input.Adapt<ProductoVersion>();
                var nuevoproductoVersion = appDbContext.ProductosVersiones.Update(productoVersion);
                await appDbContext.SaveChangesAsync();
                return nuevoproductoVersion.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoVersion> Eliminar(int productoVersionId)
        {
            try
            {
                var productoVersion = await appDbContext.ProductosVersiones.FindAsync(productoVersionId);
                if(productoVersion != null)
                {
                    appDbContext.ProductosVersiones.Remove(productoVersion);
                    await appDbContext.SaveChangesAsync();
                    return productoVersion;
                }

                return new ProductoVersion();
            }
            catch
            {
                throw;
            }
        }
    }
}
