// "Copyright (c) Jacob Viau. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information."

using DurableTask.Core;
using DurableTask.DependencyInjection;
using DurableTask.Hosting.Options;

namespace DurableTask.Hosting
{
    public class DurableTaskHubClient : BaseTaskHubClient
    {
        public DurableTaskHubClient(string name, TaskHubClient client)
        : base(name)
        {
            Client = Check.NotNull(client);
        }

        public TaskHubOptions Options { get; }

        public TaskHubClient Client { get; }
    }
}
