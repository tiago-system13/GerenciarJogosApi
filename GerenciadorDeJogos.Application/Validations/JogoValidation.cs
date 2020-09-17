﻿using FluentValidation;
using GerenciadorDeJogos.Domain.Constantes;
using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.Validations
{
    public class JogoValidation: AbstractValidator<Jogo>
    {
        public JogoValidation()
        {
            RuleFor(a => a.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage(Mensagens.NOMEOBRIGATORIO);

            RuleFor(a => a.Nome)
           .MaximumLength(40)
           .WithMessage(Mensagens.NOMEJOGOMAXILENGTH);

            RuleFor(a => a.ProprietarioId).NotEqual(0)
            .WithMessage(Mensagens.PROPRIETARIOOBRIGATORIO);
        }
    }
}
