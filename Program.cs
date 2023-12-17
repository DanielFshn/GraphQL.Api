using GQL.Api.Data;
using GQL.Api.GraphQL;
using GQL.Api.GraphQL.Commands;
using GQL.Api.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer
            (builder.Configuration.GetConnectionString("GQL")));

//add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .AddInMemorySubscriptions();


var app = builder.Build();

app.UseRouting();

app.UseWebSockets();

app.UseEndpoints(endpoints => endpoints.MapGraphQL());

app.UseGraphQLVoyager("/graphql-voyager");

app.Run();
