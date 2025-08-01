using Inlog.Desafio.Backend.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inlog.Desafio.Backend.Infra.Database.Database.Configurations;

internal sealed class VehicleConfig : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);
        builder
            .HasIndex(v => v.Chassis)
            .IsUnique();
        
        builder.OwnsOne(v => v.Coordinates, coord =>
        {
            coord.Property(c => c.Latitude).HasColumnName("Latitude").IsRequired();
            coord.Property(c => c.Longitude).HasColumnName("Longitude").IsRequired();
        });
        
        builder.HasQueryFilter(v => !v.IsDeleted);
    }
}