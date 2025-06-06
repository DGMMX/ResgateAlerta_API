using EcoDenuncia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class AcompanhamentoDenunciaMapping: IEntityTypeConfiguration<AcompanhamentoDenuncia>
    {
        public void Configure(EntityTypeBuilder<AcompanhamentoDenuncia> builder)
        {
            builder
                .ToTable("TBL_ACOMPANHAMENTO_DENUNCIA");
            
            builder
                .HasKey(a => a.IdAcompanhamento);

            
            builder
                .Property(a => a.Status)
                .HasConversion<string>()
                .HasColumnName("status")
                .IsRequired();

            builder
                .Property(a => a.DataAtualizacao)
                .HasColumnName("data_atualizacao")
                .IsRequired();

            builder
                .Property(a => a.Observacao)
                .HasColumnName("observacao")
                .HasMaxLength(200);

            builder .Property(a => a.IdDenuncia)
                .HasColumnName("id_denuncia")
                .IsRequired();

            builder
                .HasOne(a => a.Denuncia)
                .WithMany()
                .HasForeignKey(a => a.IdDenuncia)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
