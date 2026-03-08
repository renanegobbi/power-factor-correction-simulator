FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CorrecaoFp.App/CorrecaoFp.App.csproj", "CorrecaoFp.App/"]
COPY ["CorrecaoFp.Business/CorrecaoFp.Business.csproj", "CorrecaoFp.Business/"]
COPY ["CorrecaoFp.Data/CorrecaoFp.Data.csproj", "CorrecaoFp.Data/"]
RUN dotnet restore "CorrecaoFp.App/CorrecaoFp.App.csproj"
COPY . .
WORKDIR "CorrecaoFp.App"
RUN dotnet build "CorrecaoFp.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CorrecaoFp.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
#COPY --from=publish /app/ControladorFP.db . 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CorrecaoFp.App.dll"]
