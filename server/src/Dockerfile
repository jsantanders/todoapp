FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["server/src/TodoApp.csproj", "server/src/"]
RUN dotnet restore "server/src/TodoApp.csproj"
COPY . .
WORKDIR "/src/server/src"
RUN dotnet build "TodoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApp.dll"]
