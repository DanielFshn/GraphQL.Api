using GQL.Api.Models;

namespace GQL.Api.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform) =>
            platform;
    }
}
