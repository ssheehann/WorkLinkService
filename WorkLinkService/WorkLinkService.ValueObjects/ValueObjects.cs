using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class JobTitle(string title) : ValueObject<string>(new JobTitleValidator(), title);
public class JobDescription(string description) : ValueObject<string>(new JobDescriptionValidator(), description);
public class ContactInfo(string contactInfo) : ValueObject<string>(new ContactInfoValidator(), contactInfo);
public class CompanyName(string companyName) : ValueObject<string>(new CompanyNameValidator(), companyName);
public class FullName(string fullName) : ValueObject<string>(new FullNameValidator(), fullName);
public class HashtagName(string name) : ValueObject<string>(new HashtagNameValidator(), name);
public class Email(string email) : ValueObject<string>(new EmailValidator(), email);

