# Usa a imagem do SDK do .NET para compilar e publicar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura as dependências
COPY ["src/ProjectManager/ProjectManager.csproj", "src/ProjectManager/"]
RUN dotnet restore "./src/ProjectManager/ProjectManager.csproj"

# Copia todos os arquivos do projeto e compila a aplicação
COPY . .
WORKDIR "/src/src/ProjectManager"
RUN dotnet publish "./ProjectManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Usa a imagem do .NET Runtime para rodar a aplicação publicada
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjectManager.dll"]
