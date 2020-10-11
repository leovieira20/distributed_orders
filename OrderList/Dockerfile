FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj ./Common/Common.Messaging.RabbitMq/Common.Messaging.RabbitMq.csproj
COPY OrderList/OrderList.Domain/OrderList.Domain.csproj ./OrderList/OrderList.Domain/OrderList.Domain.csproj
COPY OrderList/OrderList.Repository.Redis/OrderList.Repository.Redis.csproj ./OrderList/OrderList.Repository.Redis/OrderList.Repository.Redis.csproj
COPY OrderList/OrderList.Client.Web/OrderList.Client.Web.csproj ./OrderList/OrderList.Client.Web/OrderList.Client.Web.csproj

RUN dotnet new sln -n Solution
RUN dotnet sln add OrderList/OrderList.Domain/OrderList.Domain.csproj
RUN dotnet sln add OrderList/OrderList.Repository.Redis/OrderList.Repository.Redis.csproj
RUN dotnet sln add OrderList/OrderList.Client.Web/OrderList.Client.Web.csproj

RUN dotnet restore

# Copy everything else and build
COPY Common/Common.Messaging.RabbitMq/ ./Common/Common.Messaging.RabbitMq/
COPY OrderList/OrderList.Domain/ ./OrderList/OrderList.Domain/
COPY OrderList/OrderList.Repository.Redis/ ./OrderList/OrderList.Repository.Redis/
COPY OrderList/OrderList.Client.Web/ ./OrderList/OrderList.Client.Web/

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "OrderList.Client.Web.dll"]