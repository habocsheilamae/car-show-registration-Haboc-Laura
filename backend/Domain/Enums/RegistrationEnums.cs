namespace Domain.Enums;

public enum RegistrationType
{
    Exhibitor,
    Competitor,
    VIP
}

public enum RegistrationStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    Cancelled
}

// Computed from AmountPaid vs RegistrationFee, never stored.
public enum PaymentStatus
{
    Unpaid,
    Partial,
    Paid
}