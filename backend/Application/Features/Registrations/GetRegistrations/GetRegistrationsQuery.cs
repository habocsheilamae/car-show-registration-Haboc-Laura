namespace Application.Features.Registrations.GetRegistrations;

using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;

// Type and Status are strings so invalid values reach the validator and return a
// ProblemDetails 400 (instead of a generic model-binding error).
public sealed record GetRegistrationsQuery(
    string? ParticipantName = null,
    string? Type = null,
    string? Status = null,
    int Page = 1,
    int PageSize = 10) : IQuery<Result<GetRegistrationsResponse>>;

public sealed record GetRegistrationsResponse(
    IReadOnlyList<RegistrationItem> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);

public sealed record RegistrationItem(
    Guid Id,
    string RegistrationNumber,
    DateTime RegistrationDate,
    string ParticipantName,
    string ContactNumber,
    string Email,
    string Address,
    string CarMakeModel,
    string PlateNumber,
    RegistrationType RegistrationType,
    RegistrationStatus Status,
    decimal RegistrationFee,
    decimal AmountPaid,
    PaymentStatus PaymentStatus);