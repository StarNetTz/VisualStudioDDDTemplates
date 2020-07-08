using Starnet.Projections.ES;
using System.Collections.Generic;

namespace DomainName.ReadModel.Projections.ES
{
    public class EventStoreProjectionDefinitions
    {
        public static Dictionary<string, string> CreateEventStoreProjectionSources()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            //Add projection sources here
            return data;
        }

        static void Add(EventStoreProjectionParameters par, Dictionary<string, string> data)
        {
            var p = EventStoreProjectionBuilder.BuildProjectionDefinition(par);
            data.Add(p.Name, p.Source);
        }
    }
}
