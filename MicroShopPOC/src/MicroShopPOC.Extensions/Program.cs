/*
 * This project was created as a ClassLibrary, but it is not a library, it is a web application. So we need to change the project type to WebApplication.
 * For this reason I need to add Program.cs file to the project and chang *.csproj.
 * from `<Project Sdk="Microsoft.NET.Sdk">` to `<Project Sdk="Microsoft.NET.Sdk.Web">`.
 * Do not remove this program.cs file, because it is needed to use class WebApplication from `Microsoft.AspNetCore.Builder`.
*/
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.Run();
