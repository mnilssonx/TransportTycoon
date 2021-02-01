using System.Linq;

namespace TransportSimApp.Simulation
{
    public abstract class Vehicle
    {
        private static int _topTransportId = 0;
        private readonly RouteSegment[] _routeSegments;

        protected Vehicle(Location start, params RouteSegment[] segments)
        {
            Location = start;
            _routeSegments = segments;
        }

        public Location Location { get; private set; }
        public Transport Transport { get; private set; }

        protected abstract Transport CreateTransport(int id, RouteSegment segment, Package package);

        public bool TryPickUp(Package package)
        {
            if (package == null || Transport != null)
                return false;

            var matchingSegment = _routeSegments.FirstOrDefault(s => s.Destination == package.GetNextDeliverySegment().Destination);
            if (matchingSegment == null)
                return false;

            Transport = CreateTransport(_topTransportId++, matchingSegment, package);
            return true;
        }

        public bool TryCompleteTransport(out Transport completed)
        {
            var arrived = false;
            completed = null;

            if (Transport != null)
            {
                Transport.Traveled++;

                if (Transport.Traveled >= Transport.RouteSegment.Distance)
                {
                    arrived = true;
                    completed = Transport;
                    Location = Transport.RouteSegment.Destination;

                    if (Transport.Package != null)
                    {
                        // Deliver and set course for home
                        Transport.Package.Unload(Location);
                        Transport = Transport.CreateReturn();
                    }
                    else
                    {
                        // We're home and ready for new travels
                        Transport = null;
                    }
                }
            }

            return arrived;
        }
    }

    public class Boat : Vehicle
    {
        public Boat(params RouteSegment[] segments) : base(Location.PORT, segments)
        {
        }

        protected override Transport CreateTransport(int id, RouteSegment segment, Package package) => new Transport(id, TransportType.SHIP, segment, package);
    }

    public class Truck : Vehicle
    {
        public Truck(params RouteSegment[] segments) : base(Location.FACTORY, segments)
        {
        }

        protected override Transport CreateTransport(int id, RouteSegment segment, Package package) => new Transport(id, TransportType.TRUCK, segment, package);
    }
}