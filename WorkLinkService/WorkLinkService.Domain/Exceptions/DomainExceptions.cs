using WorkLinkService.Domain;

namespace WorkLinkService.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : ArgumentNullException(paramName, $"Параметр \"{paramName}\" не может быть null");
public class JobAlreadyClosedException(Job job)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) уже закрыта и не может быть изменена")
{
    public Job Job => job;
}
public class JobAlreadyActiveException(Job job)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) уже активна")
{
    public Job Job => job;
}
public class HashtagAlreadyAddedException(Job job, Hashtag hashtag)
    : InvalidOperationException($"Хэштег \"#{hashtag.Name?.Value}\" уже добавлен к работе \"{job.Title?.Value}\" (id = {job.Id})")
{
    public Job Job => job;
    public Hashtag Hashtag => hashtag;
}
public class JobNotBelongToEmployerException(Job job, Employer employer)
    : InvalidOperationException($"Работодатель \"{employer.CompanyName?.Value}\" не может изменять вакансию \"{job.Title?.Value}\" (id = {job.Id}), " +
        $"так как она принадлежит другому работодателю")
{
    public Job Job => job;
    public Employer Employer => employer;
}
public class JobAlreadySavedException(Job job, Executor executor)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) уже сохранена у исполнителя \"{executor.FullName?.Value}\"")
{
    public Job Job => job;
    public Executor Executor => executor;
}
public class JobNotSavedException(Job job, Executor executor)
    : InvalidOperationException($"Вакансия \"{job.Title?.Value}\" (id = {job.Id}) не найдена среди сохранённых работ исполнителя \"{executor.FullName?.Value}\".")
{
    public Job Job => job;
    public Executor Executor => executor;
}