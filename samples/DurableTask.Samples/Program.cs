﻿// Copyright (c) Jacob Viau. All rights reserved.
// Licensed under the APACHE 2.0. See LICENSE file in the project root for full license information.

using DurableTask.Core;
using DurableTask.DependencyInjection;
using DurableTask.Emulator;
using DurableTask.Hosting;
using DurableTask.Hosting.Options;
using DurableTask.Samples.Generics;
using DurableTask.Samples.Greetings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DurableTask.DependencyInjection.Extensions;
using DurableTask.Samples;

namespace DurableTask.Named.Samples;

/// <summary>
/// The samples program.
/// </summary>
public class Program
{
    private static readonly string s_taskHubName = "myHub";

    /// <summary>
    /// The entry point.
    /// </summary>
    /// <param name="args">The supplied arguments, if any.</param>
    /// <returns>A task that completes when this program is finished running.</returns>
    public static Task Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder => builder.AddUserSecrets<Program>())
            .ConfigureServices(services =>
            {
                services.Configure<TaskHubOptions>(opt =>
                {
                    opt.CreateIfNotExists = true;
                });
                services.AddSingleton<IConsole, ConsoleWrapper>();
                services.AddHostedService<TaskEnqueuer>();
                services.AddSingleton(UseLocalEmulator());
            })
            .ConfigureTaskHubWorker((context, builder) =>
            {
                builder.AddClient();
                builder.UseOrchestrationMiddleware<SampleMiddleware>();
                builder.UseActivityMiddleware<SampleMiddleware>();

                builder
                    .AddOrchestration<GreetingsOrchestration>()
                    .AddOrchestration<GenericOrchestrationRunner>();

                builder.AddActivitiesFromAssembly<Program>();
            })
            .UseConsoleLifetime()
            .Build();

        return host.RunAsync();
    }

    private static IOrchestrationService UseLocalEmulator()
        => new LocalOrchestrationService();

    private class TaskEnqueuer : BackgroundService
    {
        private readonly TaskHubClient _client;
        private readonly IConsole _console;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public TaskEnqueuer(TaskHubClient client, IConsole console)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            OrchestrationInstance instance = await _client.CreateOrchestrationInstanceAsync(
                NameVersionHelper.GetDefaultName(typeof(GenericOrchestrationRunner)),
                NameVersionHelper.GetDefaultVersion(typeof(GenericOrchestrationRunner)),
                _instanceId,
                null,
                new Dictionary<string, string>()
                {
                    ["CorrelationId"] = Guid.NewGuid().ToString(),
                });

            OrchestrationState result = await _client.WaitForOrchestrationAsync(
                instance, TimeSpan.FromSeconds(60), stoppingToken);

            _console.WriteLine();
            _console.WriteLine($"Orchestration finished.");
            _console.WriteLine($"Run stats: {result.Status}");
            _console.WriteLine("Press Ctrl+C to exit");
        }
    }
}
