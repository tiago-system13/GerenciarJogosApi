using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class Emprestimo: Entidade
    {
        public Guid AmigoId { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public int QuantidadeDeDias { get; set; }

        public DateTime DataPrevistaDeVolucao { get; set; }

        public Amigo Amigo { get; set; }

        public List<ItensEmprestados> ItensEmprestados { get; set; }
    }
}
