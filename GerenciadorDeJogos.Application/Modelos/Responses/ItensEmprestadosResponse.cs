using GerenciadorDeJogos.Application.Models.Request;
using System;

namespace GerenciadorDeJogos.Application.Models.Responses
{
    public class ItensEmprestadosResponse:ModelBase
    {
        public int JogoId { get; set; }

        public string NomeJogo { get; set; }

        public int EmprestimoId { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public bool? Devolvido { get; set; }
    }
}
