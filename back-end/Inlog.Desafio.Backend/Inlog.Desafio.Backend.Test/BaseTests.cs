using System.Reflection;
using System.Windows.Input;
using Inlog.Desafio.Backend.WebApi;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Inlog.Desafio.Backend.Application.Abstractions.Data;

namespace Inlog.Desafio.Backend.Test;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Vehicle).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(IApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}