using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SGM.Solicitacao.API.Application.Commands;
using SGM.Solicitacao.API.Application.Events;
using SGM.Solicitacao.API.Data;
using SGM.Solicitacao.API.Data.Repository;
using SGM.Solicitacao.API.Models;
using SGM.Core.Mediator;
using SGM.WebAPI.Core.Usuario;

namespace SGM.Solicitacao.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();


            services.AddScoped<IRequestHandler<RegistrarSolicitacaoCommand, ValidationResult>, SolicitacaoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarSolicitacaoCommand, ValidationResult>, SolicitacaoCommandHandler>();

            services.AddScoped<INotificationHandler<SolicitacaoRegistradoEvent>, SolicitacaoEventHandler>();
            services.AddScoped<INotificationHandler<SolicitacaoAtualizadaEvent>, SolicitacaoEventHandler>();

            services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();
            services.AddScoped<SolicitacaoContext>();

        }
    }
}