using GQL.Api.Data;
using GQL.Api.GraphQL.Commands;
using GQL.Api.GraphQL.Platforms;
using GQL.Api.Models;
using HotChocolate.Subscriptions;

namespace GQL.Api.GraphQL;

public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
        [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
    {
        try
        {
            var platform = new Platform
            {
                Name = input.Name,
                LicenseKey = Guid.NewGuid().ToString()
            };

            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
    {
        try
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };
            context.Commands.Add(command);
            await context.SaveChangesAsync();
            return new AddCommandPayload(command);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
