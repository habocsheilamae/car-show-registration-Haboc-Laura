namespace Application.Features.Registrations.GetRegistrations;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationsHandler(IAppDbContext db)
    : IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>
{
    public async Task<Result<GetRegistrationsResponse>> HandleAsync(
        GetRegistrationsQuery query, CancellationToken cancellationToken = default)
    {
        var registrations = db.Registrations.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.ParticipantName))
        {
            // SQLite LIKE is case-insensitive for ASCII, which covers "juan" vs "JUAN"
            var pattern = $"%{query.ParticipantName.Trim()}%";
            registrations = registrations.Where(r => EF.Functions.Like(r.ParticipantName, pattern));
        }

        if (query.Type is not null)
        {
            var type = Enum.Parse<RegistrationType>(query.Type, ignoreCase: true);
            registrations = registrations.Where(r => r.RegistrationType == type);
        }

        if (query.Status is not null)
        {
            var status = Enum.Parse<RegistrationStatus>(query.Status, ignoreCase: true);
            registrations = registrations.Where(r => r.Status == status);
        }

        var totalCount = await registrations.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

        // PaymentStatus is computed in C# (not a column), so load the page of entities
        // first, then map them in memory.
        var page = await registrations
            .OrderByDescending(r => r.RegistrationDate)
            .ThenByDescending(r => r.RegistrationNumber)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var items = page.Select(r => new RegistrationItem(
            r.Id,
            r.RegistrationNumber,
            r.RegistrationDate,
            r.ParticipantName,
            r.ContactNumber,
            r.Email,
            r.Address,
            r.CarMakeModel,
            r.PlateNumber,
            r.RegistrationType,
            r.Status,
            r.RegistrationFee,
            r.AmountPaid,
            r.PaymentStatus)).ToList();

        return Result<GetRegistrationsResponse>.Success(
            new GetRegistrationsResponse(items, query.Page, query.PageSize, totalCount, totalPages));
    }
}