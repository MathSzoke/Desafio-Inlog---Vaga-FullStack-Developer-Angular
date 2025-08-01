using FluentValidation;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Register;

internal sealed class RegisterVehicleCommandValidator : AbstractValidator<RegisterVehicleCommand>
{
    public RegisterVehicleCommandValidator()
    {
        RuleFor(v => v.Identifier)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.Chassis)
            .NotEmpty()
            .Length(17) // padrão internacional de chassi (VIN)
            .Matches("^[A-HJ-NPR-Z0-9]{17}$")
            .WithMessage("Chassi inválido. Deve conter exatamente 17 caracteres alfanuméricos, sem I, O ou Q.");

        RuleFor(v => v.LicensePlate)
            .NotEmpty()
            .Matches("^[A-Z]{3}-[0-9][A-Z0-9][0-9]{2}$")
            .WithMessage("Placa inválida. Formato esperado: AAA-9A99");

        RuleFor(v => v.TrackerSerialNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(v => v.VehicleType)
            .IsInEnum()
            .WithMessage("Tipo de veículo inválido.");

        RuleFor(v => v.Color)
            .NotEmpty()
            .MaximumLength(7)
            .Matches("^#[0-9A-Fa-f]{6}$")
            .WithMessage("A cor deve estar no formato hexadecimal (ex: #FFFFFF).");

        RuleFor(v => v.Coordinates.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude inválida. Deve estar entre -90 e 90.");

        RuleFor(v => v.Coordinates.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude inválida. Deve estar entre -180 e 180.");
    }
}