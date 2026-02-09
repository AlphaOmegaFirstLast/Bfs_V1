/*
  , logoutUrl: 'https://localhost:4201/Identity/Account/Logout'
  , loginUrl: 'https://localhost:4201/Identity/Account/Login'
  , tokenUrl: 'https://localhost:4201/auth/api'
  are using the same origin as dev angular origin (/main), and the proxy "proxy.config.json" will redirect to authWeb origin.
  so that in development we can use the same origin for angular and Bfs.Identity.Web
  and accordingly the cookies will be set correctly at development time!
  in development these urls are set to localhost 4201
  but in staging and production they will be set to the real authWeb origin\domain,
  which will be the same like the angular domain but with different subdomains [/auth , /main]
*/
export const environment = {
    config: 'Dev'
    , isAspire: false
    , isSecurityEnabled: false
    , loginUrl: 'http://localhost:5043/auth/Identity/Account/Login'
    , logoutUrl: 'http://localhost:5043/auth/Identity/Account/Logout'
    , tokenUrl: 'http://localhost:5043/auth/api'
    //Template_System_AddEnvironmentEntry
};
//ToDo set other environments,
//ToDo check login & logout urls with RouteGuardService and proxy settings 
