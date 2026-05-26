namespace WorkLinkService.Domain.Exceptions;

public class JobNotBelongToEmployerException(Job job, Employer employer)
    : InvalidOperationException($"Работодатель \"{employer.CompanyName?.Value}\" не может изменять вакансию \"{job.Title?.Value}\" (id = {job.Id}), " +
                                $"так как она принадлежит другому работодателю")
{
    public Job Job => job;
    public Employer Employer => employer;
}