using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class JobTitle(string title) : ValueObject<string>(new JobTitleValidator(), title);