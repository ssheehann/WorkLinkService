using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkLinkService.Domain;
using WorkLinkService.ValueObjects;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.Infrastructure.EntityFramework.Configurations;

public class ExecutorConfiguration : IEntityTypeConfiguration<Executor>
{
    public void Configure(EntityTypeBuilder<Executor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasConversion(name => name.Value, str => new FullName(str))
            .HasMaxLength(FullNameValidator.MAX_LENGTH);

        builder.Property(x => x.ContactInfo)
            .IsRequired()
            .HasConversion(info => info.Value, str => new ContactInfo(str))
            .HasMaxLength(ContactInfoValidator.MAX_LENGTH);

        builder.HasMany<Job>("_savedJobs")
            .WithMany()
            .UsingEntity("ExecutorSavedJobs",
             j => j.HasOne(typeof(Job)).WithMany().HasForeignKey("JobId"),
             j => j.HasOne(typeof(Executor)).WithMany().HasForeignKey("ExecutorId"));

        builder.Ignore(x => x.SavedJobs);
    }
}