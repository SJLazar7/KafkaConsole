using KafkaConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsole
{
    public class EventProcessor
    {
		private readonly IEventConsumer _eventConsumer;
		private readonly IEventProducer _eventProducer;
		private readonly IEventEnricher _eventEnricher;

		public EventProcessor(IEventConsumer eventConsumer, IEventProducer eventProducer, IEventEnricher eventEnricher)
		{
			_eventConsumer = eventConsumer;
			_eventProducer = eventProducer;
			_eventEnricher = eventEnricher;
		}

		public void ProcessEvents()
		{
			string consumedEvent = _eventConsumer.ConsumeEvent();
			Console.WriteLine($"Consumed event: {consumedEvent}");

			string enrichedEvent = _eventEnricher.EnrichEvent(consumedEvent);
			Console.WriteLine($"Enriched event: {enrichedEvent}");

			_eventProducer.ProduceEvent(enrichedEvent);
		}
	}
}
