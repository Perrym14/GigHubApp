using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(g => g.ArtistId)
            .IsRequired();


            Property(g => g.Venue)
            .HasMaxLength(255)
            .IsRequired();


            Property(g => g.GenreId)
            .IsRequired();

            HasMany(g => g.Attendances)
                .WithRequired(a => a.Gig)
                .WillCascadeOnDelete(false);
        }
    }
}