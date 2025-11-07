using Roleover.API.Middlewares;
using Roleover.Application.Services;
using Roleover.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
