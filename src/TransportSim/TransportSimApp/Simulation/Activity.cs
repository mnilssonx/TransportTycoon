namespace TransportSimApp.Simulation
{
    public interface IActivity
    {
    }

    public class Travel : IActivity
    {
        public Travel(RouteSegment segment, Package package)
        {
            Traveled = 0;
            RouteSegment = segment;
            Package = package;
        }

        public RouteSegment RouteSegment { get; }
        public Package Package { get; }

        public int Traveled { get; set; }
    }

    public class Idle : IActivity
    {

    }
}