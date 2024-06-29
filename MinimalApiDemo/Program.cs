using Asp.Versioning.Conventions;
using MinimalApiDemo.CrossCutting;
using MinimalApiDemo.Endpoints.Account;
using MinimalApiDemo.Endpoints.Orders;
using MinimalApiDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDemo(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Version 1.0");
        c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "Version 2.0");
    });
}

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1.0)
    .HasApiVersion(2.0)
    .Build();

app.UseCors();
app.UseExceptionHandler(opt => { });
app.UseQuery();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.AddOrderEndPoints();
app.AddAcountEndpoints();
app.MapHealthChecks("/health");

app.Run();

//app.MapGet("/version", () => "Hello version 1").WithApiVersionSet(versionSet).MapToApiVersion(1.0);
//app.MapGet("/version", () => "Hello version 2").WithApiVersionSet(versionSet).MapToApiVersion(2.0);
//app.MapGet("/version2only", () => "Hello version 2 only").WithApiVersionSet(versionSet).MapToApiVersion(2.0);
//app.MapGet("/versionneutral", () => "Hello neutral version").WithApiVersionSet(versionSet).IsApiVersionNeutral();

//app.MapGroup("/order").
//    OrderGroupBuilderV1(versionSet).
//    WithTags("v1");
