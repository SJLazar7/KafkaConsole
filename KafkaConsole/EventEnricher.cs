using KafkaConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsole
{
    public class EventEnricher : IEventEnricher
	{
		public string EnrichEvent(string eventMessage)
		{
			// Enrich the event with simple logic (could be extended)
			return $"{eventMessage} - Enriched";
		}
	}
}
