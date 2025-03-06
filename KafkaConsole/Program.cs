using KafkaConsole.Interfaces;
using KafkaConsole;

class Program
{
	static void Main(string[] args)
	{
		string bootstrapServers = "localhost:9092";
		string inputTopic = "input-topic";
		string outputTopic = "output-topic";

		// Dependency Injection (You can also use DI frameworks like Microsoft.Extensions.DependencyInjection)
		IEventConsumer eventConsumer = new KafkaEventConsumer(bootstrapServers, inputTopic);
		IEventProducer eventProducer = new KafkaEventProducer(bootstrapServers, outputTopic);
		IEventEnricher eventEnricher = new EventEnricher();

		var eventProcessor = new EventProcessor(eventConsumer, eventProducer, eventEnricher);
		eventProcessor.ProcessEvents();
	}
}
