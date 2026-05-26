using WorkLinkService.Domain.Base;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Domain;

public class Employer : Entity<Guid>
{
    private readonly ICollection<Job> _jobs = [];

    public Email Email { get; private set; } = default!;
    public CompanyName CompanyName { get; private set; } = default!;
    public ContactInfo ContactInfo { get; private set; } = default!;
    public IReadOnlyCollection<Job> Jobs => _jobs.ToList().AsReadOnly();

    public Employer(Email email, CompanyName companyName, ContactInfo contactInfo)
        : base(Guid.NewGuid())
    {
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        CompanyName = companyName ?? throw new ArgumentNullValueException(nameof(companyName));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    }

    protected Employer(Guid id, Email email, CompanyName companyName, ContactInfo contactInfo)
        : base(id)
    {
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        CompanyName = companyName ?? throw new ArgumentNullValueException(nameof(companyName));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    }

    protected Employer() : base(default!) { }

    public Job CreateJob(JobTitle title, JobDescription description, ContactInfo jobContactInfo)
    {
        var job = new Job(this, title, description, jobContactInfo);
        _jobs.Add(job);
        return job;
    }

    public bool PublishJob(Job job)
    {
        EnsureJobBelongsToMe(job);
        return job.Publish(this);
    }

    public bool CloseJob(Job job)
    {
        EnsureJobBelongsToMe(job);
        return job.Close(this);
    }

    public bool EditJobTitle(Job job, JobTitle newTitle)
    {
        EnsureJobBelongsToMe(job);
        return job.SetTitle(this, newTitle);
    }

    public bool EditJobDescription(Job job, JobDescription newDescription)
    {
        EnsureJobBelongsToMe(job);
        return job.SetDescription(this, newDescription);
    }

    public bool EditJobContactInfo(Job job, ContactInfo newContactInfo)
    {
        EnsureJobBelongsToMe(job);
        return job.SetContactInfo(this, newContactInfo);
    }

    public void AddHashtagToJob(Job job, Hashtag hashtag)
    {
        EnsureJobBelongsToMe(job);
        job.AddHashtag(this, hashtag);
    }

    public bool RemoveHashtagFromJob(Job job, Hashtag hashtag)
    {
        EnsureJobBelongsToMe(job);
        return job.RemoveHashtag(this, hashtag);
    }
    private void EnsureJobBelongsToMe(Job job)
    {
        if (!_jobs.Contains(job))
            throw new JobNotBelongToEmployerException(job, this);
    }
}