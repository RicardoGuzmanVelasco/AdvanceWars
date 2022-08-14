namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Unit Occupant { get; set; }
            public bool HasUnit => Occupant != null;
        }
    }
}