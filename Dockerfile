FROM mcr.microsoft.com/dotnet/sdk:7.0.101-jammy-arm64v8 AS buildimg
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY . .
WORKDIR /app/src/Server
RUN dotnet publish -c  Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:7.0.1-jammy-arm64v8
WORKDIR output
COPY --from=buildimg /app/src/Server/output .
ENTRYPOINT ["dotnet","Server.dll"]
