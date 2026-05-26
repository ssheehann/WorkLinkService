using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.ValueObjects;

public class HashtagName(string name) : ValueObject<string>(new HashtagNameValidator(), name);