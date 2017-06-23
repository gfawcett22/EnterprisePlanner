FROM microsoft/aspnetcore:1.1
WORKDIR /app
COPY ./publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "API_Gateway.dll"]
