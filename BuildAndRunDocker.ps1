Set-ExecutionPolicy RemoteSigned

docker-compose down

dotnet build ./EnterprisePlanner.sln

cd "./API_Gateway"

dotnet publish -o publish

cd "../Customers"

dotnet publish -o publish

cd "../Orders"

dotnet publish -o publish

cd "../"

docker-compose build

docker-compose up
