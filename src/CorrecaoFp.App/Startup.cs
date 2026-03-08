using CorrecaoFp.App.Helpers;
using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Notificacoes;
using CorrecaoFp.Business.Services;
using CorrecaoFp.Data.Context;
using CorrecaoFp.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Net;

namespace CorrecaoFp.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddHttpContextAccessor()
               .AddHttpClient();

            services.AddDbContext<ControladorFpContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlite(
                        Configuration.GetConnectionString("DefaultConnection"));
            });
            

            services
                .AddTransient(typeof(DatatablesHelper));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddScoped<ControladorFpContext>();
            services.AddScoped<ICapacitorRepository, CapacitorRepository>();
            services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository>();
            services.AddScoped<IEstagioRepository, EstagioRepository>();
            services.AddScoped<IMedicaoRepository, MedicaoRepository>();
            services.AddScoped<IModoCompensacaoRepository, ModoCompensacaoRepository>();

            services.AddScoped<ICapacitorService, CapacitorService>();
            services.AddScoped<IConfiguracaoService, ConfiguracaoService>();
            services.AddScoped<IEstagioService, EstagioService>();
            services.AddScoped<IMedicaoService, MedicaoService>();
            services.AddScoped<IModoCompensacaoService, ModoCompensacaoService>();
            
            services.AddScoped<INotificador, Notificador>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Define o middleware para interceptar exceptions não tratadas
            app.UseExceptionHandler($"/feedback/{(int)HttpStatusCode.InternalServerError}");

            // Customiza as páginas de erro
            app.UseStatusCodePagesWithReExecute("/feedback/{0}");

            app.UseHsts();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.Use(async (context, next) => {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dados}/{action=Index}/{id?}");
            });
        }
    }
}
