using WorkLinkService.Domain.Base;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

namespace WorkLinkService.Domain;

public class Employer(Guid id, Email email, CompanyName companyName, ContactInfo contactInfo) : Entity<Guid>(id)
{
    private readonly ICollection<Job> _jobs = [];
    public Email Email { get; private set; } = email ?? throw new ArgumentNullValueException(nameof(email));
    public CompanyName CompanyName { get; private set; } = companyName ?? throw new ArgumentNullValueException(nameof(companyName));
    public ContactInfo ContactInfo { get; private set; } = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    public IReadOnlyCollection<Job> Jobs => _jobs.ToList().AsReadOnly();
    public Employer(Email email, CompanyName companyName, ContactInfo contactInfo) : this(Guid.NewGuid(), email, companyName, contactInfo) { }
    protected Employer() : this(default!, default!, default!) { }
    public Job CreateJob(JobTitle title, JobDescription description, ContactInfo jobContactInfo)
    {
        var job = new Job(this, title, description, jobContactInfo);
        _jobs.Add(job);
        return job;
    }
    public void PublishJob(Job job)
    {
        EnsureJobBelongsToMe(job);
        job.Publish(this);
    }
    public void CloseJob(Job job)
    {
        EnsureJobBelongsToMe(job);
        job.Close(this);
    }
    public bool UpdateContactInfo(ContactInfo newContactInfo)
    {
        if (newContactInfo == null) throw new ArgumentNullValueException(nameof(newContactInfo));
        if (ContactInfo == newContactInfo) return false;
        ContactInfo = newContactInfo;
        return true;
    }
    public bool UpdateCompanyName(CompanyName newCompanyName)
    {
        if (newCompanyName == null) throw new ArgumentNullValueException(nameof(newCompanyName));
        if (CompanyName == newCompanyName) return false;
        CompanyName = newCompanyName;
        return true;
    }
    private void EnsureJobBelongsToMe(Job job)
    {
        if (job.Employer != this) throw new JobNotBelongToEmployerException(job, this);
    }
}