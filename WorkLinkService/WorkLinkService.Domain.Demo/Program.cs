using WorkLinkService.Domain;
using WorkLinkService.Domain.Exceptions;
using WorkLinkService.ValueObjects;

Console.WriteLine("=== WorkLinkService — демонстрация доменного слоя ===\n");

// 1. Создаём работодателя
Console.WriteLine("1. Создание работодателя...");
var employer = new Employer(
    new Email("hr@techcorp.ru"),
    new CompanyName("TechC orp"),
    new ContactInfo("Telegram: @techcorp_hr")
);
Console.WriteLine($"   Работодатель: {employer.CompanyName.Value} ({employer.Email.Value})");

// 2. Работодатель создаёт вакансию (статус Draft)
Console.WriteLine("\n2. Создание вакансии...");
var job = employer.CreateJob(
    new JobTitle("C# Backend Developer"),
    new JobDescription("Ищем разработчика на .NET 8, PostgreSQL, RabbitMQ."),
    new ContactInfo("Telegram: @techcorp_hr")
);
Console.WriteLine($"  Вакансия: '{job.Title?.Value}' | Статус: {job.Status}");

// 3. Добавляем хэштеги
Console.WriteLine("\n3. Добавление хэштегов...");
var tagCsharp = new Hashtag(new HashtagName("csharp"));
var tagDotnet = new Hashtag(new HashtagName("dotnet"));
var tagRemote = new Hashtag(new HashtagName("remote"));
job.AddHashtag(tagCsharp);
job.AddHashtag(tagDotnet);
job.AddHashtag(tagRemote);
Console.WriteLine($"   Хэштеги: {string.Join(", ", job.Hashtags.Select(h => "#" + h.Name.Value))}");

// 4. Публикуем вакансию
Console.WriteLine("\n4. Публикация вакансии...");
employer.PublishJob(job);
Console.WriteLine($"   Статус после публикации: {job.Status}");

// 5. Исполнитель сохраняет вакансию
Console.WriteLine("\n5. Исполнитель сохраняет вакансию...");
var executor = new Executor(
    new FullName("Анна Иванова"),
    new ContactInfo("Telegram: @anna_dev")
);
executor.SaveJob(job);
Console.WriteLine($"   Исполнитель: {executor.FullName.Value}");
Console.WriteLine($"   Сохранённых вакансий: {executor.SavedJobs.Count}");

// 6. Проверяем доменные исключения
Console.WriteLine("\n6. Проверка доменных исключений...");

try { job.AddHashtag(tagCsharp); }
catch (HashtagAlreadyAddedException ex)
{ Console.WriteLine($"   [OK] {ex.GetType().Name}: {ex.Message}"); }

try { executor.SaveJob(job); }
catch (JobAlreadySavedException ex)
{ Console.WriteLine($"   [OK] {ex.GetType().Name}: {ex.Message}"); }

try { _ = new JobTitle(""); }
catch (Exception ex)
{ Console.WriteLine($"   [OK] {ex.GetType().Name}: {ex.Message}"); }

// 7. Закрываем вакансию
Console.WriteLine("\n7. Закрытие вакансии...");
employer.CloseJob(job);
Console.WriteLine($"   Статус: {job.Status}");

try { job.SetTitle(new JobTitle("Новое название")); }
catch (JobAlreadyClosedException ex)
{ Console.WriteLine($"   [OK] {ex.GetType().Name}: {ex.Message}"); }

try { _ = new Email("не-email"); }
catch (Exception ex)
{ Console.WriteLine($"   [OK] {ex.GetType().Name}: {ex.Message}"); }

Console.WriteLine("\n=== Все проверки пройдены успешно! ===");