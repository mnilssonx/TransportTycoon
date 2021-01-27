using System;
using System.Linq;
using TransportSimApp;
using TransportSimApp.Simulation;
using Xunit;

namespace Tests
{
    public class When_parsing_input
    {
        [Theory]
        [InlineData("A, B", Location.A, Location.B)]
        public void Should_return_locations(string input, params Location[] expected)
        {
            var result = ArgumentParser.Parse(input, Locations.All.ToArray());

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("A, X, B")]
        public void Should_throw_exception_for_invalid_location(string input)
        {
            Assert.Throws<ArgumentException>(() => ArgumentParser.Parse(input, Locations.All.ToArray()));
        }

        [Theory]
        [InlineData("A, X, B")]
        public void Should_throw_exception_for_invalid_destination(string input)
        {
            Assert.Throws<ArgumentException>(() => ArgumentParser.Parse(input, new Location[] {}));
        }

        [Theory]
        [InlineData("")]
        public void Should_throw_exception_for_empty_input(string input)
        {
            Assert.Throws<ArgumentException>(() => ArgumentParser.Parse(input, Locations.All.ToArray()));
        }
    }
}
