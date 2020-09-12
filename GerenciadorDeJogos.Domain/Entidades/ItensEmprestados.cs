using GerenciadorDeJogos.Domain.Entidades.Base;
using System;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class ItensEmprestados: Entidade
    {
        public Guid JogoId { get; set; }

        public Guid EmprestimoId { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public bool? Devolvido { get; set; }

        public Jogo Jogo { get; set; }

        public Emprestimo Emprestimo { get; set; }
    }
}
