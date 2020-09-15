using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class PesquisaEmprestimoRequest:PesquisaResquest
    {
        public int? AmigoId { get; set; }

        public int? JogoId { get; set; }

        public DateTime? DataEmprestimo { get; set; }
    }
}
