FROM microsoft/aspnetcore:1.1
WORKDIR /app
COPY ./publish .
EXPOSE 5003
ENTRYPOINT ["dotnet", "Shipments.dll"]