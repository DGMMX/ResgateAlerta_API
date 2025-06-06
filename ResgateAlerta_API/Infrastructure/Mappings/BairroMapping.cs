using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class BairroMapping: IEntityTypeConfiguration<Bairro>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bairro> builder)
        {
            builder
                .ToTable("TBL_BAIRRO");

            builder
                .HasKey(b => b.IdBairro);

            builder
                .Property(b => b.IdBairro)
                .HasColumnName("id_bairro");

            builder.Property(b => b.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(b => b.IdCidade)
                .HasColumnName("id_cidade")
                .IsRequired();

            builder
                .HasOne(b => b.Cidade)
                .WithMany(c => c.Bairros)
                .HasForeignKey(b => b.IdCidade)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
