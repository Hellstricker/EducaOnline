using EducaOnline.Aluno.API.Application.Commands;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Aluno.API.Models.Enum;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using FluentValidation.Results;
using MediatR;

public class AlunoCommandHandler : CommandHandler,
    IRequestHandler<AdicionarAlunoCommand, ValidationResult>,
    IRequestHandler<AtualizarAlunoCommand, ValidationResult>,
    IRequestHandler<RealizarMatriculaCommand, ValidationResult>,
    IRequestHandler<ConcluirAulaCommand, ValidationResult>,
    IRequestHandler<EmitirCertificadoCommand, ValidationResult>
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IAlunoRepository _alunoRepository;

    public AlunoCommandHandler(IMediatorHandler mediatorHandler, IAlunoRepository alunoRepository)
    {
        _mediatorHandler = mediatorHandler;
        _alunoRepository = alunoRepository;
    }

    public async Task<ValidationResult> Handle(AdicionarAlunoCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return request.ValidationResult;

        var aluno = new Aluno(request.Id, request.Nome, request.Email);

        var ra = !_alunoRepository.BuscarAlunos().Any()
            ? 10000
            : (await _alunoRepository.BuscarUltimoRa()) + 1;

        aluno.VincularRa(ra);

        await _alunoRepository.AdicionarAluno(aluno);
        return await PersistirDados(_alunoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarAlunoCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return request.ValidationResult;

        var aluno = await _alunoRepository.BuscarAlunoPorId(request.Id);
        if (aluno == null)
        {
            AdicionarErro("Aluno não encontrado.");
            return ValidationResult;
        }

        aluno.AtualizarDados(request.Nome, request.Email);

        await _alunoRepository.AtualizarAluno(aluno);
        return await PersistirDados(_alunoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RealizarMatriculaCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return request.ValidationResult;

        var aluno = await _alunoRepository.BuscarAlunoPorId(request.AlunoId);
        if (aluno == null)
        {
            AdicionarErro("Aluno não encontrado.");
            return ValidationResult;
        }

        if (aluno.Matricula != null && aluno.Matricula.Status != StatusMatriculaEnum.CANCELADO)
        {
            AdicionarErro("Aluno já está com uma matrícula ativa.");
            return ValidationResult;
        }

        var matricula = new Matricula(
            alunoId: request.AlunoId,
            cursoId: request.CursoId,
            cursoNome: request.CursoNome,
            totalAulas: request.TotalAulas,
            cargaHorariaTotal: request.CargaHorariaTotal
        );

        aluno.RealizarMatricula(matricula);

        await _alunoRepository.AtualizarAluno(aluno);
        return await PersistirDados(_alunoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(ConcluirAulaCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return request.ValidationResult;

        var aluno = await _alunoRepository.BuscarAlunoPorId(request.AlunoId);
        if (aluno == null)
        {
            AdicionarErro("Aluno não encontrado.");
            return ValidationResult;
        }

        aluno.ConcluirAula(request.AulaId);

        await _alunoRepository.AtualizarAluno(aluno);
        return await PersistirDados(_alunoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(EmitirCertificadoCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return request.ValidationResult;

        var aluno = await _alunoRepository.BuscarAlunoPorId(request.AlunoId);
        if (aluno == null)
        {
            AdicionarErro("Aluno não encontrado.");
            return ValidationResult;
        }

        if (aluno.Matricula == null)
        {
            AdicionarErro("Aluno não possui matrícula.");
            return ValidationResult;
        }

        if (!aluno.Matricula.PodeEmitirCertificado())
        {
            AdicionarErro("Progresso insuficiente para emitir certificado.");
            return ValidationResult;
        }

        aluno.EmitirCertificado(new Certificado(aluno.Matricula.CursoNome));

        await _alunoRepository.AtualizarAluno(aluno);
        return await PersistirDados(_alunoRepository.UnitOfWork);
    }

    private bool ValidarComando(Command message)
    {
        if (message.EhValido()) return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            AdicionarErro(error.ErrorMessage);
        }

        return false;
    }
}
