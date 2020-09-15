using GerenciadorDeJogos.Application.Models.Request;
using System;

namespace GerenciadorDeJogos.Application.Models.Result
{
    public class JogoResult: ModelBase
    {
        public string Nome { get; set; }
        public int ProprietarioId { get; set; }
        public string NomeProprietario { get; set; }
    }
}
