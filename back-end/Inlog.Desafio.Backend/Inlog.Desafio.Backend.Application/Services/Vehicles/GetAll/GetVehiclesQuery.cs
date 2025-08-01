using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.GetAll;

public sealed record GetVehiclesQuery(
    double UserLatitude,
    double UserLongitude) : IQuery<Result<IEnumerable<Vehicle>>>;