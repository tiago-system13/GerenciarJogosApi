using System.Collections.Generic;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class EmprestimoRequest
    {
        public int AmigoId { get; set; }

        public int QuantidadeDeDias { get; set; }

        public List<ItensEmprestadosRequest> ItensEmprestados { get; set; }

    }
}
