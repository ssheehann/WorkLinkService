using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkLinkService.Domain;
using WorkLinkService.ValueObjects;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.Infrastructure.EntityFramework.Configurations;

public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(email => email.Value, str => new Email(str))
            .HasMaxLength(EmailValidator.MAX_LENGTH);

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasConversion(name => name.Value, str => new CompanyName(str))
            .HasMaxLength(CompanyNameValidator.MAX_LENGTH);

        builder.Property(x => x.ContactInfo)
            .IsRequired()
            .HasConversion(info => info.Value, str => new ContactInfo(str))
            .HasMaxLength(ContactInfoValidator.MAX_LENGTH);

        builder.HasMany<Job>("_jobs")
            .WithOne(x => x.Employer)
            .HasForeignKey("EmployerId")
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.Jobs);
    }
}