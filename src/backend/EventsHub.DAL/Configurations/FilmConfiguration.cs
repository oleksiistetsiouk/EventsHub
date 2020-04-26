using EventsHub.DAL.Entities.Film;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsHub.DAL.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films");
            builder.HasKey(f=> f.FilmId);
            builder.Property(f => f.FilmId).ValueGeneratedOnAdd();
        }
    }
}
