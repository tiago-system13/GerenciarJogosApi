using GerenciadorDeJogos.Application.Models.Request;
using System;

namespace GerenciadorDeJogos.Application.Models.Result
{
    public class ItensEmprestadosResult:ModelBase
    {
        public Guid JogoId { get; set; }

        public string NomeJogo { get; set; }

        public Guid EmprestimoId { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public bool? Devolvido { get; set; }
    }
}
