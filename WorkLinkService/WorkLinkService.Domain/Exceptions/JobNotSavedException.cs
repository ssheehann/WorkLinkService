namespace WorkLinkService.Domain.Exceptions;

public class JobNotSavedException(Job job, Executor executor)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) не найдена в сохранённых у исполнителя \"{executor.FullName?.Value}\".")
{
    public Job Job => job;
    public Executor Executor => executor;
}