#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
#RUN apt-get update
# && apt-get install -y apt-utils libgdiplus libc6-dev
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["bohoInvoiceWatermarkRemove/bohoInvoiceWatermarkRemove.csproj", "bohoInvoiceWatermarkRemove/"]
RUN dotnet restore "bohoInvoiceWatermarkRemove/bohoInvoiceWatermarkRemove.csproj"
COPY . .
WORKDIR "/src/bohoInvoiceWatermarkRemove"
RUN dotnet build "bohoInvoiceWatermarkRemove.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bohoInvoiceWatermarkRemove.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bohoInvoiceWatermarkRemove.dll"]