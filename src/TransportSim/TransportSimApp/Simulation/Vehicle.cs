using System.Linq;

namespace TransportSimApp.Simulation
{
    public abstract class Vehicle
    {
        private static int _topTransportId = 0;

        protected Vehicle(Location start)
        {
            Location = start;
        }

        public Location Location { get; private set; }
        public Transport Transport { get; private set; }

        protected abstract Transport CreateTransport(int id, RouteSegment segment, Package package);

        public bool TryPickUp(Package package)
        {
            if (package == null || Transport != null)
                return false;

            Transport = CreateTransport(_topTransportId++, package.GetNextDeliverySegment(), package);
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
        public Boat() : base(Location.PORT)
        {
        }

        protected override Transport CreateTransport(int id, RouteSegment segment, Package package) => new Transport(id, TransportType.SHIP, segment, package);
    }

    public class Truck : Vehicle
    {
        public Truck() : base(Location.FACTORY)
        {
        }

        protected override Transport CreateTransport(int id, RouteSegment segment, Package package) => new Transport(id, TransportType.TRUCK, segment, package);
    }
}