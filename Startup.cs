using Microsoft.EntityFrameworkCore;
using KingPriceUserManagementWebApp.Data;
using System.Text.Json.Serialization;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddControllersWithViews();
        services.AddControllers();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
    }

    // Configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
    {
        DataSeeder.Initialize(dbContext);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }
       
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
