using Events.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.API.Infra.Mappings
{
    public class EventMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Tag).HasMaxLength(50).IsRequired();
            builder.Property(p => p.EventDate).IsRequired();
            builder.Property(p => p.Value);
            builder.Property(p => p.Processed).IsRequired();
        }
    }
}
