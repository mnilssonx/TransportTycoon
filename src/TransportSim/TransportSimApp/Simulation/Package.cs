using System.Linq;

namespace TransportSimApp.Simulation
{
    public class Package
    {
        public Package(Location location, Route deliveryRoute)
        {
            Location = location;
            DeliveryRoute = deliveryRoute;
        }

        public Route DeliveryRoute { get; }

        public Location Location { get; set; }

        public void Unload(Location destination)
        {
            Location = destination;
        }

        public RouteSegment GetNextDeliverySegment()
        {
            return DeliveryRoute.Segments.Single(s => s.Start == Location);
        }
    }
}
