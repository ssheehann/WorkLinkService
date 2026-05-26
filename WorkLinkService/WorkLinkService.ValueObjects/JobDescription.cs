using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class JobDescription(string description) : ValueObject<string>(new JobDescriptionValidator(), description);