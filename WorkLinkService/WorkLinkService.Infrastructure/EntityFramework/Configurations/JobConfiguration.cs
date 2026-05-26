using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkLinkService.Domain;
using WorkLinkService.ValueObjects;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.Infrastructure.EntityFramework.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Title)
            .IsRequired(false)
            .HasConversion(title => title!.Value, str => new JobTitle(str))
            .HasMaxLength(JobTitleValidator.MAX_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasConversion(desc => desc.Value, str => new JobDescription(str))
            .HasMaxLength(JobDescriptionValidator.MAX_LENGTH);

        builder.Property(x => x.ContactInfo)
            .IsRequired()
            .HasConversion(info => info.Value, str => new ContactInfo(str))
            .HasMaxLength(ContactInfoValidator.MAX_LENGTH);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.Property(x => x.ModifiedAt)
            .IsRequired(false)
            .HasConversion(
                src => !src.HasValue ? src : src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                dst => !dst.HasValue ? dst : dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
            );

        builder.HasMany<Hashtag>("_hashtags")
            .WithMany()
             .UsingEntity("JobHashtags",
             j => j.HasOne(typeof(Hashtag)).WithMany().HasForeignKey("HashtagId"),
             j => j.HasOne(typeof(Job)).WithMany().HasForeignKey("JobId"));

        builder.Ignore(x => x.Hashtags);
        builder.Ignore(x => x.Employer);
    }
}