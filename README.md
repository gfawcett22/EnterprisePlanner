# EnterprisePlanner

### Generic enterprise application following a microservices architecture.

## Front end [/App](https://github.com/gfawcett22/EnterprisePlanner/tree/master/App)
Built using Angular. Directions for running are in its readme.

## Microservices
Built using ASP.NET Core Web API. To run, use Visual Studio. Docker/Kubernetes is not currently configured, so just run the projects locally and test using an API endpoint testing tool such as Postman.

## Migrating databases.
Each microservice is supposed to own its datastore. As a result, each microservice needs it's own database. Change the connection string in appsettings.json in each project to your local database. To migrate your database, open Package Mangement Console in visual studio. For each project, run `Add-Migration [name of migration]` followed by `Update-Database` to use Entity Framework code first to initialize your database. 
