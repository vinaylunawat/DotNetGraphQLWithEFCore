using System.Security.Claims;

#nullable enable
namespace Geography.Serverless
{
    public class GraphQLRequestContext : Dictionary<string, object?>
    {
        public ClaimsPrincipal User { get; set; }

        public HttpRequest Request { get; set; }

        public GraphQLRequestContext(ClaimsPrincipal user, HttpRequest request)
        {
            User = user;
            Request = request;
        }
    }
}