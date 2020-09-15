using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class ItensDevolvidosRequest 
    {
        public int EmprestimoId { get; set; }

        public int JogoId { get; set; }

        public bool? Devolvido { get; set; }

    }
}
