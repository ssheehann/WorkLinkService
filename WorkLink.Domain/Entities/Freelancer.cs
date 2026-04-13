using WorkLink.Domain.ValueObjects;

namespace WorkLink.Domain.Entities;

public class Freelancer
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FullName { get; private set; }
    public ContactInfo Contact { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Freelancer() { }

    public Freelancer(string email, string fullName, ContactInfo contact)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email не может быть пустым");
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException("Имя не может быть пустым");

        Id = Guid.NewGuid();
        Email = email;
        FullName = fullName;
        Contact = contact ?? throw new ArgumentNullException(nameof(contact));
        CreatedAt = DateTime.UtcNow;
    }

    public string GetContactForJob(Job job)
    {
        return $"По вакансии '{job.Title}' пишите сюда: {job.ContactInfo}";
    }
}