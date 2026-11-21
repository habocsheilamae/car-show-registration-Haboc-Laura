using System.Text.Json.Serialization;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Api.Features.Registrations;
using Application.Features.Registrations.CreateRegistration;
using Application.Features.Registrations.GetRegistrations;
using Application.Features.Registrations.GetRegistrationById;
using Application.Features.Registrations.GetRegistrationSummary;

var builder = WebApplication.CreateBuilder(args);

// Database (SQLite)
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Data Source=carshow.db";
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Validators
builder.Services.AddValidatorsFromAssembly(typeof(IAppDbContext).Assembly);

// Handlers
builder.Services.AddScoped<
    ICommandHandler<CreateRegistrationCommand, Result<CreateRegistrationResponse>>,
    CreateRegistrationHandler>();

builder.Services.AddScoped<
    IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>,
    GetRegistrationsHandler>();

builder.Services.AddScoped<
    IQueryHandler<GetRegistrationByIdQuery, Result<GetRegistrationByIdResponse>>,
    GetRegistrationByIdHandler>();

builder.Services.AddScoped<
    IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>>,
    GetRegistrationSummaryHandler>();
// builder.Services.AddScoped<ICommandHandler<TCommand, Result<TResponse>>, TCommandHandler>();
// builder.Services.AddScoped<IQueryHandler<TQuery, Result<TResponse>>, TQueryHandler>();

// JSON: enums as strings (e.g. "VIP", "CheckedIn")
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// CORS: allow the React dev server
builder.Services.AddCors(options =>
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()));

// API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "Car Show Registration API";
        document.Info.Description = "Vertical Slice API for Car Show 2027 registrations";
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Apply migrations + seed (enable AFTER creating the InitialCreate migration)
using (var scope = app.Services.CreateScope())
{
    var dbContext = (AppDbContext)scope.ServiceProvider.GetRequiredService<IAppDbContext>();
    dbContext.Database.Migrate();
    await DbSeeder.SeedAsync(dbContext);
}

app.UseCors("Frontend");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Root endpoint
app.MapGet("/", () => "Welcome to the Car Show Registration API! Go to /scalar/v1 for API documentation.");

// Map endpoints
app.MapRegistrationEndpoints();

app.Run();