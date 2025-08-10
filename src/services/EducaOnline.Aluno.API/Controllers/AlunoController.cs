using EducaOnline.Aluno.API.DTO;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Aluno.API.Services;
using EducaOnline.Core.Communication;
using EducaOnline.Core.DomainObjects;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.WebAPI.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : MainController
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(INotificationHandler<DomainNotification> notifications,
                               IMediatorHandler mediatorHandler,
                               IAlunoService alunoService)
            : base(notifications, mediatorHandler)
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno([FromBody] AlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro("InvalidData", "Dados do aluno inválidos.");
                return BadRequest(ObterMensagensErro());
            }

            try
            {
                var aluno = await _alunoService.CriarAluno(alunoDto.Nome, alunoDto.Email);
                return Ok(aluno);
            }
            catch (DomainException ex)
            {
                NotificarErro("AlunoErro", ex.Message);
                return BadRequest(ObterMensagensErro());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAlunoPorId(Guid id)
        {
            try
            {
                var aluno = await _alunoService.ObterAlunoPorId(id);
                if (aluno == null)
                {
                    NotificarErro("AlunoNaoEncontrado", "Aluno não encontrado");
                    return NotFound(ObterMensagensErro());
                }

                return Ok(aluno);
            }
            catch (DomainException ex)
            {
                NotificarErro("AlunoErro", ex.Message);
                return BadRequest(ObterMensagensErro());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAluno(Guid id, [FromBody] AlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro("InvalidData", "Dados do aluno inválidos.");
                return BadRequest(ObterMensagensErro());
            }

            try
            {
                var aluno = await _alunoService.ObterAlunoPorId(id);
                if (aluno == null)
                {
                    NotificarErro("AlunoNaoEncontrado", "Aluno não encontrado");
                    return NotFound(ObterMensagensErro());
                }

                aluno = new Aluno.API.Models.Aluno(id, alunoDto.Nome, alunoDto.Email);
                await _alunoService.AtualizarAluno(aluno);
                return Ok(aluno);
            }
            catch (DomainException ex)
            {
                NotificarErro("AlunoErro", ex.Message);
                return BadRequest(ObterMensagensErro());
            }
        }

        [HttpPost("matricular")]
        public async Task<IActionResult> RealizarMatricula([FromBody] MatriculaDto matriculaDto)
        {
            try
            {
                await _alunoService.RealizarMatricula(matriculaDto.AlunoId, matriculaDto.CursoId, matriculaDto.TotalAulas);
                return Ok("Matrícula realizada com sucesso!");
            }
            catch (DomainException ex)
            {
                NotificarErro("MatriculaErro", ex.Message);
                return BadRequest(ObterMensagensErro());
            }
        }

        [HttpPost("concluir-aula")]
        public async Task<IActionResult> ConcluirAula([FromBody] AulaDto aulaDto)
        {
            try
            {
                await _alunoService.ConcluirAula(aulaDto.AlunoId, aulaDto.AulaId, aulaDto.Horas);
                return Ok("Aula concluída com sucesso!");
            }
            catch (DomainException ex)
            {
                NotificarErro("AulaErro", ex.Message);
                return BadRequest(ObterMensagensErro());
            }
        }

        [HttpPost("emitir-certificado")]
        public async Task<IActionResult> EmitirCertificado([FromBody] CertificadoDto certificadoDto)
        {
            try
            {
                var certificado = new Certificado(certificadoDto.Curso);
                await _alunoService.EmitirCertificado(certificadoDto.AlunoId, certificado);
                return Ok("Certificado emitido com sucesso!");
            }
            catch (DomainException ex)
            {
                NotificarErro("CertificadoErro", ex.Message);
                return BadRequest(ObterMensagensErro());
            }
        }
    }
}

