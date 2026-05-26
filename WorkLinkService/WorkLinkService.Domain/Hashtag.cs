using WorkLinkService.Domain.Base;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Domain;

public class Hashtag(Guid id, HashtagName name) : Entity<Guid>(id)
{
    public HashtagName Name { get; private set; } = name ?? throw new ArgumentNullValueException(nameof(name));
    public Hashtag(HashtagName name) : this(Guid.NewGuid(), name) { }
    protected Hashtag() : this(default!, default!) { }
}