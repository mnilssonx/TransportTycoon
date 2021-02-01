using System.Linq;
using TransportSimApp.Simulation;

namespace TransportSimApp.Events
{
    public class TransportEvent
    {
        public TransportEvent(EventType eventType, int time, Transport transport, Location location)
        {
            Event = eventType;
            Time = time;
            TransportId = transport.Id;
            Kind = transport.Type;
            Location = location;

            if (eventType == EventType.DEPART)
                Destination = transport.RouteSegment.Destination;  

            if (transport.Package != null)
            {
                Cargo = new[]
                {
                    new CargoSection(
                        transport.Package.Id,
                        transport.Package.DeliveryRoute.Destination,
                        transport.Package.DeliveryRoute.Segments.First().Start)
                };
            }
        }

        public EventType Event { get; set; }
        public int Time { get; set; }
        public int TransportId { get; set; }
        public TransportType Kind { get; set; }
        public Location Location { get; set; }
        public Location? Destination { get; set; }
        public CargoSection[] Cargo { get; set; }

        public class CargoSection
        {
            public CargoSection(int cargoId, Location destination, Location origin)
            {
                CargoId = cargoId;
                Destination = destination;
                Origin = origin;
            }

            public int CargoId { get; set; }
            public Location Destination { get; set; }
            public Location Origin { get; set; }
        }
    }

    public enum EventType
    {
        DEPART,
        ARRIVE
    }
}
