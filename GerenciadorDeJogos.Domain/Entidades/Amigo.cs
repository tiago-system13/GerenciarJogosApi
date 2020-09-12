using GerenciadorDeJogos.Domain.Entidades.Base;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class Amigo: Entidade
    { 
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public List<Jogo> Jogos { get; set; }

        public List<Emprestimo> Emprestimos { get; set; }
    }
}
