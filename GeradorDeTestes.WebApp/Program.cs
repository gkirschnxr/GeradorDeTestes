using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes;
using GeradorDeTestes.WebApp.DependencyInjection;

namespace GeradorDeTestes.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IRepositorioQuestao, RepositorioQuestaoEmOrm>();

            builder.Services.AddEntityFrameworkConfig(builder.Configuration);
            builder.Services.AddSerilogConfig(builder.Logging);

            builder.Services.AddControllersWithViews();
            
            var app = builder.Build();

            

            app.UseAntiforgery();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapDefaultControllerRoute();


            app.Run();
        }
    }
}
