using GerenciadorDeJogos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeJogos.Infrastructure.Mapeamentos
{
    public class UsuarioMapeamento:IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("usuario_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
               .HasColumnName("nome")
               .HasMaxLength(70)
               .IsRequired();

            builder.Property(x => x.Login)
               .HasColumnName("login")
               .HasMaxLength(40)
               .IsRequired();

            builder.Property(x => x.Senha)
               .HasColumnName("senha")
               .IsRequired();

            builder.ToTable("tb_usuario");
        }
    }
}
