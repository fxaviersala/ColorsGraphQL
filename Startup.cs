using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using colorsql.Data;
using colorsql.Models;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace colorsql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ColorsContext>(opt =>
                opt.UseSqlite(Configuration.GetConnectionString("Sqlite"))
            );

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddScoped<ColorQuery>();
            services.AddTransient<ColorType>();
            services.AddTransient<TranslationType>();
            services.AddTransient<ColorsType>();
            services.AddScoped<ISchema, ColorSchema>();

            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<ITranslationRepository, TranslationRepository>();

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

            // Emplenar amb dades de prova, si cal
            InitializeAsync(app.ApplicationServices).Wait();

            // app.UseHttpsRedirection();
            app.UseMvc();
        }

        /// <summary>
        /// Emplenar amb les dades de prova
        /// </summary>
        public static async Task InitializeAsync(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<ColorsContext>();
                db.Database.Migrate();
                await InsertTestData(db);
            }
        }

        private static async Task InsertTestData(ColorsContext context)
        {
            if (context.Colors.Any())
                return;
            var one = new List<Color>() {
                new Color { Rgb="#FF0000",
                                   translations=new List<Translation>() {
                                        new Translation { Language="catalan", Name="vermell"},
                                        new Translation { Language="spanish", Name="rojo"},
                                        new Translation { Language="english", Name="red"}
                                   }
                },
                new Color { Rgb="#000000",
                                   translations=new List<Translation>() {
                                        new Translation { Language="catalan", Name="negre"},
                                        new Translation { Language="english", Name="black"}
                                    }
                },
                new Color { Rgb="#0000FF",
                                   translations=new List<Translation>() {
                                        new Translation { Language="catalan", Name="blau"},
                                    }
                }
            };
            context.AddRange(one);
            await context.SaveChangesAsync();
        }
    }
}
