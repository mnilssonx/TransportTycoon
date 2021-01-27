using System;
using System.Linq;
using TransportSimApp.Simulation;

namespace TransportSimApp
{
    public static class ArgumentParser
    {
        public static Location[] Parse(string arg, Location[] validDestinations)
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException("At least one destination is required!");

            return arg
                .Split(",")
                .Select(a => Enum.TryParse<Location>(a, out var location) && validDestinations.Contains(location)
                    ? location 
                    : throw new ArgumentException($"Invalid destination: {a}!"))
                .ToArray();
        }
    }
}
