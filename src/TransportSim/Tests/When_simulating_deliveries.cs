using System.Linq;
using TransportSimApp;
using TransportSimApp.Config;
using Xunit;

namespace Tests
{
    public class When_simulating_deliveries
    {
        [Theory]
        [InlineData(5, "B")]
        [InlineData(5, "A")]
        [InlineData(5, "A", "B")]
        [InlineData(7, "A", "B", "B")]
        [InlineData(29, "A", "A", "B", "A", "B", "B", "A", "B")]
        [InlineData(29, "A", "A", "A", "A", "B", "B", "B", "B")]
        [InlineData(49, "B", "B", "B", "B", "A", "A", "A", "A")]
        public void Should_calculate_total_time(int expected, params string[] destinations)
        {
            var routes = DefaultRoutes.Get();
            var vehicles = DefaultVehicles.Get();

            var result = Simulator.CalculateTime(ArgumentParser.Parse(string.Join(",", destinations), routes.Destinations.ToArray()), routes, vehicles);

            Assert.Equal(expected, result);
        }
    }
}
