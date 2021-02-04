using TransportSimApp.Simulation;

namespace TransportSimApp.Config
{
    public static class DefaultVehicles
    {
        public static VehiclePool Get()
        {
            return new VehiclePool(
                new Truck(),
                new Truck(),
                new Boat());
        }
    }
}
