using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class Jogo: Entidade
    {
        public string Nome { get; set; }

        public int ProprietarioId { get; set; }

        public Amigo Proprietario { get; set; }

        public List<ItensEmprestados> ItensEmprestados { get; set; }
    }
}
