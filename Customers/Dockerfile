FROM microsoft/aspnetcore-build:1.1
ARG source
WORKDIR /app
EXPOSE 5001
COPY . .
ENTRYPOINT ["dotnet", "Customers.dll"]
