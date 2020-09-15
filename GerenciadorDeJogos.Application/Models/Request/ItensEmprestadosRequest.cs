using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class ItensEmprestadosRequest:ModelBase
    {
        public int JogoId { get; set; }

        public int EmprestimoId { get; set; }
    }
}
