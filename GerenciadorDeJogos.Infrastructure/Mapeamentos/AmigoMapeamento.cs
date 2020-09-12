using GerenciadorDeJogos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeJogos.Infrastructure.Mapeamentos
{
    public class AmigoMapeamento:IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("amigo_id");

            builder.Property(x => x.Nome)
               .HasColumnName("nome_amigo")
               .HasMaxLength(70)
               .IsRequired();

            builder.Property(x => x.Nome)
               .HasColumnName("telefone_amigo")
               .HasMaxLength(20)
               .IsRequired();

            builder.ToTable("tb_amigo");
        }
    }

}

