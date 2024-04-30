FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-image
WORKDIR /app 

# -----------------------------------------------------------------------------------------
# Copy and restore all layers
# -----------------------------------------------------------------------------------------

# Main solution
COPY *.sln .

# API projects
COPY ./API/TFEHelper.Backend.API/TFEHelper.Backend.API.csproj ./API/TFEHelper.Backend.API/
COPY ./API/TFEHelper.Backend.API.Test/TFEHelper.Backend.API.Test.csproj ./API/TFEHelper.Backend.API.Test/

# Domain projects
COPY ./Domain/TFEHelper.Backend.Domain/TFEHelper.Backend.Domain.csproj ./Domain/TFEHelper.Backend.Domain/
COPY ./Domain/TFEHelper.Backend.Domain.Test/TFEHelper.Backend.Domain.Test.csproj ./Domain/TFEHelper.Backend.Domain.Test/

# Infrastructure projects
COPY ./Infrastructure/TFEHelper.Backend.Infrastructure/TFEHelper.Backend.Infrastructure.csproj ./Infrastructure/TFEHelper.Backend.Infrastructure/
COPY ./Infrastructure/TFEHelper.Backend.Infrastructure.Test/TFEHelper.Backend.Infrastructure.Test.csproj ./Infrastructure/TFEHelper.Backend.Infrastructure.Test/

# Plugin projects
COPY ./Plugins/TFEHelper.Backend.Plugins.Dummy/TFEHelper.Backend.Plugins.Dummy.csproj ./Plugins/TFEHelper.Backend.Plugins.Dummy/
COPY ./Plugins/TFEHelper.Backend.Plugins.PluginBase/TFEHelper.Backend.Plugins.PluginBase.csproj ./Plugins/TFEHelper.Backend.Plugins.PluginBase/
COPY ./Plugins/TFEHelper.Backend.Plugins.SpringerLink/TFEHelper.Backend.Plugins.SpringerLink.csproj ./Plugins/TFEHelper.Backend.Plugins.SpringerLink/
COPY ./Plugins/TFEHelper.Backend.Plugins.Scopus/TFEHelper.Backend.Plugins.Scopus.csproj ./Plugins/TFEHelper.Backend.Plugins.Scopus/
COPY ./Plugins/TFEHelper.Backend.Plugins.arXiv/TFEHelper.Backend.Plugins.arXiv.csproj ./Plugins/TFEHelper.Backend.Plugins.arXiv/
COPY ./Plugins/TFEHelper.Backend.Plugins.Pubmed/TFEHelper.Backend.Plugins.Pubmed.csproj ./Plugins/TFEHelper.Backend.Plugins.Pubmed/
COPY ./Plugins/TFEHelper.Backend.Plugins.Eric/TFEHelper.Backend.Plugins.Eric.csproj ./Plugins/TFEHelper.Backend.Plugins.Eric/

# Services projects
COPY ./Services/TFEHelper.Backend.Services/TFEHelper.Backend.Services.csproj ./Services/TFEHelper.Backend.Services/
COPY ./Services/TFEHelper.Backend.Services.Abstractions/TFEHelper.Backend.Services.Abstractions.csproj ./Services/TFEHelper.Backend.Services.Abstractions/
COPY ./Services/TFEHelper.Backend.Services.Contracts/TFEHelper.Backend.Services.Contracts.csproj ./Services/TFEHelper.Backend.Services.Contracts/
COPY ./Services/TFEHelper.Backend.Services.Test/TFEHelper.Backend.Services.Test.csproj ./Services/TFEHelper.Backend.Services.Test/

# Tools projects
COPY ./Tools/TFEHelper.Backend.Tools/TFEHelper.Backend.Tools.csproj ./Tools/TFEHelper.Backend.Tools/
COPY ./Tools/TFEHelper.Backend.Tools.Test/TFEHelper.Backend.Tools.Test.csproj ./Tools/TFEHelper.Backend.Tools.Test/

# External packages
COPY ./Packages/. ./Packages

RUN dotnet nuget add source /app/Packages

RUN dotnet restore

# -----------------------------------------------------------------------------------------
# Copy all files and build / publish
# -----------------------------------------------------------------------------------------
COPY . .
RUN dotnet build
RUN dotnet publish ./API/TFEHelper.Backend.API/TFEHelper.Backend.API.csproj -o /publish/
RUN dotnet publish ./Plugins/TFEHelper.Backend.Plugins.Dummy/TFEHelper.Backend.Plugins.Dummy.csproj -o /publish/
RUN dotnet publish ./Plugins/TFEHelper.Backend.Plugins.SpringerLink/TFEHelper.Backend.Plugins.SpringerLink.csproj -o /publish/
RUN dotnet publish ./Plugins/TFEHelper.Backend.Plugins.Scopus/TFEHelper.Backend.Plugins.Scopus.csproj -o /publish/
RUN dotnet publish ./Plugins/TFEHelper.Backend.Plugins.arXiv/TFEHelper.Backend.Plugins.arXiv.csproj -o /publish/
RUN dotnet publish ./Plugins/TFEHelper.Backend.Plugins.Pubmed/TFEHelper.Backend.Plugins.Pubmed.csproj -o /publish/
RUN dotnet publish ./Plugins/TFEHelper.Backend.Plugins.Eric/TFEHelper.Backend.Plugins.Eric.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app 
EXPOSE 5000

RUN mkdir -p /app/data
COPY --from=build-image /publish .
ENV ASPNETCORE_URLS=http://+:5000 
ENTRYPOINT ["dotnet", "TFEHelper.Backend.API.dll"]