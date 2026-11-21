namespace Application.Features.Registrations.GetRegistrations;

using Domain.Enums;
using FluentValidation;

public sealed class GetRegistrationsValidator : AbstractValidator<GetRegistrationsQuery>
{
    public GetRegistrationsValidator()
    {
        RuleFor(x => x.ParticipantName).MaximumLength(150);

        RuleFor(x => x.Type)
            .Must(BeValid<RegistrationType>)
            .When(x => x.Type is not null)
            .WithMessage("Type must be one of: Exhibitor, Competitor, VIP.");

        RuleFor(x => x.Status)
            .Must(BeValid<RegistrationStatus>)
            .When(x => x.Status is not null)
            .WithMessage("Status must be one of: Pending, Confirmed, CheckedIn, Cancelled.");

        RuleFor(x => x.Page).GreaterThanOrEqualTo(1)
            .WithMessage("Page must be 1 or more.");

        RuleFor(x => x.PageSize).InclusiveBetween(1, 50)
            .WithMessage("Page size must be between 1 and 50.");
    }

    // Names only: rejects numeric strings like "1" that Enum.TryParse would accept
    internal static bool BeValid<TEnum>(string? value) where TEnum : struct, Enum =>
        value is not null
        && !int.TryParse(value, out _)
        && Enum.TryParse<TEnum>(value, ignoreCase: true, out var parsed)
        && Enum.IsDefined(parsed);
}