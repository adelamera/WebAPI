
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace MovieAPI
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My Movies API",
                    Description = "My First Movies ASP.NET Core Web API",
                    TermsOfService = "None",
                });
            });
            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterType<FakeMovieRepository>().As<IFakeMovieRepository>().SingleInstance();
            this.ApplicationContainer = builder.Build();

            

            return new AutofacServiceProvider(this.ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            
                if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Movies API V1");
                }
            });
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());

        }
    }
}
