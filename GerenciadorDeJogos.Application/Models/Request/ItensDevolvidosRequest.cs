using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class ItensDevolvidosRequest 
    {
        public Guid EmprestimoId { get; set; }

        public Guid JogoId { get; set; }

        public bool? Devolvido { get; set; }

    }
}
