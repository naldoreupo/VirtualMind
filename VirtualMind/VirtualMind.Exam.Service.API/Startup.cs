using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VirtualMind.Exam.Domain;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Infraestructure;
using VirtualMind.Exam.Infraestructure.Interface;
using VirtualMind.Exam.Transversal;
using Newtonsoft;
using Microsoft.EntityFrameworkCore;

namespace VirtualMind.Exam.Service.API
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
            services.AddDbContext<VirtualMindDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VirtualMindDB")));

            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));                 

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });

            IMapper iMapper = Maps.InitMapper();
            services.AddSingleton(iMapper);

            services.AddTransient<InterfaceQuoteDomain, QuoteDomain>();

            services.AddTransient<InterfacePurchaseInfraestructure, PurchaseInfraestructure>();

            services.AddTransient<InterfacePurchaseDomain, PurchaseDomain>();
            services.AddTransient<InterfaceCurrencyFactoryDomain, CurrencyFactoryDomain>();
            services.AddTransient<InterfaceCurrencyDomain, CurrencyBrasilianDomain>();
            services.AddTransient<InterfaceCurrencyDomain, CurrencyDolarDomain>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
