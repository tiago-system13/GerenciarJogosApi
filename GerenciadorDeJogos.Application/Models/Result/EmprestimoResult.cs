using GerenciadorDeJogos.Application.Models.Request;
using System;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Application.Models.Result
{
    public class EmprestimoResult: ModelBase
    {
        public Guid AmigoId { get; set; }

        public string NomeAmigo { get; set; }

        public int QuantidadeDeDias { get; set; }

        public DateTime DataPrevistaDeVolucao { get; set; }

        List<ItensEmprestadosResult> ItensEmprestados { get; set; }
    }
}
