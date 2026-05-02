using Bfs.Auth.Contracts;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Bfs.Core.Services.Auth
{

    public class UserTenantService : IUserTenantService
    {
        private readonly BfsSettings _settings;
        private readonly IScopeData _scopeData;

        public UserTenantService(IOptions<BfsSettings> bfsSettings, IScopeData scopeData)
        {
            _settings = bfsSettings.Value;
            _scopeData = scopeData;
        }

        // where is the best place to put this method? Should it be in a message handler service? Should it be in the UserService? Should it be in a TenantService? Should it be in the IdentityWebApi project instead?
        // what do you think copilot?
        // I think it depends on the overall architecture of the application and how responsibilities are divided among services.
        // If the method is primarily concerned with user management within the context of a tenant,
        // it might make sense to place it in a TenantService.
        // However, if it's more about linking users to the Identity Web API,
        // it could be argued that it belongs in the UserService or even in a separate service that specifically handles interactions with the Identity Web API.
        // Ultimately, the decision should be guided by principles of separation of concerns and maintainability.
        // This method creates a user in the Tenant database and links it to the Identity Web API (Master Database) by adding a tenant claim to the user.
        public async Task SetUpUser<T>(IAspnetUserRequest userRequest, ICrudService<T> userService) where T : IAuthUser, new()
        {
            if (userRequest == null) throw new ArgumentNullException(nameof(userRequest));
            if (string.IsNullOrEmpty(userRequest.AspNetUserId)) throw new ArgumentException("AspNetUserId cannot be null or empty.", nameof(userRequest.AspNetUserId));
            // Todo: Add additional validation as needed (e.g., check if the user already exists in the Tenant database, validate the format of AspNetUserId, etc.)
            // Create the user in the Tenant database
            var user = new T() { AspNetUserId = userRequest.AspNetUserId, Name= userRequest.Name, Notes = userRequest.Notes };
            var newUser = await userService.CreateAsync(user);

            //Add a tenant claim to the user in Identity Web API (Master Database)
            var userTenant = new UserTenantRequest(userRequest.AspNetUserId, _scopeData.TenantId.ToString());
            var identityWebApiUrl = _settings.ApiBaseUrls.IdentityWebApi;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(identityWebApiUrl);
            var response = await httpClient.PostAsJsonAsync("/identity/UserTenant/link", userTenant);
        }

    }
}
