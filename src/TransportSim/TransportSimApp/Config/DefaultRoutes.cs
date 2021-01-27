using TransportSimApp.Simulation;

namespace TransportSimApp.Config
{
    public static class DefaultRoutes
    {
        public static Routes Get()
        {
            return new Routes(
                new Route(Location.A, 
                    new RouteSegment(1, Location.Factory, Location.Port), 
                    new RouteSegment(4, Location.Port, Location.A)),
                new Route(Location.B, 
                    new RouteSegment(5, Location.Factory, Location.B)));
        }
    }
}
