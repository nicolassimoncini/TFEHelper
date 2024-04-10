FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-image
WORKDIR /app 

COPY ./API/TFEHelper.Backend.API/TFEHelper.Backend.API.csproj ./API/TFEHelper.Backend.API/
COPY ./API/TFEHelper.Backend.API.Test/TFEHelper.Backend.API.Test.csproj ./API/TFEHelper.Backend.API.Test/

COPY ./Domain/TFEHelper.Backend.Domain/TFEHelper.Backend.Domain.csproj ./Domain/TFEHelper.Backend.Domain/
COPY ./Domain/TFEHelper.Backend.Domain.Test/TFEHelper.Backend.Domain.Test.csproj ./Domain/TFEHelper.Backend.Domain.Test/

COPY ./Infrastructure/TFEHelper.Backend.Infrastructure/TFEHelper.Backend.Infrastructure.csproj ./Infrastructure/TFEHelper.Backend.Infrastructure/
COPY ./Infrastructure/TFEHelper.Backend.Infrastructure.Test/TFEHelper.Backend.Infrastructure.Test.csproj ./Infrastructure/TFEHelper.Backend.Infrastructure.Test/

COPY ./Plugins/TFEHelper.Backend.Plugins.Dummy/TFEHelper.Backend.Plugins.Dummy.csproj ./Plugins/TFEHelper.Backend.Plugins.Dummy/
COPY ./Plugins/TFEHelper.Backend.Plugins.PluginBase/TFEHelper.Backend.Plugins.PluginBase.csproj ./Plugins/TFEHelper.Backend.Plugins.PluginBase/
COPY ./Plugins/TFEHelper.Backend.Plugins.SpringerLink/TFEHelper.Backend.Plugins.SpringerLink.csproj ./Plugins/TFEHelper.Backend.Plugins.SpringerLink/

COPY ./Services/TFEHelper.Backend.Services/TFEHelper.Backend.Services.csproj ./Services/TFEHelper.Backend.Services/
COPY ./Services/TFEHelper.Backend.Services.Abstractions/TFEHelper.Backend.Services.Abstractions.csproj ./Services/TFEHelper.Backend.Services.Abstractions/
COPY ./Services/TFEHelper.Backend.Services.Contracts/TFEHelper.Backend.Services.Contracts.csproj ./Services/TFEHelper.Backend.Services.Contracts/
COPY ./Services/TFEHelper.Backend.Services.Test/TFEHelper.Backend.Services.Test.csproj ./Services/TFEHelper.Backend.Services.Test/

COPY ./Tools/TFEHelper.Backend.Tools/TFEHelper.Backend.Tools.csproj ./Services/TFEHelper.Backend.Tools/
COPY ./Tools/TFEHelper.Backend.Tools.Test/TFEHelper.Backend.Tools.Test.csproj ./Services/TFEHelper.Backend.Tools.Test/

RUN dotnet restore 
COPY . .
RUN dotnet publish ./API/TFEHelper.Backend.API/TFEHelper.Backend.API.csproj -o /publish/ 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app 
RUN mkdir -p /app/data
COPY --from=build-image /publish .
ENV ASPNETCORE_URLS=http://+:5000 
ENTRYPOINT ["dotnet", "TFEHelper.Backend.API.dll"]