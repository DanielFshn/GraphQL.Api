using GQL.Api.Data;
using GQL.Api.Models;

namespace GQL.Api.GraphQL.Platforms;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents any software or service that has a command line interface.");

        descriptor.Field(X => X.LicenseKey).Ignore();

        descriptor.Field(p => p.Commands)
            .ResolveWith<Resplvers>(p => p.GetCommands(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the list of aviable commands for this platform");

        base.Configure(descriptor);
    }
    public class Resplvers
    {
        public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
        {
            return context.Commands.Where(p => p.PlatformId == platform.Id);
        }
    }
}


