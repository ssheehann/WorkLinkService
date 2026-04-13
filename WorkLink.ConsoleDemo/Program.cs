using WorkLink.Domain.Entities;
using WorkLink.Domain.Enums;
using WorkLink.Domain.Exceptions;
using WorkLink.Domain.ValueObjects;

namespace WorkLink.ConsoleDemo;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("WorkLink - проверка домена\n");

        try
        {
            var employerContact = new ContactInfo("@hr_popka", ContactType.Telegram);
            var jobContact = new ContactInfo("@hr_popka", ContactType.Telegram);
            var freelancerContact = new ContactInfo("@danil_kolbasenko", ContactType.Telegram);

            var employer = new Employer("hr@popka.ru", "ООО ПОПКА", employerContact);
            Console.WriteLine($"Создан работодатель: {employer.CompanyName}");

            var freelancer = new Freelancer("danil@mail.ru", "Данил Колбасенко", freelancerContact);
            Console.WriteLine($"Создан фрилансер: {freelancer.FullName}");

            var job = employer.CreateJob(
                "Разработчик",
                "Ищем разработчика на проект POPKA. Удалённо.",
                jobContact,
                "100 000 000 руб"
            );
            Console.WriteLine($"Создана вакансия: {job.Title}");

            Console.WriteLine($"\nКонтакт для отклика: {job.ContactInfo}");

            var message = freelancer.GetContactForJob(job);
            Console.WriteLine($"\n{message}");

            employer.CloseJob(job);
            Console.WriteLine($"\nВакансия закрыта, статус: {job.Status}");

            Console.WriteLine("\nПробуем отредактировать закрытую вакансию...");
            job.Update("Новое название", "Новое описание", jobContact);
        }
        catch (InvalidJobStatusException ex)
        {
            Console.WriteLine($"\nОшибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nОшибка: {ex.Message}");
        }

        Console.WriteLine("\nГотово");
    }
}