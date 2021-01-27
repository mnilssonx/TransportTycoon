namespace TransportSimApp.Simulation
{
    public class Route
    {
        public Route(Location destination, params RouteSegment[] segments)
        {
            Destination = destination;
            Segments = segments;
        }

        public Location Destination { get; }

        public RouteSegment[] Segments { get; }
    }

    public class RouteSegment
    {
        public RouteSegment(int distance, Location start, Location destination)
        {
            Distance = distance;
            Start = start;
            Destination = destination;
        }

        public int Distance { get; }

        public Location Start { get; }

        public Location Destination { get; }

        public RouteSegment Reverse() => new RouteSegment(Distance, Destination, Start);
    }
}