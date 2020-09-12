using GerenciadorDeJogos.Domain.Enum;

namespace GerenciadorDeJogos.Domain.Entidades.Base
{
    public class Pesquisa
    {
        public string Nome { get; set; }
        public int IndiceDePagina { get; set; }

        public int RegistrosPorPagina { get; set; }

        public TipoDeOrdenacao Ordenacao { get; set; }

    }
}
