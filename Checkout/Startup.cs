using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Checkout.Contracts;
using Checkout.Models;
using Checkout.repositories;
using Checkout.Services;
using Checkout.Rules;

namespace Checkout
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

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            // Create some dummy data
            var repository = new ProductRepository();
            repository.Save(new Product() { SKU = ProductConstants.A99_SKU, UnitPrice = ProductConstants.A99_UnitPrice });
            repository.Save(new Product() { SKU = ProductConstants.B15_SKU, UnitPrice = ProductConstants.B15_UnitPrice });
            repository.Save(new Product() { SKU = ProductConstants.C40_SKU, UnitPrice = ProductConstants.C40_UnitPrice });
            services.AddSingleton<IProductRepository>(repository);

            // Add the special offer rules
            var offers = new SpecialOffersCalculatorBuilder()
                                .WithBulkDiscount(ProductConstants.A99_SKU, 0.2M, 3)
                                .WithBulkDiscount(ProductConstants.B15_SKU, 0.15M, 2)
                                .Build();
            services.AddSingleton<ISpecialOffersCalculator>(offers);


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Checkout", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
