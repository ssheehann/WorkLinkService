using WorkLink.Domain.Enums;
using WorkLink.Domain.Exceptions;
using WorkLink.Domain.ValueObjects;

namespace WorkLink.Domain.Entities;

public class Job
{
    public Guid Id { get; private set; }
    public Guid EmployerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ContactInfo ContactInfo { get; private set; }
    public string? Budget { get; private set; }
    public JobStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Employer? Employer { get; private set; }

    private Job() { }

    public Job(Employer employer, string title, string description, ContactInfo contactInfo, string? budget = null)
    {
        if (employer == null)
            throw new ArgumentNullException(nameof(employer));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Заголовок не может быть пустым");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание не может быть пустым");

        Id = Guid.NewGuid();
        EmployerId = employer.Id;
        Employer = employer;
        Title = title;
        Description = description;
        ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));
        Budget = budget;
        Status = JobStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }
    public void Update(string title, string description, ContactInfo contactInfo, string? budget = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Заголовок не может быть пустым");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание не может быть пустым");

        if (Status == JobStatus.Closed)
            throw new InvalidJobStatusException("Нельзя редактировать закрытую вакансию");

        Title = title;
        Description = description;
        ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));
        Budget = budget;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Close()
    {
        if (Status == JobStatus.Closed)
            throw new InvalidJobStatusException("Вакансия уже закрыта");
        Status = JobStatus.Closed;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Reopen()
    {
        if (Status == JobStatus.Active)
            throw new InvalidJobStatusException("Вакансия уже активна");
        Status = JobStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }
}