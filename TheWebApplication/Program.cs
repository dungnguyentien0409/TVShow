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
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Configuration.AddConfigurationFile("appsettings.test.json");
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EntitiesToDtoMappingProfile());
            mc.AddProfile(new DtoToViewModelMappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddTransient<ICharacteristicService, CharacteristicService>();
        builder.Services.AddSingleton<IRateLimitingCache, RateLimitingCache>();
        System.Threading.Thread.Sleep(900000);
        builder.Services.AddDbContext<TVShowContext>(options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
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

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Styles")),
            RequestPath = "/Styles"
        });
        app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
                RequestPath = "/Scripts"
            }
        );

        app.Run();
    }
}

