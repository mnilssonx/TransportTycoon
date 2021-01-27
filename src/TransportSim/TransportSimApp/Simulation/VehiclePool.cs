namespace TransportSimApp.Simulation
{
    public class VehiclePool
    {
        private readonly Vehicle[] _vehicles;

        public VehiclePool(params Vehicle[] vehicles)
        {
            _vehicles = vehicles;
        }

        public Vehicle[] All => _vehicles;

        
    }
}