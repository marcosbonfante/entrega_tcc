using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SGM.OrdemServico.API.Application.Commands;
using SGM.OrdemServico.API.Application.Events;
using SGM.OrdemServico.API.Data;
using SGM.OrdemServico.API.Data.Repository;
using SGM.OrdemServico.API.Models;
using SGM.Core.Mediator;
using SGM.WebAPI.Core.Usuario;

namespace SGM.OrdemServico.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<RegistrarOrdemCommand, ValidationResult>, OrdemCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarOrdemCommand, ValidationResult>, OrdemCommandHandler>();

            services.AddScoped<INotificationHandler<OrdemRegistradaEvent>, OrdemEventHandler>();
            services.AddScoped<INotificationHandler<OrdemAtualizadaEvent>, OrdemEventHandler>();

            services.AddScoped<IOrdemRepository, OrdemRepository>();
            services.AddScoped<OrdemServicoContext>();

        }
    }
}