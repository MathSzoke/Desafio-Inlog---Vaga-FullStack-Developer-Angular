using SharedKernel;

namespace Inlog.Desafio.Backend.Domain.Vehicles;

public static class VehicleError
{
    public static Error VehicleNotFound(Guid vehicleId)
    {
        return Error.NotFound(
            "Vehicles.NotFound",
            $"O veículo com o Id = '{vehicleId}' was not found");
    }
    public static readonly Error NotFoundByChassis = Error.Conflict(
        "Vehicles.NotFoundByChassis",
        "O veículo com o Chassi informado não foi localizado.");
    public static Error VehicleAlreadyLinked(string chassis)
    {
        return Error.Conflict(
            "Vehicles.AlreadyExists",
            $"O veículo com o Chassi ‘{chassis}’ já existe.");
    }

    public static Error LicensePlateAlreadyInUse(string licensePlate)
    {
        return Error.Conflict(
            "Vehicles.AlreadyExists",
            $"O veículo com a placa ‘{licensePlate}’ já existe.");
    }

    public static Error TrackerSerialAlreadyInUse(string trackerSerialNumber)
    {
        return Error.Conflict(
            "Vehicles.AlreadyExists",
            $"O número de série do rastreador ‘{trackerSerialNumber}’ já existe.");
    }
    
    public static Error Unauthorized()
    {
        return Error.Failure(
            "Vehicles.Unauthorized",
            "Você não está autorizado para realizar esta ação.");
    }
}