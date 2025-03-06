using Confluent.Kafka;
using KafkaConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsole
{
    public class KafkaEventProducer : IEventProducer
	{
		private readonly string _bootstrapServers;
		private readonly string _outputTopic;

        public KafkaEventProducer(string bootstrapServers, string outputTopic)
        {
			_bootstrapServers = bootstrapServers;
			_outputTopic = outputTopic;
		}

		public void ProduceEvent(string enrichedEvent)
        {
			var producerConfig = new ProducerConfig
			{
				BootstrapServers = _bootstrapServers
			};

			using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
			{
				producer.Produce(_outputTopic, new Message<Null, string> { Value = enrichedEvent });
				Console.WriteLine($"Produced enriched message: {enrichedEvent}");
			}
		}
    }
}
