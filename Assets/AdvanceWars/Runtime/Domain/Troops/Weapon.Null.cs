using System.Collections.Generic;

namespace AdvanceWars.Runtime
{
    public partial record Weapon
    {
        public static Weapon Null { get; } = new NoWeapon();

        record NoWeapon : Weapon, INull
        {
            // ReSharper disable once ConvertToPrimaryConstructor (Rider bug concerning records ctor)
            public NoWeapon() : base(new Dictionary<Armor, int>()) { }

            public override string ToString()
            {
                return this.GetType().Name;
            }
        }
    }
}