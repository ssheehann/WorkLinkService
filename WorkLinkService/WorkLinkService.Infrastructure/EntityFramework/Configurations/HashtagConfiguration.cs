using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkLinkService.Domain;
using WorkLinkService.ValueObjects;
using WorkLinkService.ValueObjects.Validators;

namespace WorkLinkService.Infrastructure.EntityFramework.Configurations;

public class HashtagConfiguration : IEntityTypeConfiguration<Hashtag>
{
    public void Configure(EntityTypeBuilder<Hashtag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(name => name.Value, str => new HashtagName(str))
            .HasMaxLength(HashtagNameValidator.MAX_LENGTH);
    }
}