using EventsHub.DAL.Entities.Film;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsHub.DAL.Configurations
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.ToTable("Cinemas");
            builder.HasKey(c => c.CinemaId);
            builder.Property(c => c.CinemaId).ValueGeneratedOnAdd();    
        }
    }
}
