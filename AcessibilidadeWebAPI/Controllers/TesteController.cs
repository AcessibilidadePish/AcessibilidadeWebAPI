using AcessibilidadeWebAPI.Dtos.Testes;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AcessibilidadeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ILogger<TesteController> _logger;
        private readonly AzureMqttPushService mqttPush;

        public TesteController(ILogger<TesteController> logger, AzureMqttPushService mqttPush)
        {
            _logger = logger;
            this.mqttPush = mqttPush;
        }

        [HttpPost("TestePost")]
        public async Task TestePost(TesteDto aaaaaaaaaaaaaaa)
        {
            _logger.LogInformation("TestePost called with: {lat}, {lng}", aaaaaaaaaaaaaaa.lat, aaaaaaaaaaaaaaa.lng);
            _logger.LogInformation("Sending ACK to device via MQTT...");    
            await mqttPush.EnviarAckParaDispositivoAsync();
            _logger.LogInformation("ACK sent successfully.");
        }


        [HttpGet("TesteGet")]
        public string TesteGet([FromQuery]object testeDto)
        {
            return JsonSerializer.Serialize(testeDto, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
