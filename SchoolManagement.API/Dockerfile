# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie os arquivos .csproj e restaure as dependências
COPY ["SchoolManagement.API/SchoolManagement.API.csproj", "SchoolManagement.API/"]
COPY ["SchoolManagement.Application/SchoolManagement.Application.csproj", "SchoolManagement.Application/"]
COPY ["SchoolManagement.Domain/SchoolManagement.Domain.csproj", "SchoolManagement.Domain/"]
COPY ["SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj", "SchoolManagement.Infrastructure/"]
COPY ["SchoolManagement.Query/SchoolManagement.Query.csproj", "SchoolManagement.Query/"]
COPY ["SchoolManagement.Contract/SchoolManagement.Contract.csproj", "SchoolManagement.Contract/"]
COPY ["SchoolManagement.Command/SchoolManagement.Command.csproj", "SchoolManagement.Command/"]

# Restaure as dependências
RUN dotnet restore "SchoolManagement.API/SchoolManagement.API.csproj"

# Copie o restante dos arquivos e compile a aplicação
COPY . .
WORKDIR "/src/SchoolManagement.API"
RUN dotnet build "SchoolManagement.API.csproj" -c Release -o /app/build

# Publicação da aplicação
FROM build AS publish
RUN dotnet publish "SchoolManagement.API.csproj" -c Release -o /app/publish

# Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolManagement.API.dll"]