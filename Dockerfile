FROM microsoft/dotnet:1.0.0-preview2-sdk

ENV workingdir /onlinestore
COPY . $workingdir
WORKDIR $workingdir
RUN dotnet restore

RUN dotnet test test/OnlineStore.Tests

EXPOSE 5000

ENTRYPOINT dotnet run -p src/OnlineStore.RestApi