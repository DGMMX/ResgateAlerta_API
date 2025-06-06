using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class CidadeMapping : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cidade> builder)
        {
            builder
                .ToTable("TBL_CIDADE");

            builder
                .HasKey(c => c.IdCidade);

            builder 
                .Property(c => c.IdCidade)
                .HasColumnName("id_cidade");

            builder
                .Property(c => c.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(c => c.IdEstado)
                .HasColumnName("id_estado")
                .IsRequired();

            builder
                .HasOne(c => c.Estado)
                .WithMany(e => e.Cidades)
                .HasForeignKey(c => c.IdEstado)
                .HasConstraintName("fk_estado")
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
