namespace WorkLinkService.Domain.Exceptions;

public class JobAlreadySavedException(Job job, Executor executor)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) уже сохранена у исполнителя \"{executor.FullName?.Value}\".")
{
    public Job Job => job;
    public Executor Executor => executor;
}