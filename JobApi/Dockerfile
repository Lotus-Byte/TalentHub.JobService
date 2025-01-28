FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
USER $APP_UID
WORKDIR /app


# build
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /TalentHub.JobService
#COPY ["src/HappyDiary.WebApi/HappyDiary.WebApi.csproj", "src/HappyDiary.WebApi/"]
COPY ["./JobApi/JobApi.csproj", "TalentHub.JobService/JobApi/"]
COPY ["./JobBll/JobBll.csproj", "TalentHub.JobService/JobBll"]
COPY ["./JobDataAccess/JobDataAccess.csproj", "TalentHub.JobService/JobDataAccess/"]
RUN dotnet restore "TalentHub.JobService/JobApi/JobApi.csproj"
COPY . .
WORKDIR "/TalentHub.JobService/JobApi"
RUN dotnet build "JobApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JobApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5260
ENTRYPOINT ["dotnet", "JobApi.dll"]
