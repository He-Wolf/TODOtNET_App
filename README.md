# TODOtNET - ASP.NET Core hosted full-stack C# single-page web application with Blazor Webassembly
## Table of Content
1. [Introduction](#introduction)
2. [Used SDK version](#used-sdk-version)
3. [Used tools](#used-tools)
4. [Used packages](#used-packages)
5. [How to run the app](#how-to-run-the-app)\
	5.1. [Build and run with Docker](#build-and-run-with-docker)\
	5.2. [Build and run with SDK](#build-and-run-with-sdk)\
	5.3. [Test and stop](#test-and-stop)
6. [Limitations](#limitations)\
    7.1. [Exception/error handling](#exception-error-handling)
7. [Some further development possibilities](#some-further-development-possibilities)
8. [Resources](#resources)

## 1. Introduction <a name="introduction"></a>
This is a basic ASP.NET Core hosted SPA using Blazor Webassembly with CRUD operations and user login/account management. You can register a new user account with your email address, name and password. After successful registration, you can log in and add, edit, remove and track TODO items. You can also edit your account data and delete your account. This app was created for learning purpose, but is might be useful as a starting-point for other projects.

The backend uses:
- JWT for authentication
- Entity Core as ORM
- Identity Core for identity management
- SQLite for DB management

The frontend uses:
- Blazor Webassembly
- Bootstrap

Tooling:
- Git Extensions as git gui
- VSC as text editor
- Docker for containerization
- Windows 10 as OS

If any question, please do not hesitate to contact me.

## 2. Used SDK version <a name="used-sdk-version"></a>
.NET Core SDK v3.1.301
## 3. Used tools <a name="used-tools"></a>
- dotnet-ef
- dotnet-aspnet-codegenerator
## 4. Used packages <a name="used-packages"></a>
- Microsoft.AspNetCore.Authentication.JwtBearer v3.1.3
- Microsoft.AspNetCore.Identity v2.2.0
- Microsoft.AspNetCore.Identity.EntityFrameworkCore v3.1.3
- Microsoft.AspNetCore.Mvc.NewtonsoftJson v3.1.3
- Microsoft.EntityFrameworkCore.Design v3.1.2
- Microsoft.EntityFrameworkCore.SQLite v3.1.2
- Microsoft.EntityFrameworkCore.SqlServer v3.1.2
- Microsoft.IdentityModel.Tokens v6.5.0
- Microsoft.VisualStudio.Web.CodeGeneration.Design v3.1.1
- Microsoft.AspNetCore.Components.WebAssembly.Server 3.2.0
- AutoMapper.Extensions.Microsoft.DependencyInjection v7.0.0

- Microsoft.AspNetCore.Components.Authorization v3.1.4
- Microsoft.AspNetCore.Components.WebAssembly v3.2.0
- Microsoft.AspNetCore.Components.WebAssembly.Build v3.2.0
- Microsoft.AspNetCore.Components.WebAssembly.DevServer v3.2.0
- System.Net.Http.Json v3.2.0

-System.ComponentModel.Annotations v4.7.0
## 5. How to run the API <a name="how-to-run-the-app"></a>
### 5.1. Build and run with Docker <a name="build-and-run-with-docker"></a>
- download and install Docker
- clone or download the content of the repository
- open a terminal and navigate to the containing folder
- write "docker build -t todowebapi:v1 ." and press Enter
- write "docker run -it --rm -p 5000:5000 todowebapi:v1" and press Enter
### 5.2. Build and run with SDK <a name="build-and-run-with-sdk"></a>
- download and install .NET Core SDK version v3.1.201 or greater (latest 3.1)
- clone or download the content of the repository
- open a terminal and navigate to the containing folder
- write "dotnet restore" and press Enter
- write "dotnet run" and press Enter
### 5.3. Test and stop <a name="test-and-stop"></a>
- if no error message in the terminal, open your browser (recommended: latest Chrome, Firefox, Safari, Edge Chromium or Chromium) and open: http://localhost:5000/swagger
- first register a user account, then log in and after that you can manage your TODO items and account
- after testing go back to the terminal and press "Ctrl+C" to stop the web server
## 6. Limitations <a name="limitations"></a>
### 6.1. Exception/error handling <a name="exception-error-handling"></a>
This application needs to be extended with exception handling and more response values. There are some already known issues which may cause error when it is not used correctly. I only tested the app with correct input values.
## 7. Some further development possibilities <a name="some-further-development-possibilities"></a>
- token refreshing
- Facebook sign-in
- adding roles (admin, user)
- OpenID Connect & IdentityServer4
- SPA frontend (Blazor webassembly)
- automated unit and integration tests
## 8. Resources <a name="resources"></a>
There are several online source which I used to create this web app.\
Including but not limited to:
- Microsoft:
	- https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1
	- https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-3.1
	- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-3.1
	- https://docs.microsoft.com/en-us/ef/core/
	- https://docs.microsoft.com/en-us/dotnet/csharp/
- Tutorialspoint:
	- https://www.tutorialspoint.com/csharp/
	- https://www.tutorialspoint.com/asp.net_core/
	- https://www.tutorialspoint.com/asp.net_mvc/index.htm
	- https://www.tutorialspoint.com/entity_framework/index.htm
	- https://www.tutorialspoint.com/linq/index.htm
- TutorialsTeacher:
	- https://www.tutorialsteacher.com/core
	- https://www.tutorialsteacher.com/webapi/web-api-tutorials
	- https://www.tutorialsteacher.com/mvc/asp.net-mvc-tutorials
	- https://www.tutorialsteacher.com/csharp/csharp-tutorials
	- https://www.tutorialsteacher.com/linq/linq-tutorials
	- https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx
- JWT:
	- https://medium.com/@ozgurgul/asp-net-core-2-0-webapi-jwt-authentication-with-identity-mysql-3698eeba6ff8
	- https://dotnetdetail.net/asp-net-core-3-0-web-api-token-based-authentication-example-using-jwt-in-vs2019/
	- https://code-maze.com/authentication-aspnetcore-jwt-1/
	- https://fullstackmark.com/post/19/jwt-authentication-flow-with-refresh-tokens-in-aspnet-core-web-api
	- https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
	- https://www.c-sharpcorner.com/article/asp-net-core-2-0-bearer-authentication/
	- https://www.blinkingcaret.com/2017/09/06/secure-web-api-in-asp-net-core/
	- https://salslab.com/a/jwt-authentication-and-authorisation-in-asp-net-core-web-api
	- https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api

Thank to every hero on Stackoverflow and Github who helped me with their comments! (Not all heroes wear capes.)
