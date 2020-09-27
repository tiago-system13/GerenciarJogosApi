using FluentValidation;
using GerenciadorDeJogos.Domain.Constantes;
using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.Validations
{
    public class UsuarioValidacao : AbstractValidator<Usuario>
    {
       

        public UsuarioValidacao()
        {
            RuleFor(a => a.Nome)
            .NotNull().WithMessage(Mensagens.NOMEOBRIGATORIO)
            .NotEmpty().WithMessage(Mensagens.NOMEOBRIGATORIO);

            RuleFor(a => a.Nome)
           .MaximumLength(70)
           .WithMessage(Mensagens.NOMENMAXILENGTH);

            RuleFor(a => a.Login)
            .NotNull().WithMessage(Mensagens.LOGINOBRIGATORIO)
            .NotEmpty().WithMessage(Mensagens.LOGINOBRIGATORIO);

            RuleFor(a => a.Login)
          .MaximumLength(40)
          .WithMessage(Mensagens.LOGINMAXILENGTH);

            RuleFor(a => a.Senha)
            .NotNull().WithMessage(Mensagens.SENHAOBRIGATORIO)
            .NotEmpty().WithMessage(Mensagens.SENHAOBRIGATORIO);

            RuleFor(a => a.Senha)
          .MaximumLength(30)
          .WithMessage(Mensagens.SENHANMAXILENGTH);
        }
    }
}
