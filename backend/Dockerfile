FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /source

COPY . .

WORKDIR /source/api
RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained true /p:PublishTrimmed=true /p:PublishReadyToRun=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["./api", "--urls=http://0.0.0.0:5000"]

EXPOSE 5000
