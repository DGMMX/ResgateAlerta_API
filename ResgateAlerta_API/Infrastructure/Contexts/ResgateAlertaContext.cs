using ResgateAlerta.Infrastructure.Mappings;
using ResgateAlerta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;


namespace EcoDenuncia.Infrastructure.Contexts
{
    public class EcoDenunciaContext(DbContextOptions<EcoDenunciaContext> options) : DbContext(options)
    {

        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<OrgaoPublico> OrgaosPublicos { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<AcompanhamentoDenuncia> acompanhamentoDenuncias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new DenunciaMapping());
            modelBuilder.ApplyConfiguration(new OrgaoPublicoMapping());
            modelBuilder.ApplyConfiguration(new LocalizacaoMapping());
            modelBuilder.ApplyConfiguration(new BairroMapping());
            modelBuilder.ApplyConfiguration(new CidadeMapping());
            modelBuilder.ApplyConfiguration(new EstadoMapping());
            modelBuilder.ApplyConfiguration(new AcompanhamentoDenunciaMapping());
        }

    }
}
