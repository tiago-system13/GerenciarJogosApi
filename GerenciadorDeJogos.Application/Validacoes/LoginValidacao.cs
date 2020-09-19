using FluentValidation;
using GerenciadorDeJogos.Application.Models.Request;

namespace GerenciadorDeJogos.Application.Validations
{
    public class LoginValidacao : AbstractValidator<LoginRequest>
    {
        public LoginValidacao ()
        {
            RuleFor(a => a.Login)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo login é obrigatório");

            RuleFor(a => a.Login)
          .MaximumLength(40)
          .WithMessage("O campo login suporta no máximo 40 caracteres");

            RuleFor(a => a.Senha)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo senha é obrigatório");

            RuleFor(a => a.Senha)
          .MaximumLength(30)
          .WithMessage("O campo senha suporta no máximo 30 caracteres");
        }
    }
}
