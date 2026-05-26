using Microsoft.EntityFrameworkCore;
using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions;
using WorkLinkService.Infrastructure.EntityFramework;

namespace WorkLinkService.Infrastructure.EntityFramework.Repositories;

public class EfJobRepository(ApplicationDbContext context)
    : EfRepository<Job, Guid>(context), IJobRepository
{
    private readonly DbSet<Job> _jobs = context.Set<Job>();

    public override Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _jobs.Include("_hashtags").FirstOrDefaultAsync(j => j.Id == id, cancellationToken);

    public Task<IEnumerable<Job>> GetAllActiveAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        => Task.FromResult<IEnumerable<Job>>(
            (asNoTracking ? _jobs.AsNoTracking() : _jobs)
            .Include("_hashtags")
            .Where(j => j.Status == JobStatus.Active)
            .ToList());

    public Task<IEnumerable<Job>> GetByHashtagAsync(string hashtagName, CancellationToken cancellationToken, bool asNoTracking = false)
        => Task.FromResult<IEnumerable<Job>>(
            (asNoTracking ? _jobs.AsNoTracking() : _jobs)
            .Include("_hashtags")
            .Where(j => j.Status == JobStatus.Active &&
                j.Hashtags.Any(h => h.Name.Value == hashtagName))
            .ToList());

    public Task<IEnumerable<Job>> GetByEmployerIdAsync(Guid employerId, CancellationToken cancellationToken, bool asNoTracking = false)
        => Task.FromResult<IEnumerable<Job>>(
            (asNoTracking ? _jobs.AsNoTracking() : _jobs)
            .Include("_hashtags")
            .Where(j => j.Employer.Id == employerId)
            .ToList());
}