FROM microsoft/aspnetcore:1.1
WORKDIR /app
COPY ./publish .
EXPOSE 5002
ENTRYPOINT ["dotnet", "Orders.dll"]
