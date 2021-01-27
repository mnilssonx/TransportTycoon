using TransportSimApp.Simulation;

namespace TransportSimApp.Config
{
    public static class DefaultVehicles
    {
        public static VehiclePool Get()
        {
            var routes = DefaultRoutes.Get();

            return new VehiclePool(
                new Truck(
                    routes.ForDestination(Location.A).Segments[0], 
                    routes.ForDestination(Location.B).Segments[0]),
                new Truck(
                    routes.ForDestination(Location.A).Segments[0], 
                    routes.ForDestination(Location.B).Segments[0]),
                new Boat(
                    routes.ForDestination(Location.A).Segments[1]));
        }
    }
}
