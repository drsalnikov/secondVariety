#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

RUN rm -rf /app/*
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN rm -rf /src/*
COPY . .


RUN dotnet restore
RUN dotnet publish -c Release -o /app 
RUN cp -rf /src/cert /app
WORKDIR /
RUN rm -rf /src
WORKDIR /app
#RUN dotnet dev-certs https
#RUN dotnet dev-certs https --trust
ENTRYPOINT ["dotnet", "SecondVariety.dll"]