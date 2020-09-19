using GerenciadorDeJogos.Application.Models.Request;
using System;

namespace GerenciadorDeJogos.Application.Models.Responses
{
    public class JogoResponse: ModelBase
    {
        public string Nome { get; set; }
        public int ProprietarioId { get; set; }
        public string NomeProprietario { get; set; }
    }
}
