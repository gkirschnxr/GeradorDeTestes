using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes;
using GeradorDeTestes.WebApp.DependencyInjection;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.WebApp.DependencyInjection;

namespace GeradorDeTestes.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IRepositorioMateria, RepositorioMateriaEmOrm>();
            builder.Services.AddScoped<IRepositorioQuestao, RepositorioQuestaoEmOrm>();

            builder.Services.AddEntityFrameworkConfig(builder.Configuration);
            builder.Services.AddSerilogConfig(builder.Logging);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/erro");
            else
                app.UseDeveloperExceptionPage();

            app.UseAntiforgery();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
