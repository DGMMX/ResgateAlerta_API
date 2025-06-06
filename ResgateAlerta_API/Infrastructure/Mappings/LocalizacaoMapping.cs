using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class LocalizacaoMapping: IEntityTypeConfiguration<Localizacao>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Localizacao> builder)
        {
            builder
                .ToTable("TBL_LOCALIZACAO");

            builder
                .HasKey(l => l.IdLocalização);

            builder
                .Property(l => l.IdLocalização)
                .HasColumnName("id_localizacao");

            builder
                .Property(l => l.Logradouro)
                .HasColumnName("logradouro")
                .HasMaxLength(100)
                .IsRequired();

            builder 
                .Property(l => l.Numero)
                .HasColumnName("numero")
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(l => l.Complemento)
                .HasColumnName("complemento")
                .HasMaxLength(50);

            builder
                .Property(l => l.Cep)
                .HasColumnName("cep")
                .HasMaxLength(8)
                .IsRequired();

            builder
                .Property(l => l.IdBairro)
                .HasColumnName("id_bairro");

            builder
                .HasOne(l => l.Bairro)
                .WithMany(b => b.Localizacoes)
                .HasForeignKey(l => l.IdBairro)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
