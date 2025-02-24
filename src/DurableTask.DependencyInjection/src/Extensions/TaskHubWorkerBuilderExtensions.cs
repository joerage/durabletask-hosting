// Copyright (c) Jacob Viau. All rights reserved.
// Licensed under the APACHE 2.0. See LICENSE file in the project root for full license information.

using DurableTask.Core;

namespace DurableTask.DependencyInjection;

/// <summary>
/// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
/// </summary>
public static class TaskHubWorkerBuilderExtensions
{
    /// <summary>
    /// Sets the provided <paramref name="orchestrationService"/> to the <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The task hub builder.</param>
    /// <param name="orchestrationService">The orchestration service to use.</param>
    /// <returns>The original builder, with orchestration service set.</returns>
    public static ITaskHubWorkerBuilder WithOrchestrationService(
        this ITaskHubWorkerBuilder builder, IOrchestrationService orchestrationService)
    {
        Check.NotNull(builder);
        Check.NotNull(orchestrationService);
        builder.OrchestrationServiceFactory = (sp) => orchestrationService;
        return builder;
    }

    /// <summary>
    /// Sets the provided <paramref name="orchestrationServiceFactory"/> to the <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The task hub builder.</param>
    /// <param name="orchestrationServiceFactory">The orchestration service factory to use.</param>
    /// <returns>The original builder, with orchestration service set.</returns>
    public static ITaskHubWorkerBuilder WithOrchestrationService(
        this ITaskHubWorkerBuilder builder, Func<IServiceProvider, IOrchestrationService> orchestrationServiceFactory)
    {
        Check.NotNull(builder);
        Check.NotNull(orchestrationServiceFactory);
        builder.OrchestrationServiceFactory = orchestrationServiceFactory;
        return builder;
    }
}
