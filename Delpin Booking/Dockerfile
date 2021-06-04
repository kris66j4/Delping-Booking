#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Delpin Booking/Delpin Booking.csproj", "Delpin Booking/"]
RUN dotnet restore "Delpin Booking/Delpin Booking.csproj"
COPY . .
WORKDIR "/src/Delpin Booking"
RUN dotnet build "Delpin Booking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delpin Booking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delpin Booking.dll"]
