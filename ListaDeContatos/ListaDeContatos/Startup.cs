using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ListaDeContatos.Mappers;
using ListaDeContatos.Models;
using ListaDeContatos.Negocio.Implementacao;
using ListaDeContatos.Negocio.Interface;
using ListaDeContatos.Repositorio.Implementacao;
using ListaDeContatos.Repositorio.Interface;
using ListaDeContatos.Util;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ListaDeContatos
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
            //Inicio - Configurando AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
                
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            //fim - config AutoMapper

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<Usuario, NivelAcesso>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders(); 

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = x =>
                    {
                        x.Response.Redirect("/Login/Login");
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = x =>
                    {
                        x.Response.Redirect("/Menu/AcessoNegado");
                        return Task.CompletedTask;
                    }
                    
                };
               

            });

            //injetar util
            services.AddTransient<SenhaUtil, SenhaUtil>();

            //injetar interface Repositório
            services.AddTransient<UsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<LoginRepositorio, LoginRepositorio>();
            services.AddTransient<ContatoRepositorio, ContatoRepositorio>();

            services.AddTransient<UsuarioNivelAcessoRepositorio, UsuarioNivelAcessoRepositorio>();
            services.AddTransient<NivelAcessoRepositorio, NivelAcessoRepositorio>();

            services.AddTransient<AssociarUsuarioNegocio, AssociarUsuarioNegocio>();

            //injetar interface Negócio
            services.AddTransient<SenhaNegocio, SenhaNegocio>();
            services.AddTransient<RegistroNegocio, RegistroNegocio>();
            services.AddTransient<LoginNegocio, LoginNegocio>();
            services.AddTransient<PrimeiroAcessoNegocio, PrimeiroAcessoNegocio>();
            services.AddTransient<ContatoNegocio, ContatoNegocio>();

            services.AddTransient<UserManager<Usuario>, UserManager<Usuario>>();
            services.AddTransient<SignInManager<Usuario>, SignInManager<Usuario>>();

            services.AddTransient<RoleManager<NivelAcesso>, RoleManager<NivelAcesso>>();

            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            
        }
                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Menu/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
