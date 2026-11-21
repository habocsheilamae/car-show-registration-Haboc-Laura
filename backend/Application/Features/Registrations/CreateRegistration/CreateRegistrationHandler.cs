namespace Application.Features.Registrations.CreateRegistration;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class CreateRegistrationHandler(IAppDbContext db)
    : ICommandHandler<CreateRegistrationCommand, Result<CreateRegistrationResponse>>
{
    private const string Prefix = "CS27-";

    public async Task<Result<CreateRegistrationResponse>> HandleAsync(
        CreateRegistrationCommand command, CancellationToken cancellationToken = default)
    {
        var plate = command.PlateNumber.Trim().ToUpperInvariant();

        if (await db.Registrations.AnyAsync(r => r.PlateNumber == plate, cancellationToken))
        {
            return Result<CreateRegistrationResponse>.Failure(
                Error.Conflict("Registration.Conflict",
                    $"A registration with plate number '{plate}' already exists."));
        }

        // Next sequential number: CS27-0001, CS27-0002, ...
        var last = await db.Registrations
            .OrderByDescending(r => r.RegistrationNumber)
            .Select(r => r.RegistrationNumber)
            .FirstOrDefaultAsync(cancellationToken);
        var next = last is null ? 1 : int.Parse(last[Prefix.Length..]) + 1;

        var registration = Registration.Create(
            $"{Prefix}{next:D4}",
            command.ParticipantName.Trim(),
            command.ContactNumber.Trim(),
            command.Email.Trim(),
            command.Address.Trim(),
            command.CarMakeModel.Trim(),
            plate,
            command.RegistrationType,
            command.AmountPaid);

        db.Registrations.Add(registration);
        await db.SaveChangesAsync(cancellationToken);

        return Result<CreateRegistrationResponse>.Success(new CreateRegistrationResponse(
            registration.Id,
            registration.RegistrationNumber,
            registration.RegistrationDate,
            registration.ParticipantName,
            registration.ContactNumber,
            registration.Email,
            registration.Address,
            registration.CarMakeModel,
            registration.PlateNumber,
            registration.RegistrationType,
            registration.Status,
            registration.RegistrationFee,
            registration.AmountPaid,
            registration.PaymentStatus));
    }
}