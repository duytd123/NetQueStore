using Microsoft.EntityFrameworkCore;
using Net.payOS;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Services.Payos;
using NetQueStore.exe201.Services.Vnpay;

namespace NetQueStore.exe201
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<Exe2Context>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           );

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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
                var configuration = sp.GetRequiredService<IConfiguration>(); // lấy config từ DI container
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
                app.UseDeveloperExceptionPage();  // HIỂN THỊ CHI TIẾT LỖI TRONG DEV
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseStatusCodePages();

            app.UseSession();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
