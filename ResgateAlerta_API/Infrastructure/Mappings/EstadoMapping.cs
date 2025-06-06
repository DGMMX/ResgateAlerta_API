using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class EstadoMapping: IEntityTypeConfiguration<Estado>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Estado> builder)
        {
            builder
                .ToTable("TBL_ESTADO");

            builder
                .HasKey(e => e.IdEstado);

            builder
                .Property(e => e.IdEstado)
                .HasColumnName("id_estado");

            builder
                .Property(e => e.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(e => e.Uf)
                .HasColumnName("uf")
                .HasMaxLength(2)
                .IsRequired();

        }
    }
}
