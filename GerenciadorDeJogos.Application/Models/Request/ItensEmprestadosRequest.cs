using System;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class ItensEmprestadosRequest:ModelBase
    {
        public Guid JogoId { get; set; }

        public Guid EmprestimoId { get; set; }
    }
}
