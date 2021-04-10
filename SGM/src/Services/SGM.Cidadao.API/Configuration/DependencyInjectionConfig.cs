using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SGM.Cidadao.API.Application.Commands;
using SGM.Cidadao.API.Application.Events;
using SGM.Cidadao.API.Data;
using SGM.Cidadao.API.Data.Repository;
using SGM.Cidadao.API.Models;
using SGM.Core.Mediator;
using SGM.WebAPI.Core.Usuario;

namespace SGM.Cidadao.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<RegistrarCidadaoCommand, ValidationResult>, CidadaoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarEnderecoCommand, ValidationResult>, CidadaoCommandHandler>();

            services.AddScoped<INotificationHandler<CidadaoRegistradoEvent>, CidadaoEventHandler>();

            services.AddScoped<ICidadaoRepository, CidadaoRepository>();
            services.AddScoped<CidadaoContext>();

        }
    }
}