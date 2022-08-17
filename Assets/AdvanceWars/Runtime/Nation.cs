namespace AdvanceWars.Runtime
{
    public readonly struct Nation
    {
        string Id { get; init; }

        public Nation(string id)
        {
            Id = id;
        }

        public bool IsStateless => this.Equals(new Nation(null));
    }
}