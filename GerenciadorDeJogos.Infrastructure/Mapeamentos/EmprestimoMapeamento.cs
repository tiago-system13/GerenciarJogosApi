using GerenciadorDeJogos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeJogos.Infrastructure.Mapeamentos
{
    public class EmprestimoMapeamento:IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("emprestimo_id");

            builder.Property(x => x.AmigoId)
               .HasColumnName("amigo_id")
               .IsRequired();

            builder.Property(x => x.DataEmprestimo)
               .HasColumnName("data_emprestimo")
               .IsRequired();

            builder.Property(x => x.QuantidadeDeDias)
               .HasColumnName("qtd_dia")
               .IsRequired();

            builder.Property(x => x.DataPrevistaDeVolucao)
                .HasColumnName("data_prevista_devolucao")
                .IsRequired();

            builder.HasOne(x => x.Amigo)
             .WithMany(o => o.Emprestimos)
             .HasForeignKey(x => x.AmigoId);

            builder.ToTable("tb_emprestimo");
        }
    }
}
