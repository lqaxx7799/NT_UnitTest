FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY ../../Common/Nutrition.Business ../../Common/Nutrition.Business/
COPY ../../Common/Nutrition.Library ../../Common/Nutrition.Library/
COPY . ./
# Restore as distinct layer
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80
ENTRYPOINT ["dotnet", "Nutrition.APIs.dll"]