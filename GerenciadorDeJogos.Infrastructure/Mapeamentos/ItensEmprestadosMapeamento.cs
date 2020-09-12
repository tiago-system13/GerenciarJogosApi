using GerenciadorDeJogos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeJogos.Infrastructure.Mapeamentos
{
    public class ItensEmprestadosMapeamento:IEntityTypeConfiguration<ItensEmprestados>
    {
        public void Configure(EntityTypeBuilder<ItensEmprestados> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("item_emprestado_id");

            builder.Property(x => x.JogoId)
               .HasColumnName("jogo_id")
               .IsRequired();

            builder.Property(x => x.EmprestimoId)
               .HasColumnName("emprestimo_id")
               .IsRequired();

            builder.Property(x => x.DataDevolucao)
              .HasColumnName("data_devolucao");

            builder.Property(x => x.Devolvido)
             .HasColumnName("devolvido");

            builder.HasOne(x => x.Jogo)
              .WithMany(o => o.ItensEmprestados)
              .HasForeignKey(x => x.JogoId);

            builder.HasOne(x => x.Emprestimo)
              .WithMany(o => o.ItensEmprestados)
              .HasForeignKey(x => x.EmprestimoId);

            builder.ToTable("tb_item_emprestado");
        }
    }
}
