using EducaOnline.Aluno.API.Application.Commands; 
using EducaOnline.Aluno.API.Dto;
using EducaOnline.Aluno.API.DTO;
using EducaOnline.Aluno.API.Models;
using EducaOnline.Core.Communication;
using EducaOnline.MessageBus;
using EducaOnline.WebAPI.Core.Controllers;
using EducaOnline.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace EducaOnline.Aluno.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : MainController
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IMessageBus _messageBus;
        private readonly IAspNetUser _user;

        public AlunosController(
            IAlunoRepository alunoRepository,
            IMediatorHandler mediator,
            IMessageBus messageBus,
            IAspNetUser user)
        {
            _alunoRepository = alunoRepository;
            _mediator = mediator;
            _messageBus = messageBus;
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno([FromBody] AlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
            {
                AdicionarErro("Dados do aluno inválidos.");
                return CustomResponse();
            }

            var novoId = Guid.NewGuid();
            var cmd = new AdicionarAlunoCommand(novoId, alunoDto.Nome, alunoDto.Email);

            var result = await _mediator.EnviarComando(cmd);
            if(!result.IsValid)
                return CustomResponse(result);

            // Publicar evento caso necessário
            return Ok("Aluno criado com sucesso!");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterAlunoPorId(Guid id, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorId(id, cancellationToken);
            if (aluno == null)
            {
                AdicionarErro("Aluno não encontrado");
                return CustomResponse();
            }
            return Ok(aluno);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarAluno(Guid id, [FromBody] AlunoDto alunoDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                AdicionarErro("Dados do aluno inválidos.");
                return CustomResponse();
            }
            var cmd = new AtualizarAlunoCommand(id, alunoDto.Nome, alunoDto.Email);

            var result = await _mediator.EnviarComando(cmd);
            if (!result.IsValid)
                return CustomResponse(result);

            var aluno = await _alunoRepository.BuscarAlunoPorId(id, cancellationToken);
            return CustomResponse(aluno);
        }

        [HttpPost("matricular")]
        public async Task<IActionResult> RealizarMatricula([FromBody] MatriculaDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var cmd = new RealizarMatriculaCommand(
                alunoId: dto.AlunoId,
                cursoId: dto.CursoId,
                cursoNome: dto.CursoNome,
                totalAulas: dto.TotalAulas,
                cargaHorariaTotal: dto.CargaHorariaTotal
            );

            var result = await _mediator.EnviarComando(cmd);
            return CustomResponse(result);
        }

        [HttpPost("emitir-certificado")]
        public async Task<IActionResult> EmitirCertificado([FromBody] CertificadoDto certificadoDto)
        {
            // Command recebe CursoId e AlunoId; o handler decide emitir e atualiza status
            var cmd = new EmitirCertificadoCommand(
                cursoId: certificadoDto.CursoId,
                alunoId: certificadoDto.AlunoId
            );

            var result = await _mediator.EnviarComando(cmd);
            if (!result.IsValid)
                return CustomResponse(result);

            return CustomResponse("Certificado emitido com sucesso!");
        }
    }
}
