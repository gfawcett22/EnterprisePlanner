Set-ExecutionPolicy RemoteSigned

docker-compose down



dotnet build ./EnterprisePlanner.sln

cd "./API_Gateway"

rm -r publish

dotnet publish -o publish

cd "../Customers"

rm -r publish

dotnet publish -o publish

cd "../Orders"

rm -r publish

dotnet publish -o publish

cd "../"

docker-compose build --no-cache

docker-compose up
