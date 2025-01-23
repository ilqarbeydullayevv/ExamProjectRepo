using ExamProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamProject.DAL.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.FullName)
                 .IsRequired()
                 .HasMaxLength(18);

            builder.HasOne(x => x.Position)
                .WithMany(m => m.Members)
                .HasForeignKey(f => f.PositionId);
        }
    }
}
