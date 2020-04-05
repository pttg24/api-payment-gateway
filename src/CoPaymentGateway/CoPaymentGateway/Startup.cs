using CoPaymentGateway.CQRS.Extensions.DependencyInjection;
using CoPaymentGateway.Domain.BankAggregate;
using CoPaymentGateway.Domain.PaymentAggregate;
using CoPaymentGateway.Infrastructure;
using CoPaymentGateway.Infrastructure.Repositories;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoPaymentGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Checkout Payment Gateway");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //MediaTr
            //services.AddControllers();
            services.AddMediatR(typeof(Startup).Assembly);

            //CQRS
            services.AddCQRSCommands();
            services.AddCQRSQueries();

            //Repositories
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IBankRepository, BankRepository>();

            //Context
            services.AddTransient(typeof(PaymentGatewayContext));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //InMemoryDatabase
            services.AddDbContext<PaymentGatewayContext>(options => options.UseInMemoryDatabase(databaseName: "DB_PaymentGateway"));

            services.AddMvc();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Checkout Payment Gateway", Version = "v1" });
            });
        }
    }
}