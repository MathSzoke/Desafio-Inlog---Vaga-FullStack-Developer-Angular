using System.ComponentModel.Design;
using Inlog.Desafio.Backend.Application.Services.Vehicles.GetAll;
using Inlog.Desafio.Backend.Application.Services.Vehicles.Register;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Inlog.Desafio.Backend.Infra.Database.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel.CurrentUser;
using SharedKernel.DomainEvents;
using Shouldly;

namespace Inlog.Desafio.Backend.Test.Vehicles;

public class VehicleTests
{
    private static ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var fakeCurrentUser = new CurrentUserContext();
        var domainDispatcherMock = new Mock<IDomainEventsDispatcher>();

        return new ApplicationDbContext(options, fakeCurrentUser, domainDispatcherMock.Object);
    }

    [Fact]
    public async Task GetVehiclesQueryHandler_Should_Return_List_Of_Vehicles()
    {
        // Arrange
        await using var context = CreateInMemoryContext();
        
        const double userLatitude = 1.0;
        const double userLongitude = 1.0;

        context.Vehicles.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Chassis = "9BWZZZ377VT004251",
            VehicleType = VehicleType.Bus,
            Color = "#FF0000",
            Identifier = "Vehicle 1",
            LicensePlate = "AAA-1A23",
            TrackerSerialNumber = "A123456",
            Coordinates = new Coordinates { Latitude = -25.4, Longitude = -49.2 }
        });

        await context.SaveChangesAsync();

        var handler = new GetVehiclesQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetVehiclesQuery(userLatitude, userLongitude), CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Count().ShouldBe(1);
        result.Value.First().Chassis.ShouldBe("9BWZZZ377VT004251");
    }

    [Fact]
    public async Task GetVehiclesQueryHandler_Should_Return_Empty_When_No_Vehicles()
    {
        // Arrange
        await using var context = CreateInMemoryContext();

        var handler = new GetVehiclesQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetVehiclesQuery(0, 0), CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Count().ShouldBe(0);
    }

    [Fact]
    public async Task RegisterVehicleCommandHandler_Should_Create_Vehicle_Successfully()
    {
        // Arrange
        await using var context = CreateInMemoryContext();

        var handler = new RegisterVehicleCommandHandler(context);

        var command = new RegisterVehicleCommand(
            Chassis: "9BWZZZ377VT004251",
            VehicleType: VehicleType.Truck,
            Color: "#00FF00",
            Identifier: "Truck 1",
            LicensePlate: "BBB-2B34",
            TrackerSerialNumber: "B1234567",
            Coordinates: new Coordinates
            {
                Latitude = -22.9,
                Longitude = -43.2   
            }
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        var created = await context.Vehicles.FindAsync(result.Value.Id);
        created.ShouldNotBeNull();
        created.Chassis.ShouldBe("9BWZZZ377VT004251");
    }
    
    [Fact]
    public async Task UpdateVehicleCommandHandler_Should_Update_Vehicle_Successfully()
    {
        // Arrange
        await using var context = CreateInMemoryContext();

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Chassis = "ORIGINAL",
            VehicleType = VehicleType.Truck,
            Color = "#123456",
            Identifier = "Original",
            LicensePlate = "OLD-0001",
            TrackerSerialNumber = "TRACK123",
            Coordinates = new Coordinates { Latitude = 10, Longitude = 10 }
        };
        context.Vehicles.Add(vehicle);
        await context.SaveChangesAsync();

        var handler = new Inlog.Desafio.Backend.Application.Services.Vehicles.Update.UpdateVehicleCommandHandler(context);

        var command = new Inlog.Desafio.Backend.Application.Services.Vehicles.Update.UpdateVehicleCommand(
            Id: vehicle.Id,
            Chassis: "UPDATED",
            VehicleType: (int)VehicleType.Bus,
            Color: "#654321",
            Identifier: "Updated",
            LicensePlate: "NEW-9999",
            TrackerSerialNumber: "TRACK999",
            Coordinates: new Coordinates { Latitude = 20, Longitude = 30 }
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        var updated = await context.Vehicles.FindAsync(vehicle.Id);
        updated.ShouldNotBeNull();
        updated.Chassis.ShouldBe("UPDATED");
        updated.VehicleType.ShouldBe(VehicleType.Bus);
        updated.Color.ShouldBe("#654321");
        updated.Identifier.ShouldBe("Updated");
        updated.LicensePlate.ShouldBe("NEW-9999");
        updated.TrackerSerialNumber.ShouldBe("TRACK999");
        updated.Coordinates.Latitude.ShouldBe(20);
        updated.Coordinates.Longitude.ShouldBe(30);
    }
    
    [Fact]
    public async Task DeleteVehicleCommandHandler_Should_SoftDelete_Vehicle()
    {
        // Arrange
        await using var context = CreateInMemoryContext();

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Chassis = "SOFTDEL",
            VehicleType = VehicleType.Bus,
            Color = "#CCCCCC",
            Identifier = "ToDelete",
            LicensePlate = "DEL-1234",
            TrackerSerialNumber = "TODEL123",
            Coordinates = new Coordinates { Latitude = 11, Longitude = 22 }
        };
        context.Vehicles.Add(vehicle);
        await context.SaveChangesAsync();

        var handler = new Inlog.Desafio.Backend.Application.Services.Vehicles.Delete.DeleteVehicleCommandHandler(context);
        var command = new Inlog.Desafio.Backend.Application.Services.Vehicles.Delete.DeleteVehicleCommand(vehicle.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        var stillExists = await context.Vehicles.FindAsync(vehicle.Id);
        stillExists.ShouldNotBeNull();
        stillExists.IsDeleted.ShouldBeTrue();
    }
}
