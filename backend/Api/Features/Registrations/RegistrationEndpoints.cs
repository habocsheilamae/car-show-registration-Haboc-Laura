namespace Api.Features.Registrations;

using Api.Common.Extensions;
using Api.Common.Filters;
using Application.Abstractions.Messaging;
using Application.Features.Registrations.CreateRegistration;
using Application.Features.Registrations.GetRegistrationById;
using Application.Features.Registrations.GetRegistrations;
using Application.Features.Registrations.GetRegistrationSummary;
using Domain.Common;

public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registrations").WithTags("Registrations");

        group.MapPost("/", async (
                CreateRegistrationCommand command,
                ICommandHandler<CreateRegistrationCommand, Result<CreateRegistrationResponse>> handler,
                CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(command, ct);
            return result.IsSuccess
                ? Results.Created($"/api/registrations/{result.Value!.Id}", result.Value)
                : result.ToProblemDetails();
        })
            .AddEndpointFilter<ValidationFilter<CreateRegistrationCommand>>()
            .WithName("CreateRegistration")
            .WithSummary("Make a new registration");

        group.MapGet("/", async (
                [AsParameters] GetRegistrationsQuery query,
                IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>> handler,
                CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(query, ct);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
            .AddEndpointFilter<ValidationFilter<GetRegistrationsQuery>>()
            .WithName("GetRegistrations")
            .WithSummary("Show all registrations (paged, filterable)");

        group.MapGet("/summary", async (
                IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>> handler,
                CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(new GetRegistrationSummaryQuery(), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
            .WithName("GetRegistrationSummary")
            .WithSummary("KPI totals for the dashboard");

        group.MapGet("/{id:guid}", async (
                Guid id,
                IQueryHandler<GetRegistrationByIdQuery, Result<GetRegistrationByIdResponse>> handler,
                CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(new GetRegistrationByIdQuery(id), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
            .WithName("GetRegistrationById")
            .WithSummary("Find registration by ID");

        return app;
    }
}