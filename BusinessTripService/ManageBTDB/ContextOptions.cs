using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ManageBTDB
{
    public class ContextOptions
    {
        public static DbContextOptions<BTdbContext> options = GetOptions();
        private static DbContextOptions<BTdbContext> GetOptions()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<BTdbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            return options;
        }


    }

}
