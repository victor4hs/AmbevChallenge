using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(i => i.CustomerId).IsRequired().HasColumnType("uuid");
            builder.Property(i => i.CustomerName);
            builder.Property(i => i.BranchId).IsRequired().HasColumnType("uuid");
            builder.Property(i => i.BranchName);
            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.SaleDate).IsRequired().HasColumnType("timestamp")
                .HasConversion(v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified), v => v);
            builder.Property(s => s.TotalAmount).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(s => s.IsCancelled).IsRequired().HasDefaultValue(false);

            builder.HasMany(s => s.SaleItems)
                .WithOne()
                .HasForeignKey(i => i.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<DateTime>("CreatedAt").IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property<DateTime?>("UpdatedAt");



        }
    }

}
