using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions.Base;

namespace WorkLinkService.Domain.Repositories.Abstractions;

public interface IExecutorRepository : IRepository<Executor, Guid>
{
    // получить сохранённые вакансии исполнителя
    Task<IEnumerable<Job>> GetSavedJobsAsync(Guid executorId, CancellationToken cancellationToken, bool asNoTracking = false);
}