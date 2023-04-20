using DataAccessEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Helper;
using Models;
using Service;

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
        using var context = new TVShowContext(contextOptions);

        var databaseService = new DatabaseService(context, RICK_AND_MORTY_ENDPOINT);
        databaseService.ImportToDatabase();

        Console.WriteLine("Import data finished!");
    }
}

