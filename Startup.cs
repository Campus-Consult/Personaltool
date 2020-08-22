using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Personaltool.Data;
using Personaltool.Helpers;
using Personaltool.Models;
using Personaltool.Services;

namespace Personaltool
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
            //  Add the application db context
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/Login";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });

            services.AddAuthorization(
                options => {
                    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
                })
                .AddAuthentication(options => {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(
                    options => {
                        options.Events.OnSigningIn = context => {
                            // foreach(var claim in context.Principal.Claims) {
                            //     Console.WriteLine(claim.Type+": "+claim.Value);
                            // }
                            // remove id token, all claims are saved already so it just takes up cookie space
                            // this wouldn't be an issue if the cookie size wouldn't be over 8k with this, which is
                            // more than apache can handle
                            context.Properties.StoreTokens(context.Properties.GetTokens().Where(t => t.Name != "id_token"));
                            return Task.CompletedTask;
                        };
                        
                    }
                )
                .AddOpenIdConnect(options => {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    var azureConf = Configuration.GetSection("AzureAd");
                    options.CallbackPath = azureConf["CallbackPath"];
                    options.ClientId = azureConf["ClientId"];
                    options.ClientSecret = azureConf["ClientSecret"];
                    options.Authority = $"https://login.microsoftonline.com/{azureConf["TenantId"]}/v2.0";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    foreach (var scope in azureConf["Scopes"].Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                        options.Scope.Add(scope);
                    }
                    // options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.DisableTelemetry = true;
                    options.Events.OnUserInformationReceived = async ctx => {
                        // foreach(var token in ctx.Properties.GetTokens()) {
                        //     Console.WriteLine(token.Name);
                        //     Console.WriteLine(token.Value);
                        // }
                        var graphClient = GraphSdkHelper.GetAuthenticatedClient(ctx.Properties.GetTokenValue("access_token"));
                        var groups = await graphClient.Me.MemberOf.Request().GetAsync();
                        var groupClaimMapping = Configuration.GetSection("SharepointGroupClaimMapping");
                        var claims = groups
                            // get the Claim (if any) configued for this group
                            .Select(group => groupClaimMapping.GetValue<string>(group.Id))
                            .Where(claim => claim != null)
                            // more than one group can map to a claim
                            .Distinct()
                            // the value of the claim doesn't matter, only the type is checked
                            .Select(claim => new Claim(claim, "_")).ToList();
                        if (claims.Count != 0) {
                            var appIdentity = new ClaimsIdentity(claims);

                            ctx.Principal.AddIdentity(appIdentity);
                        }
                    };
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
 
            app.UseSpa(spa =>
            {

                spa.Options.SourcePath = "ClientApp";

                spa.Options.StartupTimeout = new TimeSpan(0, 0, 5);

               
                /*             Ausfuerung erzeugt Fehler / veraltet   
                spa.UseSpaPrerendering(options =>
                {
                    options.SupplyData = (context, data) =>
                    {
                        // Creates a new value called isHttpsRequest that's passed to TypeScript code
                        data["isHttpsRequest"] = context.Request.IsHttps;
                    };
                });
                */
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
