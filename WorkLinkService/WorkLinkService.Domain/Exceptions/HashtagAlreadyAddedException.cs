namespace WorkLinkService.Domain.Exceptions;

public class HashtagAlreadyAddedException(Job job, Hashtag hashtag)
    : InvalidOperationException($"Хэштег \"#{hashtag.Name?.Value}\" уже добавлен к работе \"{job.Title?.Value}\" (id = {job.Id})")
{
    public Job Job => job;
    public Hashtag Hashtag => hashtag;
}