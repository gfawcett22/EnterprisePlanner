FROM microsoft/aspnetcore:1.1
WORKDIR /app
COPY ./publish .
EXPOSE 5001
ENTRYPOINT ["dotnet", "Customers.dll"]
