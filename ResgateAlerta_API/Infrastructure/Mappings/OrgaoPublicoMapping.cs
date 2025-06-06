using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ResgateAlerta.Infrastructure.Mappings
{
    public class OrgaoPublicoMapping: IEntityTypeConfiguration<OrgaoPublico>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrgaoPublico> builder)
        {
            builder
                .ToTable("TBL_ORGAOS_PUBLICOS");

            builder
                .HasKey(o => o.IdOrgaoPublico);

            builder
                .Property(o => o.IdOrgaoPublico)
                .HasColumnName("id_orgao"); 

            builder
                .Property(o => o.Nome)
                .HasColumnName("nome")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(o => o.AreaAtuacao)
                .HasColumnName("area_atuacao")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
