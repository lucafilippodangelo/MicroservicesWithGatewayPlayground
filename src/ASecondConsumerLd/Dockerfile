#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/ASecondConsumerLd/ASecondConsumerLd.csproj", "src/ASecondConsumerLd/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
RUN dotnet restore "src/ASecondConsumerLd/ASecondConsumerLd.csproj"
COPY . .
WORKDIR "/src/src/ASecondConsumerLd"
RUN dotnet build "ASecondConsumerLd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASecondConsumerLd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASecondConsumerLd.dll"]
