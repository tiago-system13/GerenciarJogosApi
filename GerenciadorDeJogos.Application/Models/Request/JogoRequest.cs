using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class JogoRequest: ModelBase
    {
        public string Nome { get; set; }
        public Guid ProprietarioId { get; set; }
    }
}
