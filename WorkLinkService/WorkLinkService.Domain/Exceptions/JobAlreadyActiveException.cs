namespace WorkLinkService.Domain.Exceptions;

public class JobAlreadyActiveException(Job job)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) уже активна")
{
    public Job Job => job;
}