using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.DomainEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;

namespace EducaOnline.Core.Communication
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;

        //Há uma modificação neste curso para a execução do command retornar o ValidationResult
        //para que seja possível verificar se o comando foi executado com sucesso ou não e já pegar os erros.
        //Como a assinatura é a mesma, deixei comentado para modificar posteriormente a utilização do anterior
        //Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
