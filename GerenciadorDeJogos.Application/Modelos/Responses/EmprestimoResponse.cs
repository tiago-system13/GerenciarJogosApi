using GerenciadorDeJogos.Application.Models.Request;
using System;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Application.Models.Responses
{
    public class EmprestimoResponse: ModelBase
    {
        public int AmigoId { get; set; }

        public string NomeAmigo { get; set; }

        public int QuantidadeDeDias { get; set; }

        public DateTime DataPrevistaDeVolucao { get; set; }

        public List<ItensEmprestadosResponse> ItensEmprestados { get; set; }
    }
}
