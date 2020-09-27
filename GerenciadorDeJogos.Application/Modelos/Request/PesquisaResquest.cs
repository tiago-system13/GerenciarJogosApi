using GerenciadorDeJogos.Domain.Enum;

namespace GerenciadorDeJogos.Application.Models.Request
{
    public class PesquisaResquest
    {
        public string Nome { get; set; }
        public int IndiceDePagina { get; set; }
        public int RegistrosPorPagina { get; set; }
        public TipoDeOrdenacao Ordenacao { get; set; }
    }
}
