using System.Threading.RateLimiting;
using AutoMapper;
using Cache;
using DataAccessEF;
using DataAccessEF.UnitOfWork;
using Domain.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using MinimalApi.Endpoint.Configurations.Extensions;
using Services;
using Services.Implementation;
using Services.Interfaces;

namespace TheWebApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EntitiesToDtoMappingProfile());
            mc.AddProfile(new DtoToViewModelMappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Configuration.AddConfigurationFile("appsettings.json");
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddTransient<ICharacteristicService, CharacteristicService>();
        builder.Services.AddSingleton<IRateLimitingCache, RateLimitingCache>();
        builder.Services.AddDbContext<TVShowContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Characteristic/Error");
        }
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Characteristic}/{action=Index}/{id?}");

        app.Run();
    }
}

