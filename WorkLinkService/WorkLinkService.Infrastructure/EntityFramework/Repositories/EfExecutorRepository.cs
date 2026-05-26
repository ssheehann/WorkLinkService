using Microsoft.EntityFrameworkCore;
using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions;
using WorkLinkService.Infrastructure.EntityFramework;

namespace WorkLinkService.Infrastructure.EntityFramework.Repositories;

public class EfExecutorRepository(ApplicationDbContext context)
    : EfRepository<Executor, Guid>(context), IExecutorRepository
{
    private readonly DbSet<Executor> _executors = context.Set<Executor>();

    public override Task<Executor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _executors.Include("_savedJobs").FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task<IEnumerable<Job>> GetSavedJobsAsync(Guid executorId, CancellationToken cancellationToken, bool asNoTracking = false)
        => Task.FromResult<IEnumerable<Job>>(
            (asNoTracking ? _executors.AsNoTracking() : _executors)
            .Include("_savedJobs")
            .Where(e => e.Id == executorId)
            .SelectMany(e => e.SavedJobs)
            .ToList());
}