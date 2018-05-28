FROM microsoft/aspnetcore-build:2.0 as build-env
WORKDIR /code

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o ./artifact

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /code/artifact .
EXPOSE 80

ENTRYPOINT [ "dotnet", "ProductApi.dll" ]