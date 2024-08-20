using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Repositories;

namespace ProductApiIntegrationTests;

public class TestStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<Infrastructure.Persistence.ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}
