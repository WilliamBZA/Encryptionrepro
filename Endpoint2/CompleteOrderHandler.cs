using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class CompleteOrderHandler :
    IHandleMessages<CompleteOrder>
{
    static int processCount = 0;
    static ILog log = LogManager.GetLogger<CompleteOrderHandler>();

    public Task Handle(CompleteOrder message, IMessageHandlerContext context)
    {
        if (processCount++ == 0)
        {
            // Fail on the first attempt
            throw new System.NotImplementedException();
        }

        log.Info($"Received CompleteOrder with credit card number {message.CreditCard}");
        return Task.CompletedTask;
    }
}