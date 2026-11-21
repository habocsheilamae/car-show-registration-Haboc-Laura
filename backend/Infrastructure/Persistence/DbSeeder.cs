namespace Infrastructure.Persistence;

using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    private static readonly (string Name, RegistrationType Type, RegistrationStatus Status, decimal Paid)[] Rows =
    [
        ("Juanito Reyes",       RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m),
        ("Maria Dela Rosa",     RegistrationType.Exhibitor,  RegistrationStatus.Pending,   0m),
        ("Carlo Mendoza",       RegistrationType.VIP,        RegistrationStatus.CheckedIn, 5000m),
        ("Ana Villanueva",      RegistrationType.Competitor, RegistrationStatus.Pending,   1000m),
        ("Rico Santos",         RegistrationType.Exhibitor,  RegistrationStatus.Confirmed, 1500m),
        ("Liza Bonifacio",      RegistrationType.VIP,        RegistrationStatus.Confirmed, 2500m),
        ("Mark Gonzales",       RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
        ("Jessa Navarro",       RegistrationType.Exhibitor,  RegistrationStatus.Cancelled, 500m),
        ("Paolo Aquino",        RegistrationType.VIP,        RegistrationStatus.Pending,   0m),
        ("Grace Ocampo",        RegistrationType.Competitor, RegistrationStatus.Confirmed, 1500m),
        ("Ramon Del Rosario",   RegistrationType.Exhibitor,  RegistrationStatus.CheckedIn, 1500m),
        ("Kristine Bautista",   RegistrationType.VIP,        RegistrationStatus.Cancelled, 5000m),
        ("Dennis Alcantara",    RegistrationType.Competitor, RegistrationStatus.Pending,   0m),
        ("Angelica Ramos",      RegistrationType.Exhibitor,  RegistrationStatus.Confirmed, 750m),
        ("Joel Mercado",        RegistrationType.VIP,        RegistrationStatus.CheckedIn, 3000m),
        ("Sheila Tolentino",    RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m),
        ("Vincent Cruz",        RegistrationType.Exhibitor,  RegistrationStatus.Pending,   1500m),
        ("Ana Marie Lim",       RegistrationType.Competitor, RegistrationStatus.Cancelled, 0m),
        ("Bryan Dizon",         RegistrationType.VIP,        RegistrationStatus.Confirmed, 5000m),
        ("Teresa Manalo",       RegistrationType.Exhibitor,  RegistrationStatus.CheckedIn, 1000m),
        ("Juan Carlos Ibarra",  RegistrationType.Competitor, RegistrationStatus.Pending,   500m),
        ("Nina Cabrera",        RegistrationType.VIP,        RegistrationStatus.Pending,   2000m),
        ("Edgar Salvador",      RegistrationType.Exhibitor,  RegistrationStatus.Confirmed, 0m),
        ("Hazel Pineda",        RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
        ("Leo Fernandez",       RegistrationType.Exhibitor,  RegistrationStatus.Pending,   0m),
    ];

    private static readonly string[] Cars =
    [
        "Toyota Supra MK4", "Nissan Skyline R34", "Honda Civic EK9", "Mazda RX-7 FD",
        "Mitsubishi Lancer Evo IX", "Subaru WRX STI", "Toyota AE86 Trueno", "Honda S2000",
        "Nissan 370Z", "Ford Mustang GT"
    ];

    private static readonly string[] Cities =
        ["Sorsogon City", "Naga City", "Legazpi City", "Irosin", "Bulan"];

    private static readonly string[] PlatePrefixes = ["ABC", "XYZ", "SOR", "BIC", "NAG"];

    public static async Task SeedAsync(AppDbContext db)
    {
        // Run only on an empty table so a restart never adds the seed twice
        if (await db.Registrations.AnyAsync())
        {
            return;
        }

        // Fixed past dates, so new registrations always sort above the seed data
        var baseDate = new DateTime(2026, 10, 20, 8, 30, 0, DateTimeKind.Utc);
        var seeded = new List<Registration>();

        for (var i = 0; i < Rows.Length; i++)
        {
            var row = Rows[i];
            var n = i + 1;

            var registration = Registration.Create(
                registrationNumber: $"CS27-{n:D4}",
                participantName: row.Name,
                contactNumber: $"0917{1000000 + n * 1111:D7}",
                email: $"{row.Name.ToLowerInvariant().Replace(' ', '.')}@example.com",
                address: $"{100 + n} Rizal St., {Cities[i % Cities.Length]}",
                carMakeModel: Cars[i % Cars.Length],
                plateNumber: $"{PlatePrefixes[i % PlatePrefixes.Length]} {1000 + n}",
                type: row.Type,
                amountPaid: row.Paid);

            registration.SetRegistrationDate(baseDate.AddDays(i).AddHours(i % 6));
            registration.SetStatus(row.Status);
            seeded.Add(registration);
        }

        db.Registrations.AddRange(seeded);
        await db.SaveChangesAsync();
    }
}