using WorkLinkService.Domain;
using WorkLinkService.Domain.Repositories.Abstractions.Base;

namespace WorkLinkService.Domain.Repositories.Abstractions;

public interface IHashtagRepository : IRepository<Hashtag, Guid>
{
    // тк название хэштега уникальное
    Task<Hashtag?> GetByNameAsync(string name, CancellationToken cancellationToken);
}