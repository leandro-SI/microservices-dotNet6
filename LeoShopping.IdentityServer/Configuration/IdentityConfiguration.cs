using Duende.IdentityServer.Models;

namespace LeoShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

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
                    ClientSecrets = { new Secret("r23|n^k+P/WaE=9T7!$cJLm6sVjF8ZvxMgHb".Sha256()) } 
                }
            };

    }
}
