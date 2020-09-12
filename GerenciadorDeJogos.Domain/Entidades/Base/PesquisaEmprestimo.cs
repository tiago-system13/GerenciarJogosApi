using System;

namespace GerenciadorDeJogos.Domain.Entidades.Base
{
    public class PesquisaEmprestimo: Pesquisa
    {
        public Guid? AmigoId { get; set; }

        public Guid? JogoId { get; set; }

        public DateTime? DataEmprestimo { get; set; }
    }
}
