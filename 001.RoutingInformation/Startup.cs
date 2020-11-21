using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace _001.RoutingInformation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // добавляем сервис маршрутизации
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);

            //routeBuilder.MapRoute("{controller}/{action}/{id}",
            //    async context => {
            //        RouteData data = context.GetRouteData();

            //        foreach (var element in data.Values)
            //            await context.Response.WriteAsync($"<br>{element.ToString()}</br>");
            //    });

            routeBuilder.MapRoute("{controller}/{action}",
                async context => {
                    string controller = context.GetRouteValue("controller").ToString();
                    string action = context.GetRouteValue("action").ToString();
                    await context.Response.WriteAsync($"The route controller is {controller} ");
                    await context.Response.WriteAsync($" The route action is {action}");
                   
                });

            routeBuilder.MapRoute("{controller}/{action}/{id}", async context =>
            {
                RouteData data = context.GetRouteData();
                foreach (var element in data.Values)
                {
                    await context.Response.WriteAsync(element.ToString()+" ");
                }
            });
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Default page.");
            });
        }
    }
}
