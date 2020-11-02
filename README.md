# Barrier Management Tool
[![Build Status](https://dev.azure.com/lambdaville/Fusion-BMT/_apis/build/status/equinor.fusion-bmt?branchName=master)](https://dev.azure.com/lambdaville/Fusion-BMT/_build/latest?definitionId=22&branchName=master)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/45edba07b87447489c54a51867141261)](https://www.codacy.com/gh/equinor/fusion-bmt/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=equinor/fusion-bmt&amp;utm_campaign=Badge_Grade)

To run the project with docker-compose use:

* `docker-compose up --build`

### Prerequisites
* [.NET Core 3.1+](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Node 12+ with npm](https://github.com/nodesource/distributions/blob/master/README.md)
* [Docker](https://docs.docker.com/engine/install/)


## Frontend
The frontend is built using TypeScript and components from the Equinor Design System ([EDS](https://eds.equinor.com/components/component-status/)).

### Run frontend
```
cd frontend
npm install
npm start
```

## Backend
The backend is build using .NET Core 3.1. We use GraphQL to handle requests 
to the backend, and [Hot Chocolate](https://github.com/ChilliCream/hotchocolate)
is used as the implementation in .NET.

We are using a Entity Framework SQL database for storing our data.
The environment variable `Database__ConnectionString` can be a ADO.NET connection 
string to an existing database. If empty we use an InMemory database which is 
initialized with dummy data.

### GraphQL schema
When running locally, a playgrond server for trying out GrapQL queries will be
available at [localhost:500/graphql/playground](http://localhost:500/graphql/playground).
This will not work properly production since the playground server will not provide 
bearer token for authentication. For generating a bearer token and try out the 
API, the Swagger URL [localhost:5000/swagger](http://localhost:500/swagger/index.html) can be used.

The Schema used for the models in the backend can be found [here](https://backend-fusion-bmt-dev.radix.equinor.com/graphql/schema).



### Environment variables required:
```
Database__ConnectionString="SECRET"
AzureAd__ClientSecret="SECRET"
```
### Run backend
```
cd backend/api
dotnet run
```


### BMT in Radix
* [dev frontend](https://frontend-fusion-bmt-dev.radix.equinor.com)
* [dev backend](https://backend-fusion-bmt-dev.radix.equinor.com/swagger/index.html)
* [demo frontend](https://frontend-fusion-bmt-prod.radix.equinor.com/)
* [Radix console](https://console.radix.equinor.com/applications/pia)


### Etc
* [Figma drawings](https://www.figma.com/proto/wAzF4PAx9OPOoMGtsaju06/BMT?node-id=1%3A3110&viewport=650%2C493%2C0.052038900554180145&scaling=min-zoom)
* [Microsoft Teams gruppe](https://teams.microsoft.com/_#/conversations/Generelt?threadId=19:bfb40c49b3e2494fa69763c4bcf642a9@thread.tacv2&ctx=channel)


## Configuration

### Backend configuration
* Reset SQL database: `dotnet ef database drop`
* Pupulate DB with questgions: `dotnet ...`



### API structure
