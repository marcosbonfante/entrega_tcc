using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SGM.WebAPI.Core.Usuario;
using SGM.WebApp.MVC.Extensions;
using SGM.WebApp.MVC.Services;
using SGM.WebApp.MVC.Services.Handlers;
using System.Net.Http;

namespace SGM.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            // Identidade 
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
                .ConfigureHttpMessageHandlerBuilder(b =>
                {
                    b.PrimaryHandler =
                    new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                });

            // Cidadão
            services.AddHttpClient<ICidadaoService, CidadaoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .ConfigureHttpMessageHandlerBuilder(b =>
                {
                    b.PrimaryHandler =
                    new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                });

            // Solicitação
            services.AddHttpClient<ISolicitacaoService, SolicitacaoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .ConfigureHttpMessageHandlerBuilder(b =>
                {
                    b.PrimaryHandler =
                    new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                });

            // Ordens de Serviço
            services.AddHttpClient<IOrdemService, OrdemService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .ConfigureHttpMessageHandlerBuilder(b =>
                {
                    b.PrimaryHandler =
                    new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                });

        }

    }
}