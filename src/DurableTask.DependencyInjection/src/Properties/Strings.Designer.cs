﻿// <auto-generated />

using System.Resources;
using System.Globalization;

namespace DurableTask.DependencyInjection.Properties;

internal static class Strings
{
    private static readonly ResourceManager _resourceManager
        = new ResourceManager("DurableTask.DependencyInjection.Properties.Strings", typeof(Strings).Assembly);

    /// <summary>
    ///     Add is not supported. Use the &lt;c&gt;ITaskHubWorkerBuilder&lt;/c&gt; instead.
    /// </summary>
    public static string AddToObjectManagerNotSupported
        => GetString("AddToObjectManagerNotSupported");

    /// <summary>
    ///     Expected middleware of type '{type}' to be present in '{list}'.
    /// </summary>
    public static string ExpectedMiddlewareMissing(object type, object list)
        => string.Format(
            GetString("ExpectedMiddlewareMissing", nameof(type), nameof(list)),
            type, list,
            CultureInfo.CurrentUICulture);

    /// <summary>
    ///     InnerActivity is null.
    /// </summary>
    public static string InnerActivityNull
        => GetString("InnerActivityNull");

    /// <summary>
    ///     InnerOrchestration is null.
    /// </summary>
    public static string InnerOrchestrationNull
        => GetString("InnerOrchestrationNull");

    /// <summary>
    ///     Failed to create middleware for descriptor: {descriptor}
    /// </summary>
    public static string MiddlewareCreationFailed(object descriptor)
        => string.Format(
            GetString("MiddlewareCreationFailed", nameof(descriptor)),
            descriptor,
            CultureInfo.CurrentUICulture);

    /// <summary>
    ///     Failed to add TaskHubClient. No IOrchestrationServiceClient was found in the service container and '{orchestrationType}' does not implement IOrchestrationServiceClient.
    /// </summary>
    public static string NotOrchestrationServiceClient(object orchestrationType)
        => string.Format(
            GetString("NotOrchestrationServiceClient", nameof(orchestrationType)),
            orchestrationType,
            CultureInfo.CurrentUICulture);

    /// <summary>
    ///     Scope already exists for orchestration '{orchestrationId}'.
    /// </summary>
    public static string ScopeAlreadyExists(object orchestrationId)
        => string.Format(
            GetString("ScopeAlreadyExists", nameof(orchestrationId)),
            orchestrationId,
            CultureInfo.CurrentUICulture);

    /// <summary>
    ///     '{name}' cannot be an empty string or start with the null character.
    /// </summary>
    public static string StringEmpty(object name)
        => string.Format(
            GetString("StringEmpty", nameof(name)),
            name,
            CultureInfo.CurrentUICulture);

    private static string GetString(string name, params string[] formatterNames)
    {
        var value = _resourceManager.GetString(name);
        for (var i = 0; i < formatterNames.Length; i++)
        {
            value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
        }

        return value;
    }
}
