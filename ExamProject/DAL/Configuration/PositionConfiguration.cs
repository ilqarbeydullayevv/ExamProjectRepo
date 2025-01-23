using ExamProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamProject.DAL.Configuration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(x => x.Name)
                 .IsRequired()
                 .HasMaxLength(10);

            builder.HasMany(m => m.Members)
                .WithOne(x => x.Position)
                .HasForeignKey(f => f.PositionId);
        }
    }
}
