using EducaOnline.Core.Messages;
using FluentValidation;

namespace EducaOnline.Aluno.API.Application.Commands
{
    public class PagarMatriculaCommand : Command
    {
        public PagarMatriculaCommand(Guid alunoId, Guid cursoId, string? nomeCartao, string? numeroCartao, string? expiracaoCartao, string? cvvCartao, decimal valorCurso)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
            ValorCurso = valorCurso;
        }

        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string? NomeCartao { get; set; }
        public string? NumeroCartao { get; set; }
        public string? ExpiracaoCartao { get; set; }
        public string? CvvCartao { get; set; }
        public decimal ValorCurso { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new PagarMatriculaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PagarMatriculaValidation : AbstractValidator<PagarMatriculaCommand>
    {
        public PagarMatriculaValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do aluno não informado");

            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Curso não informado.");

            RuleFor(c => c.NomeCartao)
                .NotEqual(string.Empty)
                .WithMessage("Nome do cartão inválido.");

            RuleFor(c => c.NumeroCartao)
              .NotEqual(string.Empty)
              .WithMessage("Numero do cartão não informado")
              .MinimumLength(16)
              .WithMessage("Numero do cartão inválido");

            RuleFor(c => c.ExpiracaoCartao)
              .NotEqual(string.Empty)
              .WithMessage("Expiracao do cartão não informada");

            RuleFor(c => c.CvvCartao)
              .NotEqual(string.Empty)
              .WithMessage("Ccv não informado")
              .MinimumLength(3)
              .WithMessage("Ccv inválido");
        }
    }
}
