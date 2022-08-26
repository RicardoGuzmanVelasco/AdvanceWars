namespace AdvanceWars.Runtime
{
    public readonly struct Nation
    {
        public string Id { get; }

        public Nation(string id)
        {
            // TODO: Contract.Require(string.IsNullOrWhiteSpace(id)).False();
            Id = id;
        }

        public bool IsStateless => this.Equals(new Nation(null));

        public override string ToString()
        {
            return Id;
        }
    }
}