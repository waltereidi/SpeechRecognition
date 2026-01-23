using System.Reflection;
using BuildingBlocks.Messaging.Abstractions;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Messaging;

/// <summary>
/// Extensões para configuração de injeção de dependência do módulo de mensageria.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adiciona os serviços de mensageria com MassTransit e RabbitMQ.
    /// </summary>
    /// <param name="services">Coleção de serviços.</param>
    /// <param name="configuration">Configuração do MassTransit.</param>
    /// <param name="consumerAssemblies">Assemblies contendo os consumidores a serem registrados.</param>
    /// <returns>A coleção de serviços para encadeamento.</returns>
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        MassTransitConfiguration configuration,
        params Assembly[] consumerAssemblies)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        // Registra a configuração
        services.AddSingleton(Options.Create(configuration));

        // Registra o barramento de eventos
        services.AddScoped<IEventBus, MassTransitEventBus>();
        services.AddScoped<IIntegrationEventPublisher, MassTransitEventBus>();

        // Configura o MassTransit
        services.AddMassTransit(busConfigurator =>
        {
            // Registra consumidores dos assemblies fornecidos
            if (consumerAssemblies.Length > 0)
            {
                busConfigurator.AddConsumers(consumerAssemblies);
            }

            // Configura o RabbitMQ
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.Host, (ushort)configuration.Port, configuration.VirtualHost, hostCfg =>
                {
                    hostCfg.Username(configuration.Username);
                    hostCfg.Password(configuration.Password);
                });

                // Configura retry
                cfg.UseMessageRetry(retryCfg =>
                {
                    retryCfg.Interval(
                        configuration.RetryCount,
                        TimeSpan.FromSeconds(configuration.RetryIntervalSeconds));
                });

                // Configura endpoints automaticamente
                cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(
                    prefix: configuration.QueuePrefix,
                    includeNamespace: false));
            });
        });

        return services;
    }

    /// <summary>
    /// Adiciona os serviços de mensageria usando configuração via Action.
    /// </summary>
    /// <param name="services">Coleção de serviços.</param>
    /// <param name="configureOptions">Action para configurar as opções.</param>
    /// <param name="consumerAssemblies">Assemblies contendo os consumidores.</param>
    /// <returns>A coleção de serviços para encadeamento.</returns>
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        Action<MassTransitConfiguration> configureOptions,
        params Assembly[] consumerAssemblies)
    {
        var configuration = new MassTransitConfiguration();
        configureOptions(configuration);
        return services.AddMessaging(configuration, consumerAssemblies);
    }

    /// <summary>
    /// Adiciona um handler de evento de integração ao container de DI.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento.</typeparam>
    /// <typeparam name="THandler">Tipo do handler.</typeparam>
    /// <param name="services">Coleção de serviços.</param>
    /// <returns>A coleção de serviços para encadeamento.</returns>
    public static IServiceCollection AddIntegrationEventHandler<TEvent, THandler>(this IServiceCollection services)
        where TEvent : class, IIntegrationEvent
        where THandler : class, IIntegrationEventHandler<TEvent>
    {
        services.AddScoped<IIntegrationEventHandler<TEvent>, THandler>();
        services.AddScoped<GenericConsumer<TEvent>>();
        return services;
    }

    /// <summary>
    /// Adiciona handlers de eventos de integração de um assembly.
    /// </summary>
    /// <param name="services">Coleção de serviços.</param>
    /// <param name="assembly">Assembly contendo os handlers.</param>
    /// <returns>A coleção de serviços para encadeamento.</returns>
    public static IServiceCollection AddIntegrationEventHandlersFromAssembly(
        this IServiceCollection services,
        Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var handlerInterfaceType = typeof(IIntegrationEventHandler<>);

        var handlerTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                .Select(i => new { ImplementationType = t, InterfaceType = i }))
            .ToList();

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.InterfaceType, handler.ImplementationType);
        }

        return services;
    }
}
