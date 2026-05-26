using Microsoft.EntityFrameworkCore;
using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions;
using WorkLinkService.Infrastructure.EntityFramework;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Infrastructure.EntityFramework.Repositories;

public class EfEmployerRepository(ApplicationDbContext context)
    : EfRepository<Employer, Guid>(context), IEmployerRepository
{
    private readonly DbSet<Employer> _employers = context.Set<Employer>();

    public override Task<Employer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _employers.Include("_jobs").FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task<Employer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => _employers.Include("_jobs").FirstOrDefaultAsync(e => e.Email.Equals(new Email(email)), cancellationToken);
}