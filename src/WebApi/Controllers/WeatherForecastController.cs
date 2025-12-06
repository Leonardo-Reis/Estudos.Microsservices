using Estudos.Microsservices.Contratos;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Estudos.Microsservices.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller(ISendEndpointProvider sendEndpointProvider) : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;

        [HttpGet(Name = "GetSendMessage")]
        [Route("Message")]
        public async Task<string> GetSendMessage()
        {
            Console.WriteLine("Teste 3");
            var datetime = DateTime.Now;

            var busMessage = new BusMessage()
            {
                Message = $"Mensagem de teste! {datetime}"
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:SegundoTeste"));
            await endpoint.Send(busMessage);

            Console.WriteLine($"Enviado {datetime}");

            return "Hello World!";
        } 

        [HttpGet(Name = "GetTeste")]
        [Route("teste")]
        public string GetTeste()
        {
            Console.WriteLine("teste1");
            return "Teste Controller";
        }
    }
}
