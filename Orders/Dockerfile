FROM microsoft/aspnetcore-build:1.1
ARG source
WORKDIR /app
EXPOSE 5002
COPY . .
ENTRYPOINT ["dotnet", "Orders.dll"]
