using Microsoft.Extensions.Configuration;

namespace GestionClientesMyM.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            //var configuracionesIniciales = new List<Configuracion>
            //{
            //    new Configuracion { Id = 1, Campo = "NombreEmpresa", ValorString = "", ValorNumerico = 0 }
            //};

            //foreach (var config in configuracionesIniciales)
            //{
            //    if (!context.Configuraciones.Any(c => c.Campo == config.Campo))
            //    {
            //        context.Configuraciones.Add(config);
            //    }
            //}

            context.SaveChanges();
        }
    }
}
