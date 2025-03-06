using Confluent.Kafka;
using KafkaConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsole
{
	public class KafkaEventConsumer : IEventConsumer
	{
		private readonly string _topic;
		private readonly string _bootstrapServers;

		public KafkaEventConsumer(string topic, string bootstrapServers)
		{
			_topic = topic;
			_bootstrapServers = bootstrapServers;
		}

		public string ConsumeEvent()
		{
			var consumerConfig = new ConsumerConfig
			{
				GroupId = "test-consumer-group",
				BootstrapServers = _bootstrapServers,
				AutoOffsetReset = AutoOffsetReset.Earliest
			};

			using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
			{
				consumer.Subscribe(_topic);
				var consumeResult = consumer.Consume();
				return consumeResult.Message.Value;
			}
		}
	}
}
