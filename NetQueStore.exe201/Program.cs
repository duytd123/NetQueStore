using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Services.Payos;
using NetQueStore.exe201.Services.Vnpay;

namespace NetQueStore.exe201;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-CSRF-TOKEN"; 
        });

        builder.Services.AddDbContext<Exe2Context>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
       );
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.Lax;
            options.HttpOnly = HttpOnlyPolicy.Always;
            options.Secure = CookieSecurePolicy.None; 
        });

        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 30 * 1024 * 1024; 
        });

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);


        builder.Services.AddScoped<IVnPayService, VnPayService>();

        IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        builder.Services.AddSingleton<PayOSService>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>(); 
            var logger = sp.GetRequiredService<ILogger<PayOSService>>();

            var payos = new PayOS(
                configuration["PayOS:PAYOS_CLIENT_ID"] ?? throw new Exception("Missing PAYOS_CLIENT_ID"),
                configuration["PayOS:PAYOS_API_KEY"] ?? throw new Exception("Missing PAYOS_API_KEY"),
                configuration["PayOS:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Missing PAYOS_CHECKSUM_KEY")
            );

            return new PayOSService(payos, configuration, logger);
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();  
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseStatusCodePages();

        app.UseSession();

        app.UseAntiforgery();

        app.Use(async (context, next) =>
        {
            var path = context.Request.Path;

            if (path.StartsWithSegments("/Admin") && !path.StartsWithSegments("/Admin/Login"))
            {
                var isLoggedIn = context.Session.GetString("AdminLoggedIn");
                if (string.IsNullOrEmpty(isLoggedIn))
                {
                    context.Response.Redirect("/Admin/Login");
                    return;
                }
            }

            await next();
        });


        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
