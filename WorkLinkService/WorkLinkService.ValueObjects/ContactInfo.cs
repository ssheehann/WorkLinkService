using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class ContactInfo(string contactInfo) : ValueObject<string>(new ContactInfoValidator(), contactInfo);