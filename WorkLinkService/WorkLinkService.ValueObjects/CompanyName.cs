using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class CompanyName(string companyName) : ValueObject<string>(new CompanyNameValidator(), companyName);