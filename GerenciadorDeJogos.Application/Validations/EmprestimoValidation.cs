﻿using FluentValidation;
using GerenciadorDeJogos.Domain.Constantes;
using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.Validations
{
    public class EmprestimoValidation: AbstractValidator<Emprestimo>
    {
        public EmprestimoValidation()
        {
            RuleFor(a => a.AmigoId)
           .NotEqual(0)
           .WithMessage(Mensagens.AMIGOOBRIGATORIO);

            RuleFor(a => a.QuantidadeDeDias)
           .NotEqual(0)
           .WithMessage(Mensagens.QUANTIDADEDEDIAS);

            RuleForEach(a => a.ItensEmprestados).NotEmpty()
            .Must((itemPrestado)=> itemPrestado.JogoId > 0)
            .WithMessage(Mensagens.JOGOOBRIGATORIO);
        }
    }
}
