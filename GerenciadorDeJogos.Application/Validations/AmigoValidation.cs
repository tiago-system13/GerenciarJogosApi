using FluentValidation;
using GerenciadorDeJogos.Domain.Constantes;
using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.Validations
{
    public class AmigoValidation: AbstractValidator<Amigo>
    {
        public AmigoValidation()
        {
            RuleFor(a => a.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage(Mensagens.NOMEOBRIGATORIO);

            RuleFor(a => a.Nome)
           .MaximumLength(40)
           .WithMessage(Mensagens.NOMEAMIGOMAXILENGTH);

            RuleFor(a => a.Telefone)
            .NotNull()
            .NotEmpty()
            .WithMessage(Mensagens.TELEFONEOBRIGATORIO);

            RuleFor(a => a.Telefone)
          .MaximumLength(20)
          .WithMessage(Mensagens.TELEFONEMAXILENGTH);
        }
    }
}
