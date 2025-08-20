using EducaOnline.Core.Messages;
using FluentValidation;

namespace EducaOnline.Aluno.API.Application.Commands
{
    public class AtualizarAlunoCommand : Command
    {
        public AtualizarAlunoCommand(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarAlunoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarAlunoValidation : AbstractValidator<AtualizarAlunoCommand>
    {
        public AtualizarAlunoValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do aluno inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("Nome não informado.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("E-mail inválido.");
        }
    }
}
