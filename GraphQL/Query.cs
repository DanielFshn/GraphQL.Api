using GQL.Api.Data;
using GQL.Api.Models;

namespace GQL.Api.GraphQL;

public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }

    [UseDbContext(typeof(AppDbContext))]
    public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
    {
        return context.Commands;
    }
}