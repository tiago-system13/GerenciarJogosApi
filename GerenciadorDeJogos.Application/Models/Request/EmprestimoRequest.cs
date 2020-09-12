using System;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class EmprestimoRequest:ModelBase
    {
        public Guid AmigoId { get; set; }

        public int QuantidadeDeDias { get; set; }

        public DateTime DataPrevistaDeVolucao { get; set; }

        public List<ItensEmprestadosRequest> ItensEmprestados { get; set; }

    }
}
