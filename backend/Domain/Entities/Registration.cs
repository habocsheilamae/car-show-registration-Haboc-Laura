using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Registration : BaseEntity
{
    public string RegistrationNumber { get; private set; } = string.Empty;
    public DateTime RegistrationDate { get; private set; }

    public string ParticipantName { get; private set; } = string.Empty;
    public string ContactNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string CarMakeModel { get; private set; } = string.Empty;
    public string PlateNumber { get; private set; } = string.Empty;

    public RegistrationType RegistrationType { get; private set; }
    public RegistrationStatus Status { get; private set; } = RegistrationStatus.Pending;

    public decimal RegistrationFee { get; private set; }
    public decimal AmountPaid { get; private set; }

    // Computed, not stored
    public PaymentStatus PaymentStatus =>
        AmountPaid <= 0 ? PaymentStatus.Unpaid
        : AmountPaid < RegistrationFee ? PaymentStatus.Partial
        : PaymentStatus.Paid;

    private Registration() { } // for EF Core

    public static decimal GetFee(RegistrationType type) => type switch
    {
        RegistrationType.Exhibitor => 1500m,
        RegistrationType.Competitor => 2500m,
        RegistrationType.VIP => 5000m,
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    public static Registration Create(
        string registrationNumber,
        string participantName,
        string contactNumber,
        string email,
        string address,
        string carMakeModel,
        string plateNumber,
        RegistrationType type,
        decimal amountPaid)
    {
        return new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationNumber = registrationNumber,
            RegistrationDate = DateTime.UtcNow,
            ParticipantName = participantName,
            ContactNumber = contactNumber,
            Email = email,
            Address = address,
            CarMakeModel = carMakeModel,
            PlateNumber = plateNumber,
            RegistrationType = type,
            Status = RegistrationStatus.Pending,
            RegistrationFee = GetFee(type),
            AmountPaid = amountPaid
        };
    }

    public void SetStatus(RegistrationStatus status) => Status = status;
    public void SetRegistrationDate(DateTime utc) =>
    RegistrationDate = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
}