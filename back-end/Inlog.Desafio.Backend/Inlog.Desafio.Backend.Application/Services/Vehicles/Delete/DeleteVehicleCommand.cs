using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Delete;

public sealed record DeleteVehicleCommand(Guid Id) : ICommand;