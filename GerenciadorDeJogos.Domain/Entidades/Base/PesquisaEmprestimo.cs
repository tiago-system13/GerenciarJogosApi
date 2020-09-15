using System;

namespace GerenciadorDeJogos.Domain.Entidades.Base
{
    public class PesquisaEmprestimo: Pesquisa
    {
        public int? AmigoId { get; set; }

        public int? JogoId { get; set; }

        public DateTime? DataEmprestimo { get; set; }
    }
}
