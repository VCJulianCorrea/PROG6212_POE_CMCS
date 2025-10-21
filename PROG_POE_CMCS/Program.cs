using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PROG_POE_CMCS.Data;
namespace PROG_POE_CMCS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PROG_POE_CMCSContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PROG_POE_CMCSContext") ?? throw new InvalidOperationException("Connection string 'PROG_POE_CMCSContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

/* References:
 * 
 * tdykstra (2024). Upload files in ASP.NET Core. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-9.0.
 * Rathod, J. (2019). How to Enable Theme Customization Dynamically in ASP.NET? [online] Satva Solutions. Available at: https://satvasolutions.com/blog/enable-theme-customization-dynamically-using-asp-net-mvc [Accessed 21 Oct. 2025].
 * Webdevtutor.net. (2024). Handling Button Click Event in C# MVC. [online] Available at: https://www.webdevtutor.net/blog/c-sharp-mvc-button-click-event [Accessed 21 Oct. 2025].
 * Microsoft.com. (2022). Format currency in asp-validation-for - Microsoft Q&A. [online] Available at: https://learn.microsoft.com/en-us/answers/questions/770320/format-currency-in-asp-validation-for [Accessed 21 Oct. 2025].
 * Rafiq, Z. (2023). Security ASP.net Core MVC (C#) Encryption and Decryption. [online] C-sharpcorner.com. Available at: https://www.c-sharpcorner.com/article/the-encryption-and-decryption-of-asp-net-core-mvc-c-sharp/.
 */
