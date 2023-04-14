using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace LeoShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new  ApiScope("leo_shopping", "leoShopping Server"),
                new  ApiScope(name: "read", "Read data"),
                new  ApiScope(name: "write", "Write data"),
                new  ApiScope(name: "delete", "Delete data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("r23|n^k+P/WaE=9T7!$cJLm6sVjF8ZvxMgHb".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "leo_shopping",
                    ClientSecrets = { new Secret("r23|n^k+P/WaE=9T7!$cJLm6sVjF8ZvxMgHb".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:4430/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "leo_shopping"
                    }
                }
            };

    }
}
