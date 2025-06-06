using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class DenunciaMapping: IEntityTypeConfiguration<Denuncia>
    {
        public void Configure(EntityTypeBuilder<Denuncia> builder)
        {
            builder
                .ToTable("TBL_DENUNCIAS");

            builder
                .HasKey(d => d.IdDenuncia);

            builder
                .Property(d => d.IdDenuncia)
                .HasColumnName("id_denuncia");

            builder
                .Property(d => d.IdUsuario)
                .HasColumnName("id_usuario");

            builder
                .Property(d => d.IdLocalizacao)
                .HasColumnName("id_localizacao");

            builder
                .Property(d => d.IdOrgaoPublico)
                .HasColumnName("id_orgao");

            builder
                .Property(d => d.DataHora)
                .HasColumnName("data_hora")
                .IsRequired();

            builder
                .Property(d => d.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .HasOne(d => d.Usuario)
                .WithMany(u => u.Denuncias)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(d => d.Localizacao)
                .WithMany()
                .HasForeignKey(d => d.IdLocalizacao)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(d => d.OrgaoPublico)
                .WithMany()
                .HasForeignKey(d => d.IdOrgaoPublico)
                .OnDelete(DeleteBehavior.Cascade);



        }
    
    
    }
}
