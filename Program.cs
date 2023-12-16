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
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddProjections();


var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapGraphQL());

app.UseGraphQLVoyager("/graphql-voyager");

app.Run();
