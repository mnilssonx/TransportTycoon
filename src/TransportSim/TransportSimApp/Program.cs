using System;
using TransportSimApp.Config;

namespace TransportSimApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var routes = DefaultRoutes.Get();
            var vehicles = DefaultVehicles.Get();

            while (true)
            {
                Console.WriteLine("Enter destinations: ");
                var destinations = ArgumentParser.Parse(Console.ReadLine(), routes.Destinations);

                var totalTime = Simulator.CalculateTime(destinations, routes, vehicles);

                Console.WriteLine($"Total time: {totalTime}");
                Console.WriteLine("");
                Console.ReadLine();
            }
        }
    }
}
