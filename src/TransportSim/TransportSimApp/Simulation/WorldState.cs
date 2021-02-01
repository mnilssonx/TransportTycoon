using System.Collections.Generic;
using System.Linq;

namespace TransportSimApp.Simulation
{
    public class WorldState
    {
        private readonly Location[] _destinations;
        private readonly IDictionary<Location, List<Package>> _packagesAtLocations;

        public WorldState(Location[] destinations, Routes routes)
        {
            _destinations = destinations;

            // Initially queue all package at the factory
            _packagesAtLocations = Locations.All
                .ToDictionary(
                    l => l,
                    l => new List<Package>(
                        l == Location.FACTORY
                            ? destinations.Select((d, i) => new Package(i, Location.FACTORY, routes.ForDestination(d)))
                            : Enumerable.Empty<Package>()));
        }

        public int Time { get; private set; }

        public Package GetNextPackageAtLocation(Location location)
        {
            return _packagesAtLocations[location].FirstOrDefault();
        }

        public void RegisterDelivery(Location location, Package package)
        {
            _packagesAtLocations[location].Add(package);
        }

        public void RegisterPickup(Location location, Package package)
        {
            _packagesAtLocations[location].Remove(package);
        }

        public void Advance()
        {
            Time++;
        }

        public bool AllDestinationsHaveBeenReached()
        {
            var packageLocations = _packagesAtLocations.Values
                .SelectMany(packages => packages
                    .Select(p => p.Location)).ToList();

            // If the list of destinations equals the list of locations of all packages, they have all arrived
            return (_destinations.Length == packageLocations.Count && !packageLocations.Except(_destinations).Any());
        }
    }
}
