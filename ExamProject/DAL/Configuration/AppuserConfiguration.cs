using ExamProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamProject.DAL.Configuration
{
    public class AppuserConfiguration : IEntityTypeConfiguration<Appuser>
    {
        public void Configure(EntityTypeBuilder<Appuser> builder)
        {
            builder.Property(x => x.FullName)
                  .IsRequired()
                  .HasMaxLength(18);
        }
    }
}
