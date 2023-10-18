using LibraryCult.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCult.Catalogo.API.Data
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("varchar(100)")
                .HasColumnName("Description");

            builder.Property(p => p.Value)
                .HasColumnName("Value")
                .HasColumnType("decimal(18,6)")
                .IsRequired();

            builder.Property(p => p.RegisterDate)
                .HasColumnType("Date").HasDefaultValueSql("GetUtcDate()");

            builder.ToTable("Products");              
        }
    }
}
