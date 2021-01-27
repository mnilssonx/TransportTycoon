using TransportSimApp.Simulation;

namespace TransportSimApp
{
    public static class Simulator
    {
        public static int CalculateTime(Location[] destinations, Routes routes, VehiclePool vehiclePool)
        {
            var world = new World(destinations, routes);

            while (!world.AllDestinationsHaveBeenReached())
            {
                foreach (var vehicle in vehiclePool.All)
                {
                    var pickupCandidate = world.GetNextPackageAtLocation(vehicle.Location);
                    if (vehicle.TryPickUp(pickupCandidate))
                    {
                        world.RegisterPickup(vehicle.Location, pickupCandidate);
                    }
                }

                foreach (var vehicle in vehiclePool.All)
                {
                    if (vehicle.TryDeliver(out var delivery))
                    {
                        world.RegisterDelivery(vehicle.Location, delivery);
                    }
                }

                world.Update();
            }

            return world.Time;
        }
    }
}
