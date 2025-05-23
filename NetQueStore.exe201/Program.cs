using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
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
            builder.Logging.AddConsole(); // Hiển thị log ra terminal của VS Code
            builder.Logging.SetMinimumLevel(LogLevel.Debug);


            builder.Services.AddScoped<IVnPayService, VnPayService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
