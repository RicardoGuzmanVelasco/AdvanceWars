﻿using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class TheaterOpsBuilder
    {
        Terrain battlefield = TerrainBuilder.Terrain().Build();
        Battalion troop = BattalionBuilder.Battalion().Build();

        TheaterOpsBuilder() { }
        public static TheaterOpsBuilder TheaterOps() => new TheaterOpsBuilder();

        public TheaterOpsBuilder Where(Terrain battlefield)
        {
            this.battlefield = battlefield;
            return this;
        }

        public TheaterOpsBuilder Who(Battalion troop)
        {
            this.troop = troop;
            return this;
        }

        public TheaterOpsBuilder WithVanisherOf(TheaterOps target)
        {
            Who
            (
                BattalionBuilder.Battalion().WithWeapon
                (
                    WeaponBuilder.Weapon().MaxDmgTo
                    (
                        target.Troops.Unit.Armor.ToString()
                    ).Build()
                ).Build()
            );
            return this;
        }

        public TheaterOps Build()
        {
            return new TheaterOps(battlefield, troop);
        }
    }
}