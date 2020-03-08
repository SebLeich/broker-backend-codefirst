# Generic .NET WebApi 2.0 with pre-configured Token Authentification

The Repository contains a generic Web Api 2.0 with pre-configured Bearer Token Authorization. How to set up?

1. Create a local Database

2. Update connection string in Web.config ("AuthContext")

3. Run Project via Visual Studio

4. Register User via POST to endpoint (http://localhost:58021/api/account/register)
- RAW JSON { "username": ... , "password": ... , "confirmPassword": ... }

5. Generate token via POST to endpoint (http://localhost:58021/token) 
- x-www-form-urlencoded { "username": ... , "password": ... , "grant_type": "password", ["client_id": ...] }
-> client_id is necessary, if you want to retrieve a refresh token!


Based on the following ideas:
1. https://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/<br>
2. https://bitoftech.net/2014/07/16/enable-oauth-refresh-tokens-angularjs-app-using-asp-net-web-api-2-owin/

# Known Issues
1. Generating a already existing account with password and user name causes a HTTP 500 response, no customized response

## 1. How to set token expiration?
In Startup.cs set OAuthAuthorizationServerOptions AccessTokenExpireTimeSpan (e.g. TimeSpan.FromDays(1)).

## 2. How to access protected endpoints?
Add Header Key: "Authorization", Value: "Bearer [token]".

## 3. How to gain (refresh) tokens?
Post user credentials to yout enpoint: [domain]/token
* username: ...
* password: ...
* grant_type: password
* client_id: ... (optional, only if you want to get refresh tokens)

OR (via refresh token):
* client_id: ...
* refresh_token: ...
* grant_type: refresh_token

The response should be the following:
{ "access_token": " ... ", "token_type": "bearer", "expires_in": " ... ", "refresh_token": " ... ", "as:client_id": " ... ", "username": " ... " }

## 4. How to add custom endpoints?
1. Add new Controller to folder "Controllers" via right mouse button -> Add -> Controller ...
2. Select Web-API-2 empty Controller
3. Choose a title (e.g. "CustomerController")
4. Add empty constructor
5. Add method
6. Make accessable via annotations: (following example)

`[RoutePrefix("customer")]`<br>
`// -> prefix for all methods of the controller`<br>
`public class CustomerController : ApiController { ...`

`[Authorize]`<br>
`// -> the method returns 401 if no valid token sended` <br>
`[GET]`<br>
`// -> HTTP Method given in request (GET, POST, PUT, DELETE)` <br>
`[Route("...")]` <br>
`// -> the URL of the method (e.g. "ALL" --> [domain]/api/customer/all)` <br>
`public IHttpActionResult GetAllCustomers(){ ... ` <br>
