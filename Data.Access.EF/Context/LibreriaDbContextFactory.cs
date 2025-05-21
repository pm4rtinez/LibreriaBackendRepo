using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Context
{
    public class LibreriaDbContextFactory : IDesignTimeDbContextFactory<LibreriaDbContext>
    {
        public LibreriaDbContext CreateDbContext(string[] args)
        {
            // Ruta a Presentation.Api para leer appsettings.json
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "../Presentation.Api");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LibreriaDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new LibreriaDbContext(optionsBuilder.Options);
        }
    }
}
