using System;
using TransportSimApp.Events;
using TransportSimApp.Simulation;

namespace TransportSimApp
{
    public static class Simulator
    {
        public static int CalculateTime(Location[] destinations, Routes routes, VehiclePool vehiclePool)
        {
            var worldState = new WorldState(destinations, routes);
            var eventDispatcher = new EventDispatcher(Console.WriteLine);

            while (true)
            {
                foreach (var vehicle in vehiclePool.All)
                {
                    var pickupCandidate = worldState.GetNextPackageAtLocation(vehicle.Location);
                    if (vehicle.TryPickUp(pickupCandidate))
                    {
                        worldState.RegisterPickup(vehicle.Location, pickupCandidate);
                    }

                    if (vehicle.Transport != null && vehicle.Transport.Traveled == 0)
                    {
                        eventDispatcher.Dispatch(new TransportEvent(EventType.DEPART, worldState.Time, vehicle.Transport, vehicle.Location));
                    }
                }

                if (worldState.AllDestinationsHaveBeenReached())
                    break;
                
                worldState.Advance();

                foreach (var vehicle in vehiclePool.All)
                {
                    if (!vehicle.TryCompleteTransport(out var completed)) 
                        continue;

                    if (completed.Package != null)
                        worldState.RegisterDelivery(vehicle.Location, completed.Package);

                    eventDispatcher.Dispatch(new TransportEvent(EventType.ARRIVE, worldState.Time, completed, vehicle.Location));
                }
            }

            return worldState.Time;
        }
    }
}
