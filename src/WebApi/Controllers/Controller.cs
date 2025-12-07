using Estudos.Microsservices.Contratos;
using Estudos.Microsservices.Infra.Data.SqlServer;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.Microsservices.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller(ISendEndpointProvider sendEndpointProvider,
                            AppDbContext appDbContext) : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpPost(Name = "PostOfertaProduto")]
        [Route("OfertaProduto")]
        public async Task<string> PostOfertaProduto([FromBody] OfertaProdutoEntrada entrada)
        {
            var datetime = DateTime.Now;

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:SegundoTeste"));
            await endpoint.Send(busMessage);

            Console.WriteLine($"Enviado {datetime}");

            return "Hello World!";
        }

        [HttpGet(Name = "GetSendMessage")]
        [Route("Message")]
        public async Task<string> GetSendMessage()
        {
            var datetime = DateTime.Now;
            Console.WriteLine($"Enviando mensagem {datetime}");

            var message = $"Mensagem de teste! {datetime}";

            var busMessage = new BusMessage()
            {
                Message = message
            };

            var busMessageSqlDb = new BusMessageSqlDb(message, datetime);

            await _appDbContext.BusMessage.AddAsync(busMessageSqlDb);
            await _appDbContext.SaveChangesAsync();

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
