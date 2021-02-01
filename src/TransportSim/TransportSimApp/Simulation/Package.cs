using System.Linq;

namespace TransportSimApp.Simulation
{
    public class Package
    {
        public Package(int id, Location location, Route deliveryRoute)
        {
            Id = id;
            Location = location;
            DeliveryRoute = deliveryRoute;
        }

        public int Id { get; set; }
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
