using Newtonsoft.Json;
using RabbitMQ.Client;

using System.Text;
using System.Threading.Tasks;


namespace MS_CITAS.Application.Services.Implementation
{
    public class EmisorMQ
    {
        private const string QueueName = "recetas_queue";
        private const string ExchangeName = "recetas_exchange";

        public async Task PublicarMensaje(object message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            {
                await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Fanout, durable: true);
                //durable: Sobrevivi al reinicio del brocker
                //exclusie: Pdora ser utilizada por otras conexiones
                //autoDelete:La borra se borrara cuando el consumidor este abajo

                await channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                await channel.QueueBindAsync(QueueName, ExchangeName, "");

                var jsonMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);



                await channel.BasicPublishAsync(exchange: ExchangeName, routingKey: "", body: body);
            }
        }
    }
}