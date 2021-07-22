using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.MessageBodyEncryption.Endpoint2";
        var endpointConfiguration = new EndpointConfiguration("Samples.MessageBodyEncryption.Endpoint2");

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString("[Connection string]");

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.Recoverability().Immediate(a => a.NumberOfRetries(0));
        endpointConfiguration.Recoverability().Delayed(a => a.NumberOfRetries(0));

        endpointConfiguration.SendFailedMessagesTo("error");

        endpointConfiguration.RegisterMessageEncryptor();
        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}