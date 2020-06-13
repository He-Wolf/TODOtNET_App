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
Backend:
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

Frontend:
- Microsoft.AspNetCore.Components.Authorization v3.1.4
- Microsoft.AspNetCore.Components.WebAssembly v3.2.0
- Microsoft.AspNetCore.Components.WebAssembly.Build v3.2.0
- Microsoft.AspNetCore.Components.WebAssembly.DevServer v3.2.0
- System.Net.Http.Json v3.2.0

Shared:
- System.ComponentModel.Annotations v4.7.0
## 5. How to run the API <a name="how-to-run-the-app"></a>
### 5.1. Build and run with Docker <a name="build-and-run-with-docker"></a>
- download and install Docker
- clone or download the content of the repository
- open a terminal and navigate to the containing folder
- write "docker build -t todowebapp:v1 ." and press Enter
- write "docker run -it --rm -p 5000:5000 todowebapp:v1" and press Enter
### 5.2. Build and run with SDK <a name="build-and-run-with-sdk"></a>
- download and install .NET Core SDK version v3.1.301 or greater (latest 3.1)
- clone or download the content of the repository
- open a terminal and navigate to the containing folder
- write "dotnet restore" and press Enter
- navigate to folder Server
- write "dotnet run" and press Enter
### 5.3. Test and stop <a name="test-and-stop"></a>
- if no error message in the terminal, open your browser (recommended: latest Chrome, Firefox, Safari, Edge Chromium or Chromium) and open: http://localhost:5000
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
- automated unit and integration tests
## 8. Resources <a name="resources"></a>
There are several online source which I used for creating this web app.
I reused my TODOtNET_API RESTful API project as a backend for this application. All the resources that are listed in the README.md at that repo applies here as well.\
Blazor resource - Including but not limited to:
- https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/
- https://www.mikesdotnetting.com/article/342/managing-authentication-token-expiry-in-webassembly-based-blazor
- https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo
- https://medium.com/@st.mas29/microsoft-blazor-web-api-with-jwt-authentication-part-1-f33a44abab9d
- https://blazor-university.com/
- https://blazor-tutorial.net/
- https://studyblazor.com/

Thank to every hero on Stackoverflow and Github who helped me with their comments! (Not all heroes wear capes.)
