using WorkLinkService.Domain.Base;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Domain;

public class Executor(Guid id, FullName fullName, ContactInfo contactInfo) : Entity<Guid>(id)
{
    private readonly ICollection<Job> _savedJobs = [];
    public FullName FullName { get; private set; } = fullName ?? throw new ArgumentNullValueException(nameof(fullName));
    public ContactInfo ContactInfo { get; private set; } = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    public IReadOnlyCollection<Job> SavedJobs => _savedJobs.ToList().AsReadOnly();
    public Executor(FullName fullName, ContactInfo contactInfo) : this(Guid.NewGuid(), fullName, contactInfo) { }
    protected Executor() : this(default!, default!) { }
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