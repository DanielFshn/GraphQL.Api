using GQL.Api.Data;
using GQL.Api.Models;

namespace GQL.Api.GraphQL.Commands;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command.");

        descriptor.Field(c => c.Platform)
            .ResolveWith<Resplvers>(p => p.GetPlatform(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the platform to which the command belongs");

        base.Configure(descriptor);
    }
    private class Resplvers
    {
        public Platform GetPlatform([Parent] Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
        }
    }
}
