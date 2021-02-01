namespace TransportSimApp.Simulation
{
    public class Transport
    {
        public Transport(int id, TransportType type, RouteSegment segment, Package package)
        {
            Type = type;
            Id = id;
            Traveled = 0;
            RouteSegment = segment;
            Package = package;
        }

        public int Id { get; set; }
        public TransportType Type { get; set; }
        public RouteSegment RouteSegment { get; }
        public Package Package { get; }
        public int Traveled { get; set; }

        public Transport CreateReturn()
        {
            return new Transport(this.Id, this.Type, this.RouteSegment.Reverse(), null);
        }
    }
}