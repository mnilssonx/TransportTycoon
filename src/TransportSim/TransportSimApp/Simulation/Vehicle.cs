using System.Linq;

namespace TransportSimApp.Simulation
{
    public abstract class Vehicle
    {
        private readonly RouteSegment[] _routeSegments;
        private IActivity _activity = new Idle();

        protected Vehicle(Location start, params RouteSegment[] segments)
        {
            Location = start;
            _routeSegments = segments;
        }

        public Location Location { get; set; }

        public bool TryPickUp(Package package)
        {
            if (package == null || _activity is Travel)
                return false;

            var matchingSegment = _routeSegments.FirstOrDefault(s => s.Destination == package.GetNextDeliverySegment().Destination);
            if (matchingSegment == null)
                return false;

            _activity = new Travel(matchingSegment, package);
            return true;
        }

        public bool TryDeliver(out Package package)
        {
            if (_activity is Travel travel)
            {
                travel.Traveled++;

                if (travel.Traveled >= travel.RouteSegment.Distance)
                {
                    Location = travel.RouteSegment.Destination;

                    if (travel.Package != null)
                    {
                        // Deliver and set course for home
                        package = travel.Package;
                        travel.Package.Unload(Location);
                        _activity = new Travel(travel.RouteSegment.Reverse(), null);
                        return true;
                    }
                    else
                    {
                        // We're home and ready for new travels
                        _activity = new Idle();
                    }
                }
            }

            package = null;
            return false;
        }
    }

    public class Boat : Vehicle
    {
        public Boat(params RouteSegment[] segments) : base(Location.Port, segments)
        {
        }
    }

    public class Truck : Vehicle
    {
        public Truck(params RouteSegment[] segments) : base(Location.Factory, segments)
        {
        }
    }
}