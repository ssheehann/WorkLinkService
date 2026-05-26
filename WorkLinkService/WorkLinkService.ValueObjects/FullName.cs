using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class FullName(string fullName) : ValueObject<string>(new FullNameValidator(), fullName);