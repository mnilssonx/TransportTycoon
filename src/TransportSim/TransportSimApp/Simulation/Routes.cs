using System.Collections.Generic;
using System.Linq;

namespace TransportSimApp.Simulation
{
    public class Routes
    {
        private readonly IDictionary<Location, Route> _map;

        public Routes(params Route[] routes)
        {
            _map = routes.ToDictionary(r => r.Destination, r => r);
        }

        public Route ForDestination(Location destination) => _map[destination];

        public Location[] Destinations => _map.Values.Select(r => r.Destination).ToArray();

        
    }
}
