using DataAccessEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Helper;
using Models;
using Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheConsoleProgram;

class Program
{
    const string RICK_AND_MORTY_ENDPOINT = "https://rickandmortyapi.com/api/character/";

    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        var contextOptions = new DbContextOptionsBuilder<TVShowContext>()
            .UseSqlServer(connectionString)
            .Options;

        using (var context = new TVShowContext(contextOptions))
        {
            var tmp = context.Database.CanConnect();
            //if (!context.Database.CanConnect()) return;

            context.Database.Migrate();

            var databaseService = new DatabaseService(context, RICK_AND_MORTY_ENDPOINT);
            databaseService.ImportToDatabase();
        }


        //Console.WriteLine("Import data finished!");
    }
}

