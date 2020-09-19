using System;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class DevolucaoRequest:ModelBase
    {
        public List<ItensDevolvidosRequest> ItensDevolvidos { get; set; }
    }
}
