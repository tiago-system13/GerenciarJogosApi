﻿using GerenciadorDeJogos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeJogos.Infrastructure.Mapeamentos
{
    public class JogoMapeamento : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("jogo_id");

            builder.Property(x => x.Nome)
               .HasColumnName("nome_jogo")
               .HasMaxLength(40)
               .IsRequired();

            builder.Property(x => x.ProprietarioId)
               .HasColumnName("proprietario_id")
               .HasMaxLength(40)
               .IsRequired();

            builder.HasOne(x => x.Proprietario)
              .WithMany(o => o.Jogos)
              .HasForeignKey(x => x.ProprietarioId);

            builder.ToTable("tb_jogo");
        }
    }
}
