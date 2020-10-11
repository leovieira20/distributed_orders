FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj ./Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj
COPY ProductInventory/ProductInventory.Domain/ProductInventory.Domain.csproj ./ProductInventory/ProductInventory.Domain/ProductInventory.Domain.csproj
COPY ProductInventory/ProductInventory.Repository.Mongo/ProductInventory.Repository.Mongo.csproj ./ProductInventory/ProductInventory.Repository.Mongo/ProductInventory.Repository.Mongo.csproj
COPY ProductInventory/ProductInventory.Client.Web/ProductInventory.Client.Web.csproj ./ProductInventory/ProductInventory.Client.Web/ProductInventory.Client.Web.csproj

RUN dotnet new sln -n Solution
RUN dotnet sln add Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj
RUN dotnet sln add ProductInventory/ProductInventory.Domain/ProductInventory.Domain.csproj
RUN dotnet sln add ProductInventory/ProductInventory.Repository.Mongo/ProductInventory.Repository.Mongo.csproj
RUN dotnet sln add ProductInventory/ProductInventory.Client.Web/ProductInventory.Client.Web.csproj

RUN dotnet restore

# Copy everything else and build
COPY Common/Common.Messaging.RabbitMq/ ./Common/Common.Messaging.RabbitMq/
COPY ProductInventory/ProductInventory.Domain/ ./ProductInventory/ProductInventory.Domain/
COPY ProductInventory/ProductInventory.Repository.Mongo/ ./ProductInventory/ProductInventory.Repository.Mongo/
COPY ProductInventory/ProductInventory.Client.Web/ ./ProductInventory/ProductInventory.Client.Web/

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ProductInventory.Client.Web.dll"]