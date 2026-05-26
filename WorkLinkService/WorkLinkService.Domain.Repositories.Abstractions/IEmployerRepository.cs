using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions.Base;

namespace WorkLinkService.Domain.Repositories.Abstractions;

public interface IEmployerRepository : IRepository<Employer, Guid>
{
    // тк email работодателя уникальный
    Task<Employer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}