namespace WorkLinkService.Domain.Exceptions;

public class JobAlreadyClosedException(Job job)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) уже закрыта и не может быть изменена")
{
    public Job Job => job;
}