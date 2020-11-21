using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

namespace _002.Constraints
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            // определяем обработчик маршрута
            var myRouteHandler = new RouteHandler(Handle);

            // создаем маршрут, используя обработчик
            var routeBuilder = new RouteBuilder(app, myRouteHandler);

            // само определение маршрута - он должен соответствовать заданному шаблону и ограничению
            routeBuilder.MapRoute("default",
                    "{controller}/{action}/{id?}",
                    new { action = "Index" }
            );

            //routeBuilder.MapRoute("default",
            //        "{controller}/{action}/{id?}",
            //        new { action = "Index" },
            //        new { controller = "^H.*", id = @"\d{2}" }
            //);

            //routeBuilder.MapRoute("default",
            //        "{controller}/{action}/{id?}",
            //        new { id = new BoolRouteConstraint() }
            //);

            // строим маршрут
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Default page.");
            });
        }

        // собственно обработчик маршрута
        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("The route is chosen.");
        }
    }
}
