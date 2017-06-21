FROM microsoft/aspnetcore-build:1.1
ARG source
WORKDIR /app
EXPOSE 5000
COPY . .
ENTRYPOINT ["dotnet", "API_Gateway.dll"]
