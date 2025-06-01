using Microsoft.Azure.Devices;
using System;
using System.Text;
using System.Threading.Tasks;

public class AzureMqttPushService
{
    private readonly string connectionString = "HostName=iot-pish.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=f+aLiFlK7BGvIuD8bKiKmVTgxWyb9k9ocAIoTBvI7tA=";
    private readonly string deviceId = "esp32_gps_01";

    private readonly ServiceClient serviceClient;

    public AzureMqttPushService()
    {
        serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
    }

    public async Task EnviarAckParaDispositivoAsync()
    {
        var message = new Message(Encoding.UTF8.GetBytes("EN_ROUTE"))
        {
            Ack = DeliveryAcknowledgement.Full,
            ExpiryTimeUtc = DateTime.UtcNow.AddMinutes(5)
        };

        await serviceClient.SendAsync(deviceId, message);
        Console.WriteLine("[MQTT → ESP] Mensagem 'EN_ROUTE' enviada via C2D.");
    }
}
