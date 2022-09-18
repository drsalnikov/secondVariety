FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# copy csproj and restore as distinct layers
COPY . .

#RUN dotnet add package Microsoft.EntityFrameworkCore.Analyzers --version 5.0.14
# 
#RUN dotnet add package Microsoft.EntityFrameworkCore.Relational --version 6.0.8
#RUN dotnet add package Npgsql --version 6.0.6
#RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL  --version 6.0.6
#RUN dotnet add package Plotly.NET  --version 3.0.0
#RUN dotnet add package Plotly.NET.ImageExport --version 3.0.0
#RUN dotnet add package PuppeteerSharp  --version 7.1.0
#RUN dotnet add package Google.Protobuf  --version 3.21.5 
#RUN dotnet add package Google.Protobuf.Tools  --version 3.21.5
#RUN dotnet add package Grpc.AspNetCore --version 2.40.0 
#RUN dotnet add package IdentityModel --version 6.0.0
#RUN dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.8
#RUN dotnet add package Microsoft.EntityFrameworkCore --version 6.0.8
#RUN dotnet add package Microsoft.EntityFrameworkCore.Abstractions --version 6.0.8
#RUN dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.8
#RUN dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version  6.0.8
#RUN dotnet add package Microsoft.IdentityModel.Tokens --version 6.22.1
#RUN dotnet add package System.IdentityModel.Tokens.Jwt --version 6.22.1
RUN dotnet restore
#
## copy everything else and build app
#COPY . ./
##
RUN dotnet publish -c Release -o /app 
WORKDIR /app
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
ENTRYPOINT ["dotnet", "/app/grpcserv1.dll"]