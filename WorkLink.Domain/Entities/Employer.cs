using WorkLink.Domain.Exceptions;
using WorkLink.Domain.ValueObjects;

namespace WorkLink.Domain.Entities;

public class Employer
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string CompanyName { get; private set; }
    public ContactInfo Contact { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<Job> _jobs = new();
    public IReadOnlyList<Job> Jobs => _jobs;

    private Employer() { }

    public Employer(string email, string companyName, ContactInfo contact)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email не может быть пустым");
        if (string.IsNullOrEmpty(companyName))
            throw new ArgumentException("Название компании не может быть пустым");

        Id = Guid.NewGuid();
        Email = email;
        CompanyName = companyName;
        Contact = contact ?? throw new ArgumentNullException(nameof(contact));
        CreatedAt = DateTime.UtcNow;
    }
    public Job CreateJob(string title, string description, ContactInfo contactForJob, string? budget = null)
    {
        var job = new Job(this, title, description, contactForJob, budget);
        _jobs.Add(job);
        return job;
    }
    public void CloseJob(Job job)
    {
        if (job.EmployerId != Id)
            throw new InsufficientRightsException("Нельзя закрыть чужую вакансию");
        job.Close();
    }
}