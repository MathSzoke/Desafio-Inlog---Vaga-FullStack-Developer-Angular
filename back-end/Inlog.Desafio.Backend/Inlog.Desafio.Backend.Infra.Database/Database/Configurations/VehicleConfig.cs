using Inlog.Desafio.Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inlog.Desafio.Backend.Infra.Database.Database.Configurations;

internal sealed class VeiculoConfig : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);
        builder
            .HasIndex(v => v.Chassis)
            .IsUnique();
        builder.HasQueryFilter(v => !v.IsDeleted);
    }
}