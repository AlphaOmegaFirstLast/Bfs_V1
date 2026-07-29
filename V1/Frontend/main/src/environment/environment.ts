/*
  , logoutUrl: 'https://localhost:4201/Identity/Account/Logout'
  , loginUrl: 'https://localhost:4201/Identity/Account/Login'
  are using the same origin as dev angular origin (/main), and the proxy "proxy.config.json" will redirect to authWeb origin.
  so that in development we can use the same origin for angular and Bfs.Identity.Web
  and accordingly the cookies will be set correctly at development time!
  in development these urls are set to localhost 4201
  but in staging and production they will be set to the real authWeb origin\domain,
  which will be the same like the angular domain but with different subdomains [/auth , /main]
*/
export const environment = {
  config: 'Dev'
  , isSecurityEnabled: true
  , isAspire: false
  , loginUrl: 'https://localhost:7131/Identity/Account/Login'
  , logoutUrl: 'https://localhost:7131/Identity/Account/Logout'
  , identityWebOrigin: '/identity'

  , BestFitApiUrl: 'http://localhost:2101/api'
  , InfrastructureApiUrl: 'http://localhost:3101/api'
  , StoresApiUrl: 'http://localhost:8101/api'
  , AuthApiUrl: 'http://localhost:6101/api'
  , MasterApiUrl: 'http://localhost:3201/api'
    , StockExApiUrl: 'http://localhost:7101/api'
//Template_System_AddEnvironmentEntry
};
//ToDo set other environments,
//ToDo check login & logout urls with RouteGuardService and proxy settings 
