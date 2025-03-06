using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsole.Interfaces
{
    public interface IEventEnricher
    {
        string EnrichEvent(string eventMessage);
	}
}
