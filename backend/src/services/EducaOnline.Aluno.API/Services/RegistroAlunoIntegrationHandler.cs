using FluentValidation.Results;
using EducaOnline.Core.Messages.Integration;
using EducaOnline.Core.Communication;
using EducaOnline.Aluno.API.Application.Commands;
using EducaOnline.MessageBus;

namespace EducaOnline.Aluno.API.Services
{
    public class RegistroAlunoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroAlunoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarAluno(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object? s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarAluno(UsuarioRegistradoIntegrationEvent message)
        {
            var command = new AdicionarAlunoCommand(message.Id, message.Nome, message.Email);
            ValidationResult resultado;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                resultado = await mediator.EnviarComando(command);
            }

            return new ResponseMessage(resultado);
        }
    }
}
