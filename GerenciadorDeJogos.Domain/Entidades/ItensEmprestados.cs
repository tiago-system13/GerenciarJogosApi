using GerenciadorDeJogos.Domain.Entidades.Base;
using System;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class ItensEmprestados: Entidade
    {
        public int JogoId { get; set; }

        public int EmprestimoId { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public bool? Devolvido { get; set; }

        public Jogo Jogo { get; set; }

        public Emprestimo Emprestimo { get; set; }
    }
}
