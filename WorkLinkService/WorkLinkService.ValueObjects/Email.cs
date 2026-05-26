using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class Email(string email) : ValueObject<string>(new EmailValidator(), email);