using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions.Base;

namespace WorkLinkService.Domain.Repositories.Abstractions;

public interface IJobRepository : IRepository<Job, Guid>
{
    // получить все активные вакансии
    Task<IEnumerable<Job>> GetAllActiveAsync(CancellationToken cancellationToken, bool asNoTracking = false);
    // фильтрация по хэштегу
    Task<IEnumerable<Job>> GetByHashtagAsync(string hashtagName, CancellationToken cancellationToken, bool asNoTracking = false);
    // все вакансии конкретного работодателя
    Task<IEnumerable<Job>> GetByEmployerIdAsync(Guid employerId, CancellationToken cancellationToken, bool asNoTracking = false);
}