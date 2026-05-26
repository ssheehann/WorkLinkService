using WorkLinkService.Domain.Base;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Domain;

public class Executor : Entity<Guid>
{
    private readonly ICollection<Job> _savedJobs = [];

    public FullName FullName { get; private set; } = default!;
    public ContactInfo ContactInfo { get; private set; } = default!;
    public IReadOnlyCollection<Job> SavedJobs => _savedJobs.ToList().AsReadOnly();

    public Executor(FullName fullName, ContactInfo contactInfo)
        : base(Guid.NewGuid())
    {
        FullName = fullName ?? throw new ArgumentNullValueException(nameof(fullName));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    }

    protected Executor(Guid id, FullName fullName, ContactInfo contactInfo)
        : base(id)
    {
        FullName = fullName ?? throw new ArgumentNullValueException(nameof(fullName));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    }

    protected Executor() : base(default!) { }

    public void SaveJob(Job job)
    {
        if (job == null) throw new ArgumentNullValueException(nameof(job));
        if (_savedJobs.Contains(job)) throw new JobAlreadySavedException(job, this);
        _savedJobs.Add(job);
    }

    public void UnsaveJob(Job job)
    {
        if (job == null) throw new ArgumentNullValueException(nameof(job));
        if (!_savedJobs.Contains(job)) throw new JobNotSavedException(job, this);
        _savedJobs.Remove(job);
    }

    public bool UpdateContactInfo(ContactInfo newContactInfo)
    {
        if (newContactInfo == null) throw new ArgumentNullValueException(nameof(newContactInfo));
        if (ContactInfo == newContactInfo) return false;
        ContactInfo = newContactInfo;
        return true;
    }

    public bool UpdateFullName(FullName newFullName)
    {
        if (newFullName == null) throw new ArgumentNullValueException(nameof(newFullName));
        if (FullName == newFullName) return false;
        FullName = newFullName;
        return true;
    }
}