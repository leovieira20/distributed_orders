FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj ./Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj
COPY OrderManagement/OrderManagement.Domain/OrderManagement.Domain.csproj ./OrderManagement/OrderManagement.Domain/OrderManagement.Domain.csproj
COPY OrderManagement/OrderManagement.Repository.Mongo/OrderManagement.Repository.Mongo.csproj ./OrderManagement/OrderManagement.Repository.Mongo/OrderManagement.Repository.Mongo.csproj
COPY OrderManagement/OrderManagement.Client.Web/OrderManagement.Client.Web.csproj ./OrderManagement/OrderManagement.Client.Web/OrderManagement.Client.Web.csproj

RUN dotnet new sln -n Solution
RUN dotnet sln add Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj
RUN dotnet sln add OrderManagement/OrderManagement.Domain/OrderManagement.Domain.csproj
RUN dotnet sln add OrderManagement/OrderManagement.Repository.Mongo/OrderManagement.Repository.Mongo.csproj
RUN dotnet sln add OrderManagement/OrderManagement.Client.Web/OrderManagement.Client.Web.csproj

RUN dotnet restore

# Copy everything else and build
COPY Common/Common.Messaging.RabbitMq/ ./Common/Common.Messaging.RabbitMq/
COPY OrderManagement/OrderManagement.Domain/ ./OrderManagement/OrderManagement.Domain/
COPY OrderManagement/OrderManagement.Repository.Mongo/ ./OrderManagement/OrderManagement.Repository.Mongo/
COPY OrderManagement/OrderManagement.Client.Web/ ./OrderManagement/OrderManagement.Client.Web/

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "OrderManagement.Client.Web.dll"]