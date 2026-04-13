using WorkLink.Domain.Entities;

namespace WorkLink.Domain.Interfaces;

public interface IJobRepository
{
    Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Job>> GetActiveJobsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Job>> GetByEmployerIdAsync(Guid employerId, CancellationToken cancellationToken = default);
    Task AddAsync(Job job, CancellationToken cancellationToken = default);
    Task UpdateAsync(Job job, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}