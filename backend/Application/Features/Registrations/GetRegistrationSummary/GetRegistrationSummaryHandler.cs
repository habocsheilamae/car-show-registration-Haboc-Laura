namespace Application.Features.Registrations.GetRegistrationSummary;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationSummaryHandler(IAppDbContext db)
    : IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>>
{
    public async Task<Result<GetRegistrationSummaryResponse>> HandleAsync(
        GetRegistrationSummaryQuery query, CancellationToken cancellationToken = default)
    {
        var registrations = db.Registrations.AsNoTracking();

        // Status counts, computed in SQL (cancelled included in the counts)
        var counts = await registrations
            .GroupBy(r => r.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        int CountOf(RegistrationStatus status) =>
            counts.FirstOrDefault(c => c.Status == status)?.Count ?? 0;

        // Money totals exclude cancelled registrations. Only the two money columns are
        // loaded and summed in memory, which avoids SQLite's decimal aggregate limitation.
        var amounts = await registrations
            .Where(r => r.Status != RegistrationStatus.Cancelled)
            .Select(r => new { r.RegistrationFee, r.AmountPaid })
            .ToListAsync(cancellationToken);

        var totalFees = amounts.Sum(a => a.RegistrationFee);
        var totalPayments = amounts.Sum(a => a.AmountPaid);

        return Result<GetRegistrationSummaryResponse>.Success(new GetRegistrationSummaryResponse(
            TotalRegistrations: counts.Sum(c => c.Count),
            Confirmed: CountOf(RegistrationStatus.Confirmed),
            CheckedIn: CountOf(RegistrationStatus.CheckedIn),
            Pending: CountOf(RegistrationStatus.Pending),
            Cancelled: CountOf(RegistrationStatus.Cancelled),
            TotalFees: totalFees,
            TotalPayments: totalPayments,
            UncollectedPayments: totalFees - totalPayments));
    }
}