#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /app
COPY . .
WORKDIR "/app/Server"
RUN dotnet publish "blazor.Server.csproj" -c Release -o out
WORKDIR /app
COPY ["Server/todo.db", "Server/out"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS runtime
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
WORKDIR /app
COPY --from=build /app/Server/out ./
ENTRYPOINT ["dotnet", "blazor.Server.dll"]

#docker build -t todowebapp:v1 .
#docker run -it --rm -p 5000:5000 todowebapp:v1
#docker ps
#docker stop <container ID>

#If you use Windows Docker, dotnet restore might not work because of network issues.
#Docker for Windows uses the network adapter with the lowest Interface metric value.
#When this adapter is disconnected, your container cannot connect to the internet.
#To see if this is your problem as well:
#Get-NetIPInterface -AddressFamily IPv4 | Sort-Object -Property InterfaceMetric -Descending
#If yes, set your connected active connection to the lowest value:
#Set-NetIPInterface -InterfaceAlias 'Wi-Fi' -InterfaceMetric 20