using WorkLinkService.Domain.Base;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Domain;

public class Job : Entity<Guid>
{
    private readonly ICollection<Hashtag> _hashtags = [];

    public Employer Employer { get; } = default!;
    public JobTitle? Title { get; private set; } = null;
    public JobDescription Description { get; private set; } = default!;
    public ContactInfo ContactInfo { get; private set; } = default!;
    public JobStatus Status { get; private set; } = JobStatus.Draft;
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; } = null;
    public IReadOnlyCollection<Hashtag> Hashtags => _hashtags.ToList().AsReadOnly();

    protected Job() { }

    public Job(Employer employer, JobTitle title, JobDescription description, ContactInfo contactInfo)
        : base(Guid.NewGuid())
    {
        Employer = employer ?? throw new ArgumentNullValueException(nameof(employer));
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Description = description ?? throw new ArgumentNullValueException(nameof(description));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
        CreatedAt = DateTime.UtcNow;
    }

    protected Job(Guid id, Employer employer, JobTitle title, JobDescription description,
        ContactInfo contactInfo, DateTime createdAt, DateTime? modifiedAt = null)
        : base(id)
    {
        Employer = employer ?? throw new ArgumentNullValueException(nameof(employer));
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Description = description ?? throw new ArgumentNullValueException(nameof(description));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
    }

    internal bool Publish(Employer employer)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        if (Status == JobStatus.Active) throw new JobAlreadyActiveException(this);
        Status = JobStatus.Active;
        ModifiedAt = DateTime.UtcNow;
        return true;
    }

    internal bool Close(Employer employer)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        Status = JobStatus.Closed;
        ModifiedAt = DateTime.UtcNow;
        return true;
    }

    // internal — только работодатнль может вызвать через свои методы
    internal bool SetTitle(Employer employer, JobTitle newTitle)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        if (newTitle == null) throw new ArgumentNullValueException(nameof(newTitle));
        if (Title == newTitle) return false;
        Title = newTitle;
        ModifiedAt = DateTime.UtcNow;
        return true;
    }

    internal bool SetDescription(Employer employer, JobDescription newDescription)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        if (newDescription == null) throw new ArgumentNullValueException(nameof(newDescription));
        if (Description == newDescription) return false;
        Description = newDescription;
        ModifiedAt = DateTime.UtcNow;
        return true;
    }

    internal bool SetContactInfo(Employer employer, ContactInfo newContactInfo)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        if (newContactInfo == null) throw new ArgumentNullValueException(nameof(newContactInfo));
        if (ContactInfo == newContactInfo) return false;
        ContactInfo = newContactInfo;
        ModifiedAt = DateTime.UtcNow;
        return true;
    }

    internal void AddHashtag(Employer employer, Hashtag hashtag)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        if (hashtag == null) throw new ArgumentNullValueException(nameof(hashtag));
        if (_hashtags.Any(h => h.Name == hashtag.Name))
            throw new HashtagAlreadyAddedException(this, hashtag);
        _hashtags.Add(hashtag);
        ModifiedAt = DateTime.UtcNow;
    }

    internal bool RemoveHashtag(Employer employer, Hashtag hashtag)
    {
        if (Status == JobStatus.Closed) throw new JobAlreadyClosedException(this);
        if (hashtag == null) throw new ArgumentNullValueException(nameof(hashtag));
        var removed = _hashtags.Remove(hashtag);
        if (removed) ModifiedAt = DateTime.UtcNow;
        return removed;
    }
}