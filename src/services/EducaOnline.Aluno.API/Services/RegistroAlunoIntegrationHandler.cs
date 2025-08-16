using FluentValidation.Results;
using EducaOnline.Core.Messages.Integration;
using EducaOnline.MessageBus;
using EducaOnline.Core.Communication;

namespace EducaOnline.Clientes.API.Services
{
    public class RegistroAlunoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroAlunoIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
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
            //Alterar esse código para utilizar o serviço de cadastro de Aluno e este deve retornar o ValidationResult
                      

            return new ResponseMessage(new ValidationResult());
        }
    }
}
