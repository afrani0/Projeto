using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React_Arquivos_Backend.Business.Implementation;
using React_Arquivos_Backend.Business.Interface;
using React_Arquivos_Backend.ManagementAplicationDB;
using React_Arquivos_Backend.Repository.Implementation;
using React_Arquivos_Backend.Repository.Interface;

namespace React_Arquivos_Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; /***ATENÇAO :permitindo alguns
        sites com origem CORS, possam ser permitidos mesmo com algumas condições de origens diferentes*/

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Inicio configuração CORS
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                      //.AllowAnyOrigin()
                                      //.AllowCredentials();
                                  });
            });
            //FIm set CORS

            services.AddScoped<IArquivoBusiness, ArquivoBusiness>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();

            services.AddDbContext<DBAplication>(
            options => options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);//permitindo cors para paginas específicas
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
