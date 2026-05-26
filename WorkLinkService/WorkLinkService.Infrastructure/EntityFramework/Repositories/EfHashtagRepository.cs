using Microsoft.EntityFrameworkCore;
using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions;
using WorkLinkService.Infrastructure.EntityFramework;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Infrastructure.EntityFramework.Repositories;

public class EfHashtagRepository(ApplicationDbContext context)
    : EfRepository<Hashtag, Guid>(context), IHashtagRepository
{
    private readonly DbSet<Hashtag> _hashtags = context.Set<Hashtag>();

    public Task<Hashtag?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => _hashtags.FirstOrDefaultAsync(h => h.Name.Equals(new HashtagName(name)), cancellationToken);
}