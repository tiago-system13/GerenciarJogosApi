using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class PesquisaEmprestimoRequest:PesquisaResquest
    {
        public Guid? AmigoId { get; set; }

        public Guid? JogoId { get; set; }

        public DateTime? DataEmprestimo { get; set; }
    }
}
