using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _07_MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultScheme = "Cookie";
                    x.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookie")
                .AddOpenIdConnect("oidc", x =>
                {
                    x.ClientId = "mvc_id";
                    x.Authority = "https://localhost:44357/";
                    x.ClientId = "mvc_id";
                    x.ClientSecret = "mvc_secret";
                    x.SaveTokens = true;
                    x.ResponseType = "code";
                });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
