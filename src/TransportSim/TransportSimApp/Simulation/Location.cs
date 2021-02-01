using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportSimApp.Simulation
{
    public enum Location
    {
        FACTORY,
        A,
        B,
        PORT
    }

    public static class Locations
    {
        public static IEnumerable<Location> All => Enum.GetValues(typeof(Location)).Cast<Location>();
    }
}
